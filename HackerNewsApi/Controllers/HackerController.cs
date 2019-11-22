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
        IMemoryCacheImplement<Story> _HackerCacheCache;
        IHackerApiClient Httpclient;
       public HackerController(IMemoryCacheImplement<Story> memCache, IHackerApiClient client)
        {
            _HackerCacheCache = memCache;
            Httpclient = client;
        }
       
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
