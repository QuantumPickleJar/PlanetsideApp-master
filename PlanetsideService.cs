using System;
using System.Collections.Generic;
//using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace PsApp
{
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
        //easy-override constructor for forcing use of "example" service id for when the API is fucked (thanks daybreak)
        public PlanetsideService(string serviceId, bool dev)
        {
            this.ServiceId = serviceId;
            this.SynchronizationContext = SynchronizationContext.Current;
            if (dev) this.ServiceId = "example";
        }

        public List<string> returnList;
        

        public async Task<CharacterQueryResult> GetMultipleCharacters(string lowQuery)
        {
            string json;

            using (var client = new WebClient())
            {
                string url = $"https://census.daybreakgames.com/s:{ServiceId}/get/ps2:v2/character/?name.first_lower=^{lowQuery}&c:limit=100&c:sort=name.first_lower";

                json = await client.DownloadStringTaskAsync(url);
            }

            CharacterQueryResult resultClass = Newtonsoft.Json.JsonConvert.DeserializeObject<CharacterQueryResult>(json);
            return resultClass;
        }
        


        //event
        public event EventHandler<FacilityControlChangedEventArgs> FacilityControlChanged;

        protected void OnFaciltyControlChanged(FacilityControlChangedEventArgs e)
        {
            if (FacilityControlChanged != null)
                FacilityControlChanged(this, e);
//            output sample:
//            {
//                "payload":
//                {
//                    "duration_held":"87",
//                    "event_name":"FacilityControl",
//                    "facility_id":"310088",
//                    "new_faction_id":"2",
//                    "old_faction_id":"3",
//                    "outfit_id":"0",
//                    "timestamp":"1543955563",
//                    "world_id":"13",
//                    "zone_id":"528744543"
//                },
//                "service":"event",
//                "type":"serviceMessage"
//            }

        }

        protected void RaiseFacilityControlOnMainThread(FacilityControlChangedEventArgs e)
        {
            this.SynchronizationContext.Post(x =>
            {
                OnFaciltyControlChanged(e);
            }, null);
        }

        async void ListenToWebSocketStuff()
        {
            // fake it until we make it


            //FacilityControlChangedEventArgs e;

            //Random r = new Random();

            //while (IsStarted)
            //{
            //    await Task.Delay(TimeSpan.FromSeconds(5));


            //    this.SynchronizationContext.Post(x =>
            //    {
            //        OnFaciltyControlChanged(new FacilityControlChangedEventArgs()
            //        {
            //            facility_id = r.Next().ToString()
            //        });
            //    }, null);

            //    e = new FacilityControlChangedEventArgs()
            //    {
            //        facility_id = r.Next().ToString()
            //    };

            //    OnFaciltyControlChanged(e);
            //}

            //return;

            using (var clientWebSocket = new ClientWebSocket())
            {


                this.ServiceId = "example";
                Uri serverUri = new Uri($"wss://push.planetside2.com/streaming?environment=ps2&service-id=s:example");

                //Uri serverUri = new Uri($"wss://push.planetside2.com/streaming?environment=ps2&service-id=s:{ServiceId}");


                await clientWebSocket.ConnectAsync(serverUri, CancellationToken.None);



                // create our command that we're going to tell the service 
                string s = @"{\042service\042:\042event\042,\042action\042:\042subscribe\042,\042worlds\042:[\0421\042,\0429\042,\04210\042,\04211\042,\04213\042,\04217\042,\04218\042,\04219\042,\04225\042,\0421000\042,\0421001\042],\042eventNames\042:[\042FacilityControl\042,\042MetagameEvent\042]}";
                s = "{'service':'event','action':'subscribe','worlds':['1','9','10','11','13','17','18','19','25','1000','1001'],'eventNames':['FacilityControl','MetagameEvent']}";
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
                        //resultString = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                        resultString = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);

                        //empty the buffer so it's ready for a new message
                        buffer = new ArraySegment<byte>(new byte[1024]);
                        Console.WriteLine("RESULT STRING:  " + resultString);

                        //how will we do the following?

        //if (resultString is facilityControlChange)
      //{

                        // deserialize payload to a FacilityControlChangedEventArgs

                        //RaiseFacilityControlOnMainThread( // the deserialized value)
                       

                        //deserialize the resultString into an object of type ReceivedMsg
                        Message message = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(resultString);
                        
                        //filter out the help message
                    if (message.Service != "push"
                        && !(message.Service == "event" && message.Action == "help"))
                    {
                        Events.ReceivedMsg rMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<Events.ReceivedMsg>(resultString);
                            
                        if (rMsg.Service == "event" && rMsg.Type == "serviceMessage")
                        {
                            //okay, it's an event.  That's the first criteria
                            //Events.Payload.EventPayload payload = message.newPayload;


                            //if rMsg.newPayload.Event_name matches any entry in the eventsWeWant
                                
                                string innerPayloadJSON = string.Empty;
                            //if it's not specified for whatever reason, assume user wants FacilityControlArgs
                            if (eventsWeWant == null)
                            {
                                eventsWeWant = new List<string>();
                                this.eventsWeWant.Add("FacilityControl");
                                this.eventsWeWant.Add("MetagameEvent");
                            }

                            int position = Array.IndexOf(eventsWeWant.ToArray(), rMsg.newPayload.Event_name);
                            if (position > -1)
                            //if (rMsg.newPayload.Event_name == "FacilityControlChanged")
                            {
                                    //get the event payload
                                    Console.WriteLine("YEP ITS AN EVENT PAYLOAD   " + rMsg.newPayload.ToString());
                                    innerPayloadJSON = rMsg.newPayload.ToString();
                            }
                        }
//                            now we turn the rMsg.newPayload into an event, depending on the event_Name;
                            if (rMsg.newPayload.Event_name == "FacilityControl")
                                Newtonsoft.Json.JsonConvert.DeserializeObject<Events.FacilityControlChangedEvent>(innerPayloadJSON);

                            if (rMsg.newPayload.Event_name == "MetagameEvent")
                                Newtonsoft.Json.JsonConvert.DeserializeObject<Events.MetagameEventEvent>(innerPayloadJSON);
                            //}                            
                        }

                }   
                    //await SendTestCommand();
                    //we still need to send the test subscription message Command up to the service later.  

                    //set this string to something

                    //return the string as a message so that we can somehow put it into a ListView


                    // inspect the message

                    // if end of message

                    // see if it is a facility control change

                    // raise the facility control change (on other thread)


                    //OnFaciltyControlChanged(new FacilityControlChangedEventArgs
                    //{

                    //    // Facility Id
                    //    //call method to FacilityResolver to get the associated name for the ID
                    //    // Faction id
                    //});
                    //break;
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
        
        public async void AddResult(List<string> list)
        {
            returnList = list;
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
        

        // need to find a better way of suspending the websocket so that the user can re-subscribe without having to completely shutdown and restart the app
        //pressing subscribe after stopping yields this exception:
        //System.ObjectDisposedException: Cannot access a disposed object.
        //Object name: 'System.Net.WebSockets.ClientWebSocket'.
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
        
    }
}                
