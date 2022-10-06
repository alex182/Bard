using Bard.Sources.Concretes;
using Bard.Sources.Interfaces;

namespace APITests
{
    [TestClass]
    public class RocketLaunceLive
    {
        private IRocketLaunchLiveAPIClient _client;

        [TestInitialize]
        public void Setup()
        {
            var options = new RocketLaunchLiveAPIClientOptions()
            {
                ApiKey = Environment.GetEnvironmentVariable("RocketLaunchLiveAPIKey"),
                BaseUrl = "https://fdo.rocketlaunch.live"
            };

            if (string.IsNullOrEmpty(options.ApiKey))
                throw new NullReferenceException(nameof(options.ApiKey));

            var httpClient = new HttpClient();

            _client = new RocketLaunchLiveAPIClient(httpClient, options);
        }

        [TestMethod]
        public async Task GetLaunches()
        {
            var now = DateTime.UtcNow;
            var nextYear = now.AddYears(1);

            var afterDate = $"{now.Year}-{now.Month}-{now.Day}";
            var beforeDate = $"{nextYear.Year}-{nextYear.Month}-{nextYear.Day}";

            var result = await _client.GetLaunchesBetweenDates(afterDate, beforeDate);

            Assert.IsTrue(result.IsSuccessStatusCode);
            Assert.IsTrue(result.Result.Count > 0);
        }

    }
}