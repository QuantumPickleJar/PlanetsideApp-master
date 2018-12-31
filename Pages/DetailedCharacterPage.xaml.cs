using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PsApp.Gettables;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace PsApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailedCharacterPage : ContentPage
    {
        /// <summary>
        /// Event handler that is raised when the MyCharacter property is set.
        /// </summary>
        private event EventHandler MyCharacterChanged;

        protected virtual void OnMyCharacterChanged(EventArgs e)
        {
            if (MyCharacterChanged != null) 
                MyCharacterChanged(this, EventArgs.Empty);
        }

        private void RaiseOnMyCharacterChangedEvent(EventArgs e)
        {
            Console.WriteLine("\n\n[][]MYCHARACTER CHANGED[][]\n\n");
            OnMyCharacterChanged(e);
            theLoader.IsRunning = false;
            UpdateUI();
        }




        public DetailedCharacterPage(Character theChar)
        {
            BindingContext = new DetailedCharacterPageViewModel();
            InitializeComponent();
            _characterId = theChar.CharId;
            titleName = theChar.Name.First;
            pService = new PlanetsideService("trashpanda");
            //hopefully this will do the same as below, except it will get the WHOLE Character.

            //var a = await pService.GetSingleCharacterAsync(theChar.CharId).Result;

            //MyCharacter = new CharacterFull()
            //{
            //    Name = theChar.Name,
            //    character_id = theChar.CharId,
            //    faction_id = theChar.FactionId,
            //    battleRank = theChar.BattleRank
            //};
            //GetCharacter();
            theSource = theChar.ImageSrc;

            //set the title to whatever the name of the character is
        }
        private string titleName = string.Empty;
        private long _characterId;
        private ImageSource theSource;
        PlanetsideService pService;

        //made just so we can call something async when we change the character id 
        
        //character property 
        private CharacterFull _myCharacter;
        public CharacterFull MyCharacter
        {
            get { return _myCharacter; }
            set
            {
                _myCharacter = value;
                RaiseOnMyCharacterChangedEvent(EventArgs.Empty);
            }
        }
        

        //create a task factory 

        private async Task<CharacterFull> GetCharacter()
        {
            CharacterFull result = await pService.GetSingleCharacterAsync(MyCharacter.character_id);
            //MyCharacter = result;
            return result;
        }

        private async Task GetAndSetCharacter()
        {
            CharacterFull result = await pService.GetSingleCharacterAsync(MyCharacter.character_id);
            MyCharacter = result;
        }

        protected async Task WaitAndExecute(int milisec, Action actionToExecute)
        {
            await Task.Delay(milisec);
            actionToExecute();
        }

        private Task<CharacterFull> theTask = null;
        //private Task theTask = null;
        protected async override void OnAppearing()
        {
            theLoader.IsRunning = true;
            await WaitAndExecute(1000, () =>
            {
                //this works, but it doesn't wait like we want it to. 
                //theTask = new Task<CharacterFull>(() => pService.GetSingleCharacter(_characterId));
                //MyCharacter = pService.GetSingleCharacter(_characterId);
                //Console.WriteLine($"\n\n[][]DEBUG[][]\n{MyCharacter.Name.First}\n\n");
                SetCharacter();
                //await DoWhileLoading();
                //while MyCharacter is not null
            });
            
        }


        private async void SetCharacter()
        {

            //theStack.IsVisible = false;
            theLoader.IsRunning = true;
            //theTask = new Task<CharacterFull>(() => GetCharacter().Result);
            //theTask = new Task(async () => await GetAndSetCharacter());
            theTask = new Task<CharacterFull>(() => pService.GetSingleCharacter(_characterId));
            theTask.Start();
            theLoader.IsRunning = false;
            this.Title = (titleName + "'s stats");
            Console.WriteLine($"\n\n[][]TASK WHILE LOOP START[][]\n{theTask.Status.ToString()}\n");
            while (theTask.IsCompleted == false)
            {
                Console.WriteLine($"\n\n[][]Task status debug[][]\n{theTask.Status.ToString()}\n");
                //do nothing
                //if (theTask.Status == TaskStatus.RanToCompletion) break;
            }
            Console.WriteLine($"\n\n[][]EXITED WHILE LOOP[][]\n{theTask.Status.ToString()}\n");
            if (theTask.IsCompleted && theTask.IsFaulted == false)
            {
                MyCharacter = theTask.Result;
            }
            if(theTask.IsFaulted)
            {
                DisplayAlert("Error", $"Error downloading character.\n{theTask.Exception.InnerException.ToString()}\n", "Okay");
            }
        }

        private void UpdateUI()
        {
            //Name.SetBinding(Label.TextProperty, MyCharacter.Name.First);

            //spanName.Text = MyCharacter.Name.First;
            //spanBrVal.Text = MyCharacter.battle_rank.Value.ToString();
            //spanCurrCerts.Text = MyCharacter.certs.PointsBalance.ToString();
            //spanSpentCerts.Text = MyCharacter.certs.SpentCerts.ToString();
            //spanTotCerts.Text = MyCharacter.certs.TotalCerts.ToString();
            //spanCharId.Text = MyCharacter.character_id.ToString();
            theLoader.IsRunning = false;
            theStack.IsVisible = true;
        }

        async void AnimatedFetch()
        {
            ////subscribedMessages.Clear();
            //recentEvents.IsVisible = false;
            //refreshFeedBtn.IsEnabled = false;
            //feedLoader.IsRunning = true;
            //IsLoading = true;
            await Task.Run(() => GetCharacter());
            //IsLoading = false;

            //recentEvents.IsVisible = true;
            //refreshFeedBtn.IsEnabled = true;
            //feedLoader.IsRunning = false;
        }

        private async Task FetchCharacterInfo()
        {
            await Task.Run(() => GetCharacter());
            //    //IsLoading = false;

            //recentEvents.IsVisible = true;
            //refreshFeedBtn.IsEnabled = true;
            //feedLoader.IsRunning = false;
        }

       
    }

    public partial class CardViewCell : ViewCell
    {
        public void OnTap()
        {
            var dropAnimation = new Animation(d =>
            {
                this.Height = d;
                this.ForceUpdateSize();
            }
            , this.Height
            , 0
            , Easing.Linear);

            dropAnimation.Commit(this.ParentView, "DropSize", 16, (uint)350, Easing.Linear);
        }
    }
}