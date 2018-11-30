using System;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using PsApp.Events;
namespace PsApp
{
    public class PlanetsideService : IDisposable
    {
        public string ServiceId { get; }

        public PlanetsideService(string serviceId)
        {
            this.ServiceId = serviceId;
            this.ClientWebSocket = new ClientWebSocket();
        }



        //public string FacilityChanged()
        //{

        //}

        ////
        //public event EventHandler<FacilityControlChangedEventArgs> FacilityControlChanged


        public ClientWebSocket ClientWebSocket { get; private set; }

        public void GetCharacterById()
        {
            // setup
            PlanetsideService service = new PlanetsideService(ServiceId);
            const long characterId = 5428010618020694593;

            // act
            var character = service.GetCharacter(characterId);

            //verify
            string query = "Dreadnaut";

            //if (Equals(character.Name.First, "Dreadnaut"))
            //{
            //    //create new passable entry that can be read by the ListView
            //    MainPage.resultListView
            //}
            //Assert.IsNotNull(character.Name);
            //Assert.IsNotNull(character.Name.First);
            //Assert.AreEqual("Dreadnaut", character.Name.First);
        }


        public Character GetCharacter(long characterId)
        {
            string json;

            using (var client = new WebClient())
            {
                json = client.DownloadString($"http://census.daybreakgames.com/get/ps2:v2/character/?character_id={characterId}&c:show=name,faction_id,battle_rank");
            }

            CharacterQueryResult result = Newtonsoft.Json.JsonConvert.DeserializeObject<CharacterQueryResult>(json);

            return result.Characters.SingleOrDefault();
        }

        public Character GetCharacter(string lowQuery)
        {
            string json;

            using (var client = new WebClient())
            {
                json = client.DownloadString($"https://census.daybreakgames.com/s:PS2mobile2018/get/ps2/character/?name.first_lower={lowQuery}");
            }

            CharacterQueryResult resultClass = Newtonsoft.Json.JsonConvert.DeserializeObject<CharacterQueryResult>(json);
            //returns array of character objects 
            //for each 
            foreach (Character c in resultClass.Characters)
            {
                Console.WriteLine(c.Name);
            }
            return resultClass.Characters.SingleOrDefault();
        }




        public async Task GetMultipleCharacters(string lowQuery)
        {
            string json;

            using (var client = new WebClient())
            {
                json = await client.DownloadStringTaskAsync($"https://census.daybreakgames.com/s:PS2mobile2018/get/ps2/character/?name.first_lower={lowQuery}");
            }

            CharacterQueryResult resultClass = Newtonsoft.Json.JsonConvert.DeserializeObject<CharacterQueryResult>(json);

            //returns array of character objects 
            //for each 
            //foreach (Character c in resultClass.Characters)
            //{
            //    Console.WriteLine(c.Name);
            //}
            
        }

        public async Task StartAsync()
        {
            Uri serverUri = new Uri($"wss://push.planetside2.com/streaming?environment=ps2&service-id=s:{ServiceId}");

            await ClientWebSocket.ConnectAsync(serverUri, CancellationToken.None);

            ArraySegment<byte> buffer = new ArraySegment<byte>();

            while (true)
            {
                var result = await ClientWebSocket.ReceiveAsync(buffer, CancellationToken.None);

                if (result.EndOfMessage)
                {
                    Console.WriteLine("endofmessageloop"); 
                }
            }
        }

        public void Dispose()
        {
            this.ClientWebSocket?.Dispose();
            this.ClientWebSocket = null;
        }
    }
}
