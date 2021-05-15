using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.Azure;
// using AuctionSystemCore.Models;
using Microsoft.Extensions.Configuration;


namespace AuctionSystemCore.Controllers
{
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private IConfiguration _configuration;

        public ValuesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            // var db = new clientContext(HttpContext, _configuration);
            //  var blog = new Blog { Url = "http://example.com" };
            // db.Blogs.Add(blog);
            // db.SaveChanges();
            return Ok();

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
