using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDISC_StopSpildLokaltBackEnd {
    [Route("api/[controller]")]
    public class OrganizationController : Controller {

        private readonly OrganizationalDBContext _context;

        public OrganizationController(OrganizationalDBContext context) {
            _context = context;
        }

        // GET: api/organizations
        [HttpGet]
        public async Task<IEnumerable<Organization>> Get() {
            return await Task.FromResult<IEnumerable<Organization>>(_context.Organizations);
        }

        // GET api/organization/<ID>
        [HttpGet("{id}")]
        public async Task<Organization> Get(int id) {
            return await _context.Organizations.Where(t => t.Id == id).FirstOrDefaultAsync<Organization>();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Organization value) {
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
