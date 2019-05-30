using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Selenoid.Client.Infrastructure.Common.List;
using Selenoid.Client.Infrastructure.Video.List;
using Selenoid.Client.Models;

namespace Selenoid.Client
{
    public class SelenoidVideoClient : ISelenoidVideoClient
    {
        private readonly HttpClient httpClient;
        private readonly ISelenoidClientSettings settings;
        private readonly IListItemsConverter<VideoListItemConverter> listItemsConverter;

        public SelenoidVideoClient(HttpClient httpClient, 
            ISelenoidClientSettings settings,
            IListItemsConverter<VideoListItemConverter> listItemsConverter)
        {
            this.httpClient = httpClient;
            this.settings = settings;
            this.listItemsConverter = listItemsConverter;
        }

        public async Task<List<SelenoidListItem>> GetAsync()
        {
            var stringResponse = await httpClient.GetStringAsync($"{settings.SelenoidHostUrl}/video/");
            var items = listItemsConverter.Convert(stringResponse);
            return items;
        }

        public Task<Stream> GetAsync(string videoName)
        {
            var fileUrl = $"{settings.SelenoidHostUrl}/video/{videoName}";
            return httpClient.GetStreamAsync(fileUrl);
        }

        public Task DeleteAsync(string videoName)
        {
            return httpClient.DeleteAsync($"{settings.SelenoidHostUrl}/video/{videoName}.");
        }
    }
}