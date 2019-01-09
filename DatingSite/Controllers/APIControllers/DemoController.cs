using DatingSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DatingSite.Controllers.APIControllers
{
    [RoutePrefix("api/Tests")]
    public class DemoController : ApiController
    {
        // GET: api/Demo
        [Route("Test/Get")]
        public IEnumerable<Customer> Get()
        {
            return new List<Customer>
            {
                new Customer { Id=1, Name = "abc"},
                new Customer { Id=2, Name = "bbc"},
                new Customer { Id=3, Name = "cbc"}
            };
        }

        // GET: api/Demo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Demo
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Demo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Demo/5
        public void Delete(int id)
        {
        }
    }
}
