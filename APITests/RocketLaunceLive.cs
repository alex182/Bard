using Bard.Sources.Concretes;
using Bard.Sources.Interfaces;

namespace APITests
{
    [TestClass]
    public class RocketLaunceLive
    {
        private IRocketLaunchLiveAPIClientOptions _options;
        private IRocketLaunchLiveAPIClient _client;

        [TestInitialize]
        public void Setup()
        {
            _options = new RocketLaunchLiveAPIClientOptions()
            {
                ApiKey = Environment.GetEnvironmentVariable("RocketLaunchLiveAPIKey"),
                BaseUrl = "https://fdo.rocketlaunch.live"
            };

            if (string.IsNullOrEmpty(_options.ApiKey))
                throw new NullReferenceException(nameof(_options.ApiKey));

            var httpClient = new HttpClient();

            _client = new RocketLaunchLiveAPIClient(httpClient, _options);
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