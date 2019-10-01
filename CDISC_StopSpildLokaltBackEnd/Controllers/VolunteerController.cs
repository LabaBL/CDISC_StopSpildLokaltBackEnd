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

        private readonly OrganizationalDBContext _context;

        public VolunteerController(OrganizationalDBContext context) {
            _context = context;
        }

        [HttpGet("/volunteers/team/{id}")]
        public IEnumerable<string> GetTeamVolunteers(int id) {
            throw new NotImplementedException();
        }

        [HttpGet("/identification/{identifcationId}")]
        public async Task<Identification> Identification(int identifcationId) {
            //TODO How is an Identification updated? (IDEA: Thread created by Cron job that updates all Identifications daily)
            var volunteer = await _context.Volunteers.Where(v => v.Id == identifcationId).FirstOrDefaultAsync<Volunteer>();
            return volunteer.Identification;    
        }

        [HttpGet("{id}")]
        public async Task<Volunteer> Get(int id) {
            return await _context.Volunteers.Where(v => v.Id == id).FirstOrDefaultAsync<Volunteer>();    //.First  SingleOrDefaultAsync(v => v.Id.Equals(id));
        }

        [HttpPost]
        public async Task<int> Post([FromBody]Volunteer volunteer) {
            volunteer.CreatedTs = DateTime.Now;
            await _context.AddAsync(volunteer);
            await _context.SaveChangesAsync();
            return volunteer.Id; //TODO Test that ID been set here
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]Volunteer volunteer) {
             _context.Update(volunteer);
            await _context.SaveChangesAsync();
        }
    }
}
