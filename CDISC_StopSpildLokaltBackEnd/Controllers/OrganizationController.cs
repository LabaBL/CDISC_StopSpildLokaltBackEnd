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

        [HttpGet("/organizations")]
        public async Task<IEnumerable<Organization>> Get() {
            return await Task.FromResult<IEnumerable<Organization>>(_context.Organizations);
        }

        [HttpGet("{id}")]
        public async Task<Organization> Get(int id) {
            return await _context.Organizations.Where(t => t.Id == id).FirstOrDefaultAsync<Organization>();
        }

        [HttpPost]
        public void Post([FromBody]Organization value) {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value) {
            throw new NotImplementedException();
        }
    }
}
