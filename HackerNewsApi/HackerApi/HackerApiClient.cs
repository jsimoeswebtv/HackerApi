using HackerNewsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace HackerNewsApi.HackerApi
{
    public class HackerApiClient : IApiClientInterface
    {
        #region Private Fields

        private HttpClient client = new HttpClient();

        #endregion Private Fields

        #region Public Methods

        public async Task<Story> Get(int StoryId)
        {
            string path = $"https://hacker-news.firebaseio.com/v0/item/{StoryId}.json";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Story>();
            }
            else
            {
                return null;
            }
        }

        public async Task<int[]> GetBestStories()
        {
            string path = "https://hacker-news.firebaseio.com/v0/beststories.json";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<int[]>();
            }
            else
            {
                return null;
            }
        }

        #endregion Public Methods
    }
}