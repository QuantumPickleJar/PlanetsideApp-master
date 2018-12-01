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
        //public int selectedWorld = null;  have a method that gets called in the constructor that, if selectedWorld is null, asks the user to set a default world 
        public int selectedWorld;       
        public ClientWebSocket ClientWebSocket { get; private set; }

        public bool IsStarted { get; private set; }

        public PlanetsideService(string serviceId)
        {
            this.ServiceId = serviceId;
            this.ClientWebSocket = new ClientWebSocket();
        }



        
        public async Task<CharacterQueryResult> GetMultipleCharacters(string lowQuery)
        {
            string json;

            using (var client = new WebClient())
            {
                string url = $"https://census.daybreakgames.com/s:PS2mobile2018query/get/ps2:v2/character/?name.first_lower=^{lowQuery}&c:limit=100&c:sort=name.first_lower";

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



        //*******IMPORTANT*********
        /// <summary>
        /// There needs to be some sort of way for the client PlanetsideServices to recognize the ServiceID not registered error, 
        /// as this goes away after 1-3 minutes of not sending any requests under the ServiceID
        /// 
        /// Failure to implement such a method could easily derail the app if enough users failed a search at once.  
        /// 
        /// Maybe consider applying for a second ServiceId that is to be used specifically for querying?
        /// 
        /// 
        /// EDIT: Received second ServiceId to use for querying
        /// </summary>


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



            // create our command that we're going to tell the service 
            string s = @"{\042service\042:\042event\042,\042action\042:\042subscribe\042,\042worlds\042:[\0421\042,\0429\042,\04210\042,\04211\042,\04213\042,\04217\042,\04218\042,\04219\042,\04225\042,\0421000\042,\0421001\042],\042eventNames\042:[\042FacilityControl\042,\042MetagameEvent\042]}";
            
            // convert the command into an array of Bytes
            byte[] bytes = Encoding.UTF8.GetBytes(s);

            // convert to ArraySegment
            ArraySegment<byte> buffer = new ArraySegment<byte>(bytes);

            
            CancellationToken cancellationToken = CancellationToken.None;
            //send the command asynchroneously 
            await ClientWebSocket.SendAsync(buffer, WebSocketMessageType.Text, true, cancellationToken);

            WebSocketReceiveResult result;

            while (IsStarted)
            {
                //define the WebSocket result 
                result = await ClientWebSocket.ReceiveAsync(buffer, cancellationToken);

                if (result.EndOfMessage) //we have the full message. now we...
                {
                    //decode
                    string resultString = Encoding.ASCII.GetString(buffer.Array, buffer.Offset, result.Count);
                    
                    //inspect the message
                    //the first one we'll get is a 
                }


                // inspect the message

                // if end of message

                // see if it is a facility control change

                // raise the facility control change (on other thread)

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
