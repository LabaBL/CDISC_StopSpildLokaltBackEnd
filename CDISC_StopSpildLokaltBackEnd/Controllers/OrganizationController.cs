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

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name) {
            var org = await _context.Organizations.FindAsync(name);
            if (org == null) return NotFound();

            return Json(org);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]OrganizationDTO organizationDTO) {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var org = await _context.Organizations.FindAsync(organizationDTO.Name);
            if (org != null) return BadRequest("Organization with the specified name already exists.");

            var organization = new Organization {
                CreatedTs = DateTime.Now,
                Name = organizationDTO.Name
            };

            await _context.AddAsync(organization);
            await _context.SaveChangesAsync();

            return Json(organization);
        }
    }
}
