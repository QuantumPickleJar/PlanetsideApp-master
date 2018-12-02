﻿using System;
using System.Collections.Generic;
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
        public PlanetsideService(string serviceId, List<string> list)
        {
            this.ServiceId = serviceId;
            this.ClientWebSocket = new ClientWebSocket();
            this.returnList = list;
        }
        public List<string> returnList;




        public async Task<CharacterQueryResult> GetMultipleCharacters(string lowQuery)
        {
            string json;

            using (var client = new WebClient())
            {
                string url = $"https://census.daybreakgames.com/s:PS2mobile2018query/get/ps2:v2/character/?name.first_lower=^{lowQuery}&c:limit=100&c:sort=name.first_lower";

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
            List<string> results = new List<string>();
            WebSocketReceiveResult result;
            string resultString = string.Empty;
            while (IsStarted)
            {
                //define the WebSocket result 
                result = await ClientWebSocket.ReceiveAsync(buffer, cancellationToken);

                if (result.EndOfMessage) //we have the full message. now we...
                {
                    //decode
                  
                    resultString = Encoding.ASCII.GetString(buffer.Array, buffer.Offset, result.Count);
                    

                    //DEBUG: add string to resultList so we can retrieve it into a listview in FeedPage
                    
                }
//                await SendTestCommand();
                //we still need to send the test subscription message Command up to the service later.  

                //set this string to something

                //return the string as a message so that we can somehow put it into a ListView


                // inspect the message

                // if end of message

                // see if it is a facility control change

                // raise the facility control change (on other thread)


                OnFaciltyControlChanged(new FacilityControlChangedEventArgs
                {
                    //possib le event names
                    //Death
                    //FacilityControl
                    //GainExperience
                    //ItemAdded
                    //MetagameEvent
                    //PlayerFacilityCapture
                    //PlayerFacilityDefend
                    //PlayerLogout
                    //PlayerLogin
                    //SkillAdded
                    //VehicleDestroy



                    // Facility Id
                    //call method to FacilityResolver to get the associated name for the ID
                    // Faction id
                });
                break;
            }
            results.Add(resultString);
            AddResult(results);
            ClientWebSocket.Abort();
            task_Complete = true;
            IsStarted = false;
            
        }



        //TEST METHOD
        //return a string to add to an array that is in a list of items

        public async Task<string> SendTestCommand()
        { 
            string resultString = string.Empty;
            string[] events = new string[] { "PlayerLogout", "PlayerLogin", "FacilityControlChanged" };
            string[] worlds = new string[] { "17" };

            Events.Command command = new Events.Command("subscribe", "event", worlds, events);


            //reconfigure command to a byte arraySegment so the API can receive it 
            byte[] bytes = Encoding.UTF8.GetBytes(command.ToString());
            ArraySegment<byte> buffer = new ArraySegment<byte>(bytes);
            CancellationToken cancellationToken = CancellationToken.None;
            WebSocketReceiveResult result;
            await ClientWebSocket.SendAsync(buffer, WebSocketMessageType.Text, true, cancellationToken);
            result = await ClientWebSocket.ReceiveAsync(buffer, cancellationToken);
            //check the new message we received 
            if (result.EndOfMessage)
            {
                resultString = Encoding.ASCII.GetString(buffer.Array, buffer.Offset, result.Count);

            }
            //return the message payload
            return await Task.Run(() => resultString);
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
        bool task_Complete = false;
        public bool CompletedTask()
        {
            return task_Complete; 
        }

        // need to find a better way of suspending the websocket so that the user can re-subscribe without having to completely shutdown and restart the app
        //pressing subscribe after stopping yields this exception:
        //System.ObjectDisposedException: Cannot access a disposed object.
        //Object name: 'System.Net.WebSockets.ClientWebSocket'.
        public async Task StopAsync()
        {

            ClientWebSocket.Abort();
            ClientWebSocket.Dispose();
            IsStarted = false;
            // perform any cleanup??
            //thread.Abort();
        }

        public List<string> GetReturnList()
        {
            return returnList;
        }

        void Dispose()
        {
            //this.ClientWebSocket?.Dispose();
            this.ClientWebSocket.Dispose();
            this.ClientWebSocket = null;
        }

        void IDisposable.Dispose()
        {
            ((IDisposable)ClientWebSocket).Dispose();
        }
    }
}                
