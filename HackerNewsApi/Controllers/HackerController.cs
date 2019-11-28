using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNewsApi.HackerApi;
using HackerNewsApi.Memory;
using HackerNewsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackerController : ControllerBase
    {
        #region Private Fields

        private ICacheInterface<Story> _HackerCacheCache;
        private IApiClientInterface _Httpclient;

        #endregion Private Fields

        #region Public Constructors

        public HackerController(ICacheInterface<Story> memCache, IApiClientInterface client)
        {
            _HackerCacheCache = memCache;
            _Httpclient = client;
        }

        #endregion Public Constructors

        #region Public Methods

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<List<Story>>> GetAsync()

        {
            List<Story> returnList = new List<Story>();
            int[] results = await _Httpclient.GetBestStories();

            for (int i = 0; i < 20; i++)
            {
                var myStory = await _HackerCacheCache.GetOrCreate(results[i], async () => await _Httpclient.Get(results[i]));
                returnList.Add(myStory);
            }

            return returnList.OrderByDescending(o => o.score).ToList();
        }

        #endregion Public Methods
    }
}