using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarDetailingWebApi.Controllers
{
    public class UtilityController : ApiController
    {
        //// GET: api/Utility
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        [HttpGet]
        [Route("api/PublicKey")]
        // GET: api/Utility/5
        public string Get()
        {
            return "value";
        }

        //// POST: api/Utility
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Utility/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Utility/5
        //public void Delete(int id)
        //{
        //}
    }
}
