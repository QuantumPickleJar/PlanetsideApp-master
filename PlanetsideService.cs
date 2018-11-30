using System;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace PsApp
{
    public class PlanetsideService : IDisposable
    {
        public string ServiceId { get; }
        //private string subMessage = { {"service":"event","action":"subscribe","worlds":[],"eventNames":["FacilityControl","MetagameEvent"]}"};
        public int selectedWorld;
        //public int selectedWorld = null;  have a method that gets called in the constructor that, if selectedWorld is null, asks the user to set a default world 

        public bool IsStarted { get; private set; }

        public PlanetsideService(string serviceId)
        {
            this.ServiceId = serviceId;
            this.ClientWebSocket = new ClientWebSocket();
        }



        public ClientWebSocket ClientWebSocket { get; private set; }



        public Character GetCharacterById(long characterId)
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
                string url = $"https://census.daybreakgames.com/s:PS2mobile2018/get/ps2/character/?name.first_lower={lowQuery}";

                json = client.DownloadString(url);
            }

            CharacterQueryResult resultClass = Newtonsoft.Json.JsonConvert.DeserializeObject<CharacterQueryResult>(json);
            //returns array of character objects 
            //for each 
            //foreach (Character c in resultClass.Characters)
            //{
            //    Console.WriteLine(c.Name);
            //}
            return resultClass.Characters.SingleOrDefault();
        }

        public async Task<CharacterQueryResult> GetMultipleCharacters(string lowQuery)
        {
            string json;

            using (var client = new WebClient())
            {
                string url = $"https://census.daybreakgames.com/s:PS2mobile2018/get/ps2:v2/characters/?name.first_lower=^{lowQuery}&c:limit=100&c:sort=name.first_lower";

                json = await client.DownloadStringTaskAsync(url);
            }

            CharacterQueryResult resultClass = Newtonsoft.Json.JsonConvert.DeserializeObject<CharacterQueryResult>(json);

            //returns array of character objects 
            //for each 
            //foreach (Character c in resultClass.Characters)
            //{
            //    Console.WriteLine(c.Name);
            //}

            return resultClass;
        }






        //event
        public event EventHandler<FacilityControlChangedEventArgs> FacilityControlChanged;

        protected void OnFaciltyControlChanged(FacilityControlChangedEventArgs e)
        {
            if (FacilityControlChanged != null)
                FacilityControlChanged(this, e);
            //console output to see if this is working how I think it does 
        }

        async void ListenToWebSocketStuff()
        {

            Uri serverUri = new Uri($"wss://push.planetside2.com/streaming?environment=ps2&service-id=s:{ServiceId}");

            await ClientWebSocket.ConnectAsync(serverUri, CancellationToken.None);


            string s = @"{\042service\042:\042event\042,\042action\042:\042subscribe\042,\042worlds\042:[\0421\042,\0429\042,\04210\042,\04211\042,\04213\042,\04217\042,\04218\042,\04219\042,\04225\042,\0421000\042,\0421001\042],\042eventNames\042:[\042FacilityControl\042,\042MetagameEvent\042]}";
            byte[] bytes = Encoding.UTF8.GetBytes(s);

            ArraySegment<byte> buffer = new ArraySegment<byte>(bytes);

            //while (true)
            //{
            //    var result = await ClientWebSocket.ReceiveAsync(buffer, CancellationToken.None);

            //    if (result.EndOfMessage)
            //    {
            //        Console.WriteLine("endofmessageloop"); 
            //    }
            //}

            // tell the socket to subscribe to the messages we want
            CancellationToken cancellationToken = CancellationToken.None;
            await ClientWebSocket.SendAsync(buffer, WebSocketMessageType.Text, true, cancellationToken);

            WebSocketReceiveResult result;

            while (IsStarted)
            {
                result = await ClientWebSocket.ReceiveAsync(buffer, cancellationToken);

                if (result.EndOfMessage)
                {
                    //start decoding the buffer (byte)array which will be converted into a json string

                    //decode
                    string resultString = buffer.ToString();

                    //serialize 


                    //then deserialize them into a json string 

                }

                //await ClientWebSocket.ReceiveAsync(something something something)

                // inspect the message

                // if end of message

                // see if it is a facility control change

                // raise the facility control change (on othe thread)

                OnFaciltyControlChanged(new FacilityControlChangedEventArgs
                {
                    // Facility Id
                    //facilityresolver
                    // Faction id
                });

            }



        }

        public async Task StartAsync()
        {
            // create a new thread
            Thread thread = new Thread(ListenToWebSocketStuff);
            thread.Start();

            // have the new thread call ListenToWebSocketStuff()

            IsStarted = true;
        }


        public async Task StopAsync(Thread thread)
        {
            IsStarted = false;

            // perform any cleanup??
            thread.Abort();
        }

        public void Dispose()
        {
            this.ClientWebSocket?.Dispose();
            this.ClientWebSocket = null;
        }
    }
}
