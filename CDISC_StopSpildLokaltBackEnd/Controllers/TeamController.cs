using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CDISC_StopSpildLokaltBackEnd {
    [Route("api/[controller]")]
    public class TeamController : Controller {

        private readonly OrganizationalDBContext _context;

        public TeamController(OrganizationalDBContext context) {
            _context = context;
        }

        [HttpGet("/teams")]
        public IEnumerable<Team> Get() {
            return _context.Teams;
        }

        [HttpGet("team/{id}")]
        public async Task<IActionResult> Get(int id) {
            var team = await _context.Teams.Where(t => t.Id == id).FirstOrDefaultAsync<Team>();
            if (team == null) return NotFound();

            return Json(team);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TeamDTO teamDTO) {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            Organization org = await _context.Organizations.Where(o => o.Name == teamDTO.OrganizationName).FirstOrDefaultAsync();
            if (!String.IsNullOrWhiteSpace(teamDTO.OrganizationName) && org == null) {
                return BadRequest("Specified organization does not exist");
            }

            var team = new Team {
                CreatedTs = DateTime.Now,
                Postcode = teamDTO.Postcode,
                Address = teamDTO.Address,
                TeamName = teamDTO.TeamName,
                Description = teamDTO.Description,
                FacebookUrl = teamDTO.FacebookUrl,
                Organization = org
            };

            await _context.AddAsync(team);
            await _context.SaveChangesAsync();
            return Ok(team.Id);
        }

        [HttpPut("/team/setContactPerson/{id}")]
        public async Task<IActionResult> UpdateContactPerson(int id, [FromBody]int volunteerId) {
            Volunteer contactPerson = await _context.Volunteers.Where(v => v.Id == volunteerId).FirstOrDefaultAsync();
            if (contactPerson == null) return BadRequest("No Volunteer exist with the specified id.");

            var team = await _context.Teams.FindAsync(id);
            if (team == null) return BadRequest("No Team exist with the specified id.");

            team.ContactPerson = contactPerson; 
            _context.Update(team);
            await _context.SaveChangesAsync();

            return Json(team);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]TeamDTO teamDTO) {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var team = await _context.Teams.FindAsync(id);
            if (team == null) return NotFound();

            if(teamDTO.OrganizationName != null) {
                var org = await _context.Organizations.FindAsync(teamDTO.OrganizationName);
                if (org == null) return BadRequest("Specified organization does not exist");
                team.Organization = org;
            }

            if (teamDTO.Postcode != null) team.Postcode = teamDTO.Postcode;
            if (teamDTO.Address != null) team.Address = teamDTO.Address;
            if (teamDTO.TeamName != null) team.TeamName = teamDTO.TeamName;
            if (teamDTO.Description != null) team.Description = teamDTO.Description;
            if (teamDTO.FacebookUrl != null) team.FacebookUrl = teamDTO.FacebookUrl;

            _context.Update(team);
            await _context.SaveChangesAsync();

            return Json(team);
        }
    }
}
