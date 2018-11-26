using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlanetsideApi.Tests
{
    [TestClass]
    public class PlanetsideServiceTests
    {
        const string SERVICE_ID = "YOUR_API_SERVICE_ID_HERE";

        [TestMethod]
        public void ServiceIdIsCorrect()
        {
            // setup
            const string serviceId = SERVICE_ID;

            // act
            PlanetsideService service = new PlanetsideService(serviceId);

            // verify
            Assert.AreEqual(serviceId, service.ServiceId);

        }

        [TestMethod]
        public void GetCharacterByIdWorks()
        {
            // setup
            PlanetsideService service = new PlanetsideService(SERVICE_ID);
            const long characterId = 5428010618020694593;

            // act
            var character = service.GetCharacter(characterId);

            //verify
            Assert.IsNotNull(character);
            Assert.IsNotNull(character.Name);
            Assert.IsNotNull(character.Name.First);
            Assert.AreEqual("Dreadnaut", character.Name.First);
        }

        [TestMethod]
        public async Task SubscribeToFascilityControlChangeWOrks()
        {
            // setup
            PlanetsideService service = new PlanetsideService(SERVICE_ID);
            TaskCompletionSource<FacilityControlChangedEventArgs> taskCompletionSource = new TaskCompletionSource<FacilityControlChangedEventArgs>();

            service.FacilityControlChanged += (o, e) =>
            {
                taskCompletionSource.SetResult(e);
            };

            await service.StartAsync();

            Task timeoutTask = Task.Delay(TimeSpan.FromSeconds(10));

            // act
            Task theTaskThatFinished = await Task.WhenAny(timeoutTask, taskCompletionSource.Task);



            // verify

            // See which task finishes first
            // if the timeoutTask finishes first, then that's bad (no event was raised for FacilityControlChanged within 10 seconds)
            Assert.AreNotEqual(timeoutTask, theTaskThatFinished);
            var result = taskCompletionSource.Task.Result;
            Assert.IsNotNull(result);
        }
    }
}
