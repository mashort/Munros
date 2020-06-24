using Munros.API;
using Munros.Core.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Munros.Test
{
    public class MunrosControllerShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public MunrosControllerShould(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnOnlyCategorisedMunros()
        {
            var response = await _client.GetAsync("/munros");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Munro>>(stringResponse).ToList();

            Assert.DoesNotContain(result, m => string.IsNullOrEmpty(m.Category));
        }

        [Fact]
        public async Task ReturnOnlyMunrosWhenCategoryOfMunroIsRequested()
        {
            var response = await _client.GetAsync("/munros?category=MUN");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Munro>>(stringResponse).ToList();

            Assert.DoesNotContain(result, m => m.Category == "TOP");
            Assert.Contains(result, m => m.Category == "MUN");
        }

        [Fact]
        public async Task ReturnOnlyTopsWhenCategoryOfTopIsRequested()
        {
            var response = await _client.GetAsync("/munros?category=TOP");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Munro>>(stringResponse).ToList();

            Assert.DoesNotContain(result, m => m.Category == "MUN");
            Assert.Contains(result, m => m.Category == "TOP");
        }

        [Fact]
        public async Task ReturnMunrosAndTopsWhenCategoryOfEitherIsRequested()
        {
            var response = await _client.GetAsync("/munros?category=either");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Munro>>(stringResponse).ToList();

            Assert.Contains(result, m => m.Category == "MUN");
            Assert.Contains(result, m => m.Category == "TOP");
        }

        [Fact]
        public async Task LimitNumberOfResultsReturnedToResultsLimitThatIsRequested()
        {
            var response = await _client.GetAsync("/munros?resultslimit=2");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Munro>>(stringResponse).ToList();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task ReturnOnlyResultsOfTheMinimumHeightRequested()
        {
            var response = await _client.GetAsync("/munros?minheight=932");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Munro>>(stringResponse).ToList();

            Assert.DoesNotContain(result, m => m.Height < 932);
        }

        [Fact]
        public async Task ReturnOnlyResultsUpToAndIncludingTheMaximumHeightRequested()
        {
            var response = await _client.GetAsync("/munros?maxheight=932");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Munro>>(stringResponse).ToList();

            Assert.DoesNotContain(result, m => m.Height > 932);
        }

        [Fact]
        public async Task ReturnOnlyResultsWithinTheRangeOfTheMinAndMaxHeightsRequested()
        {
            var response = await _client.GetAsync("/munros?minheight=932&maxheight=1000");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Munro>>(stringResponse).ToList();

            Assert.DoesNotContain(result, m => m.Height < 932);
            Assert.DoesNotContain(result, m => m.Height > 1000);
        }

        [Fact]
        public async Task ReturnResultsSortedByNameIfRequested()
        {
            var response = await _client.GetAsync("/munros?primarysortby=name");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Munro>>(stringResponse).ToList();

            Assert.Equal("Regular Munro 1", result.FirstOrDefault().Name);
        }

        [Fact]
        public async Task ReturnResultsSortedByNameInDescendingOrderIfRequested()
        {
            var response = await _client.GetAsync("/munros?primarysortby=name&primarysortorder=desc");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Munro>>(stringResponse).ToList();

            Assert.Equal("Regular Top B", result.FirstOrDefault().Name);
        }

        [Fact]
        public async Task ReturnResultsSortedByHeightDescAndThenNameAscIfRequested()
        {
            var response = await _client.GetAsync("/munros?category=TOP&primarysortby=height&primarysortorder=desc&secondarysortby=name&secondarysortorder=asc");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Munro>>(stringResponse).ToList();

            Assert.Equal("Regular Top 1", result.FirstOrDefault().Name);
            Assert.Equal("Regular Top B", result.LastOrDefault().Name);
        }
    }
}
