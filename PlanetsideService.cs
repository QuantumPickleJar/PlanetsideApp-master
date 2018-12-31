using System;
using System.Collections.Generic;
//using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
namespace PsApp
{

    //need to add a handler for the exception:
    //System.Net.WebSockets.WebSocketException: The remote party closed the WebSocket connection without completing the close handshake.

    public class PlanetsideService
    {
        public string ServiceId { get; private set; }
        //private string subMessage = { {"service":"event","action":"subscribe","worlds":[],"eventNames":["FacilityControl","MetagameEvent"]}"};
        //public int selectedWorld = null;  have a method that gets called in the constructor that, if selectedWorld is null, asks the user to set a default world 
        public int selectedWorld;
        private string innerPayloadJSON = string.Empty;
        private List<string> eventsWeWant;


        public SynchronizationContext SynchronizationContext { get; }
        public bool IsStarted { get; private set; }

        public PlanetsideService(string serviceId)
        {
            this.ServiceId = serviceId;
            this.SynchronizationContext = SynchronizationContext.Current;
        }
        public PlanetsideService(string serviceId, int server)
        {
            this.ServiceId = serviceId;
            this.SynchronizationContext = SynchronizationContext.Current;
            this.selectedWorld = server;
        }

        public List<string> returnList;


        public async Task<CharacterQueryResult> GetMultipleCharacters(string lowQuery)
        {
            string json;

            using (var client = new WebClient())
            {
                //string url = $"https://census.daybreakgames.com/s:{ServiceId}/get/ps2:v2/character/?name.first_lower=^{lowQuery}&c:limit=50&c:sort=name.first_lower";
                string url = $"https://census.daybreakgames.com/s:trashpanda/get/ps2:v2/character/?name.first_lower=^{lowQuery}&c:limit=50&c:sort=name.first_lower";

                json = await client.DownloadStringTaskAsync(url);
            }

            CharacterQueryResult resultClass = Newtonsoft.Json.JsonConvert.DeserializeObject<CharacterQueryResult>(json);
            return resultClass;
        }

        public Gettables.CharacterFull GetSingleCharacter(long theId)
        {
            string json;

            using (var client = new WebClient())
            {
                string url = $"https://census.daybreakgames.com/s:trashpanda/get/ps2:v2/character/?character_id={theId}&c:resolve=outfit_member_extended&c:resolve=stat_history&c:resolve=online_status&c:resolve=world&c:join=world";
                var a = Task.Run(() => client.DownloadString(url));
                json = a.Result;
            }

            Gettables.CharacterDetailList resultList = Newtonsoft.Json.JsonConvert.DeserializeObject<Gettables.CharacterDetailList>(json);
            //Gettables.CharacterFull result = resultList.CharacterResult[0];
            return resultList.FirstCharacter;
        }

        public async Task<Gettables.CharacterFull> GetSingleCharacterAsync (long theId)
        {
            string json;
            using (var client = new WebClient())
            {
                string url = $"https://census.daybreakgames.com/s:trashpanda/get/ps2:v2/character/?character_id={theId}&c:resolve=outfit_member_extended&c:resolve=stat_history&c:resolve=online_status&c:resolve=world&c:join=world";
                json = await client.DownloadStringTaskAsync(url);
            }

            Gettables.CharacterDetailList resultList = Newtonsoft.Json.JsonConvert.DeserializeObject<Gettables.CharacterDetailList>(json);
            return resultList.FirstCharacter;
        }

        /// <summary>
        /// Download up to 1000 latest events of all types from the Historic Events collection 
        /// </summary>
        /// <returns>Task-wrapped Events.WorldEventListResult</returns>
        public async Task<Events.WorldEventListResult> GetRecentEvents()
        {
            string pref = Preferences.Get("globalWorldId", "100", "theWorld");
            int time = ((int)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds) - 14400;
            string json;
            using (var client = new WebClient())
            {

                string uri = $"https://census.daybreakgames.com/s:trashpanda/get/ps2:v2/world_event/?world_id={pref}&after={time}&c:limit=1000";
                if (pref == "100") //if we're debugging
                    uri = $"https://census.daybreakgames.com/s:trashpanda/get/ps2:v2/world_event/?after={time}&c:limit=1000";
                json = await client.DownloadStringTaskAsync(uri);
            }
            Events.WorldEventListResult recentList = Newtonsoft.Json.JsonConvert.DeserializeObject<Events.WorldEventListResult>(json);
            return recentList;
        }

        /// <summary>
        /// Download up to 1000 latest events of type METAGAME from the Historic Events collection 
        /// </summary>
        /// <returns>Task-wrapped Events.WorldEventListResult</returns>
        public async Task<Events.WorldEventListResult> GetMetagameEVents()
        {
            string pref = Preferences.Get("globalWorldId", "100", "theWorld");
            int time = ((int)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds) - 28800; //28800 is last 8 hours 
            string json;
            using (var client = new WebClient())
            {
                string uri = $"https://census.daybreakgames.com/s:trashpanda/get/ps2:v2/world_event/?world_id={pref}&after={time}&type=METAGAME&c:limit=1000";
                if (pref == "100") //if we're debugging
                    uri = $"https://census.daybreakgames.com/s:trashpanda/get/ps2:v2/world_event/?after={time}&type=METAGAME&c:limit=1000";

                json = await client.DownloadStringTaskAsync(uri);
            }
            Events.WorldEventListResult recentList = Newtonsoft.Json.JsonConvert.DeserializeObject<Events.WorldEventListResult>(json);
            return recentList;
        }


        //public string CreateCommand(int selectedWorld, serviceId)
        //{

        //}


        async void ListenToWebSocketStuff()
        {
            var client = new System.Net.Http.HttpClient
            {
                BaseAddress = new Uri("http://1.2.3.4"),
                DefaultRequestHeaders = { Host = "example.com" }
            };
            
            using (var clientWebSocket = new ClientWebSocket())
            {
                this.ServiceId = "example";
                Uri serverUri = new Uri($"wss://push.planetside2.com/streaming?environment=ps2&service-id=s:trashpanda");

                //Uri serverUri = new Uri($"wss://push.planetside2.com/streaming?environment=ps2&service-id=s:{ServiceId}");


                await clientWebSocket.ConnectAsync(serverUri, CancellationToken.None);



                // create our command that we're going to tell the service 
                string s = @"{\042service\042:\042event\042,\042action\042:\042subscribe\042,\042worlds\042:[\0421\042,\0429\042,\04210\042,\04211\042,\04213\042,\04217\042,\04218\042,\04219\042,\04225\042,\0421000\042,\0421001\042],\042eventNames\042:[\042FacilityControl\042,\042MetagameEvent\042]}";
                //s = "{'service':'event','action':'subscribe','worlds':['17'],'eventNames':['FacilityControl','MetagameEvent','ContinentLock',ContinentUnlock']}";
                s = "{'service':'event','action':'subscribe','worlds':['1','10','13','17','19','25','40'],'eventNames':['FacilityControl','MetagameEvent','ContinentLock',ContinentUnlock']}";
                //use json later to be able to parse the this.selectedWorld into the string command.

                //create a method for turning the data from filterbox into a JSON command as seen above ^
                
                // convert the command into an array of Bytes
                byte[] bytes = Encoding.UTF8.GetBytes(s);

                // convert to ArraySegment
                ArraySegment<byte> buffer = new ArraySegment<byte>(bytes);
                CancellationToken cancellationToken = CancellationToken.None;
                //send the command asynchroneously 
                await clientWebSocket.SendAsync(buffer, WebSocketMessageType.Text, true, cancellationToken);
                List<string> results = new List<string>();
                WebSocketReceiveResult result;
                string resultString = string.Empty;
                while (IsStarted)
                {
                    //define the WebSocket result 
                    result = await clientWebSocket.ReceiveAsync(buffer, cancellationToken);

                    if (result.EndOfMessage) //we have the full message. now we...
                    {
                        //decode the buffer array to a UTF-8 string
                        resultString = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);

                        //empty the buffer so it's ready for a new message
                        buffer = new ArraySegment<byte>(new byte[1024]);

                        //deserialize the resultString into an object of type ReceivedMsg
                        Message message = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(resultString);

                        //filter out the help message
                        if (message.Service == "push")
                        {
                            Console.WriteLine("CONNECTION STATUS - - - - - " + message.ToString());
                            var args = new Events.MessageEventArgs()
                            {
                                connectionStatus = message.connectionStatus
                            };
                            RaiseConnectionStateOnMainThread(args);
                            Console.WriteLine("---------------------EVENTRAISED ON SERVICE END-----------------------------");
                            //this event just will NOT raise on the other page for some reason

                        }
                        if (message.Service != "push"
                                && !(message.Service == "event" && message.Action == "help"))
                            {
                                Events.ReceivedMsg rMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<Events.ReceivedMsg>(resultString);

                                if (rMsg.Service == "event" && rMsg.Type == "serviceMessage")
                                {
                                    //okay, it's an event.  That's the first criteria
                                    //Events.Payload.EventPayload payload = message.newPayload;


                                    //if rMsg.newPayload.Event_name matches any entry in the eventsWeWant

                                    var payload = rMsg.newPayload;
                                    //if it's not specified for whatever reason, assume user wants FacilityControlArgs
                                    if (eventsWeWant == null)
                                    {
                                        eventsWeWant = new List<string>();
                                        this.eventsWeWant.Add("FacilityControl");
                                        this.eventsWeWant.Add("MetagameEvent");
                                    }

                                    int position = Array.IndexOf(eventsWeWant.ToArray(), payload.Event_name);
                                    if (position > -1)
                                    {
                                        //get the event payload 
                                        Console.WriteLine("\nPAYLOAD RECEIVED " + rMsg.newPayload.ToString());
                                    }

                                    if (payload.Event_name == "FacilityControl")
                                    {
                                        //there is a lot of invalid information that we need to filter out with if statements

                                        if ((payload.old_faction_id != 0) &&
                                            (payload.duration_held < payload.Timestamp))
                                        {
                                            Events.FacilityControlChangedEvent newFCevent = Newtonsoft.Json.JsonConvert.DeserializeObject<Events.FacilityControlChangedEvent>(innerPayloadJSON);
                                            //raise event 
                                            var args = new FacilityControlChangedEventArgs()
                                            {
                                                Payload = payload
                                            };
                                            RaiseFacilityControlOnMainThread(args);
                                        }
                                    }

                                    //Generic event
                                    if (payload.Event_name == "MetagameEvent")
                                    {
                                        Events.MetagameEventEvent newMgEvent = Newtonsoft.Json.JsonConvert.DeserializeObject<Events.MetagameEventEvent>(innerPayloadJSON);
                                        //raise event 
                                        var args = new Events.World.MetagameEventEventArgs()
                                        {
                                            Payload = payload
                                        };
                                        RaiseMetagameEventOnMainThread(args);
                                    }

                                    //ContinentLock
                                    if (payload.Event_name == "MetagameEvent" && payload.metagame_event_state_name=="ended")
                                    {
                                        Events.ContinentLockEvent newMgEvent = Newtonsoft.Json.JsonConvert.DeserializeObject<Events.ContinentLockEvent>(innerPayloadJSON);
                                        //raise event 
                                        var args = new Events.World.ContinentLockEventArgs()
                                        {
                                            Payload = payload
                                        };
                                        RaiseContinentLockEventOnMainThread(args);
                                    }

                                    //ContinentUnlock
                                    if (payload.Event_name == "MetagameEvent" && payload.metagame_event_state_name == "started")
                                    {
                                        Events.ContinentUnlockEvent newMgEvent = Newtonsoft.Json.JsonConvert.DeserializeObject<Events.ContinentUnlockEvent>(innerPayloadJSON);
                                        //raise event 
                                        var args = new Events.World.ContinentUnlockEventArgs()
                                        {
                                            Payload = payload
                                        };
                                        RaiseContinentUnlockEventOnMainThread(args);
                                    }
                                }
                            }
                        //await SendTestCommand
                    }
                }

            }

        }




        //TEST METHOD
        //return a string to add to an array that is in a list of items

        public async Task<string> SendTestCommand()
        {
            string resultString = string.Empty;
            string[] events = new string[] { "PlayerLogout", "PlayerLogin", "FacilityControlChanged" };
            string[] worlds = new string[] { "17" };
            eventsWeWant.Clear();
            foreach (string s in events)
            {
                eventsWeWant.Add(s);
            }
            Events.Command command = new Events.Command("subscribe", "event", worlds, events, true);

            using (var clientWebSocket = new ClientWebSocket())
            {
                //reconfigure command to a byte arraySegment so the API can receive it 
                byte[] bytes = Encoding.UTF8.GetBytes(command.ToString());
                ArraySegment<byte> buffer = new ArraySegment<byte>(bytes);
                CancellationToken cancellationToken = CancellationToken.None;
                WebSocketReceiveResult result;
                await clientWebSocket.SendAsync(buffer, WebSocketMessageType.Text, true, cancellationToken);
                result = await clientWebSocket.ReceiveAsync(buffer, cancellationToken);
                //check the new message we received 
                if (result.EndOfMessage)
                {
                    resultString = Encoding.ASCII.GetString(buffer.Array, buffer.Offset, result.Count);

                }
                //return the message payload
                return await Task.Run(() => resultString);
            }
        }
        

        private Thread thread;

        public async Task StartAsync()
        {
            // create a new thread
            thread = new Thread(ListenToWebSocketStuff);
            thread.Start();

            // have the new thread call ListenToWebSocketStuff()

            IsStarted = true;
        }



        public async Task StopAsync()
        {
            IsStarted = false;

            // perform any cleanup??
            //thread.Abort();
        }

        public List<string> GetReturnList()
        {
            return returnList;
        }


        public event EventHandler<Events.MessageEventArgs> ConnectionStateChanged;
        public event EventHandler<FacilityControlChangedEventArgs> FacilityControlChanged;
        public event EventHandler<Events.World.MetagameEventEventArgs> MetagameEventChange;
        public event EventHandler<Events.World.ContinentLockEventArgs> ContinentLocked;
        public event EventHandler<Events.World.ContinentUnlockEventArgs> ContinentUnlocked;

        protected void RaiseFacilityControlOnMainThread(FacilityControlChangedEventArgs e)
        {
            this.SynchronizationContext.Post(x =>
            {
                OnFaciltyControlChanged(e);
            }, null);
        }
        protected void RaiseMetagameEventOnMainThread(Events.World.MetagameEventEventArgs e)
        {
            this.SynchronizationContext.Post(x =>
            {
                OnMetagameEventChange(e);
            }, null);
        }
        protected void RaiseContinentLockEventOnMainThread(Events.World.ContinentLockEventArgs e)
        {
            this.SynchronizationContext.Post(x =>
            {
                OnContinentLock(e);
            }, null);
        }
        protected void RaiseContinentUnlockEventOnMainThread(Events.World.ContinentUnlockEventArgs e)
        {
            this.SynchronizationContext.Post(x =>
            {
                OnContinentUnlock(e);
            }, null);
        }

        protected void RaiseConnectionStateOnMainThread(Events.MessageEventArgs e)
        {
            this.SynchronizationContext.Post(x =>
            {
                OnConnectionChange(e);
            }, null);
        }

        protected void OnFaciltyControlChanged(FacilityControlChangedEventArgs e)
        {
            if (FacilityControlChanged != null)
                FacilityControlChanged(this, e);

        }
        protected void OnMetagameEventChange(Events.World.MetagameEventEventArgs e)
        {
            if (MetagameEventChange != null)
                MetagameEventChange(this, e);

        }
        protected void OnContinentLock(Events.World.ContinentLockEventArgs e)
        {
            if (ContinentLocked != null)
                ContinentLocked(this, e);

        }
        protected void OnContinentUnlock(Events.World.ContinentUnlockEventArgs e)
        {
            if (ContinentUnlocked != null)
                ContinentUnlocked(this, e);

        }
        
        protected void OnConnectionChange(Events.MessageEventArgs e)
        {
            if (ConnectionStateChanged != null)
                ConnectionStateChanged(this, e);

        }


        public void Notify()
        {
        }

    }
}
// documented because these only happen four times a day
//continent lock
//RECEIVED: 
//    {
//        "payload":
//            {
//                "event_name":"MetagameEvent",
//                "experience_bonus":"25.000000",
//                "faction_nc":"52.156864",
//                "faction_tr":"19.607843",
//                "faction_vs":"27.843140",
//                "instance_id":"20367",
//                "metagame_event_id":"152",
//                "metagame_event_state":"138",
//                "metagame_event_state_name":"ended",
//                "timestamp":"1544075818",
//                "world_id":"17"
//            },
//        "service":"event",
//        "type":"serviceMessage"
//    }
