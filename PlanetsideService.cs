using System;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace PlanetsideApi
{
    public class PlanetsideService : IDisposable
    {
        public PlanetsideService(string serviceId)
        {
            this.ServiceId = serviceId;
            this.ClientWebSocket = new ClientWebSocket();
        }

        public string ServiceId { get; }
        public event EventHandler<FacilityControlChangedEventArgs> FacilityControlChanged;
        public ClientWebSocket ClientWebSocket { get; private set; }
   
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
