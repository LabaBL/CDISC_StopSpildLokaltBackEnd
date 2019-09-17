using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDISC_StopSpildLokaltBackEnd {
    [Route("api/[controller]")]
    public class VolunteerController : Controller {

        private readonly SSLContext _context;

        public VolunteerController(SSLContext context) {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get() {
            throw new NotImplementedException();
        }

        // GET api/<GUID>
        [HttpGet("{id}")]
        public async Task<Volunteer> Get(Guid id) {
#pragma warning disable CS1701 // Assuming assembly reference matches identity
            return await _context.Volunteers.Where(v => v.Id.Equals(id)).FirstOrDefaultAsync<Volunteer>();    //.First  SingleOrDefaultAsync(v => v.Id.Equals(id));
#pragma warning restore CS1701 // Assuming assembly reference matches identity
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value) {
            throw new NotImplementedException();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value) {
            throw new NotImplementedException();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
            throw new NotImplementedException();
        }
    }
}
