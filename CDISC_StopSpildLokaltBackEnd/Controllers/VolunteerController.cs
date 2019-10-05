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
        public IEnumerable<Identification> GetTeamVolunteers(int id) {
            throw new NotImplementedException();
        }

        [HttpGet("/identification/{identifcationId}")]
        public async Task<IActionResult> Identification(int identifcationId) {
            //TODO How is an Identification updated? (IDEA: Thread created by Cron job that updates all Identifications daily)
            var volunteer = await _context.Volunteers.Where(v => v.Id == identifcationId).FirstOrDefaultAsync<Volunteer>();
            if (volunteer == null) return NotFound();

            return Json(volunteer.Identification); //TODO Is Identification fetched through link?
        }

        [HttpGet("{id}")]
        public async Task<Volunteer> Get(int id) {
            return await _context.Volunteers.Where(v => v.Id == id).FirstOrDefaultAsync<Volunteer>();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]VolunteerDTO volunteerDTO) {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            Organization org = await _context.Organizations.Where(o => o.Name == volunteerDTO.OrganizationName).FirstOrDefaultAsync();
            if (!String.IsNullOrWhiteSpace(volunteerDTO.OrganizationName) && org == null) {
                return BadRequest("Specified organization does not exist");
            }

            var volunteer = new Volunteer
            {
                CreatedTs = DateTime.Now,
                AuthToken = volunteerDTO.AuthToken,
                FirstName = volunteerDTO.FirstName,
                LastName = volunteerDTO.LastName,
                Postcode = volunteerDTO.Postcode,
                Phonenumber = volunteerDTO.Phonenumber,
                Email = volunteerDTO.Email,
                VolunteerType = volunteerDTO.VolunteerType,
                Organization = org
            };

            await _context.AddAsync(volunteer);
            await _context.SaveChangesAsync();
            return Ok(volunteer.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]VolunteerDTO volunteerDTO) {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer == null) return BadRequest("Specified Volunteer does not exist.");

            if(volunteerDTO.OrganizationName != null) {
                var org = await _context.Organizations.FindAsync(volunteerDTO.OrganizationName);
                if (org == null) return BadRequest("Specified organization does not exist");
                volunteer.Organization = org;
            }

            if (volunteerDTO.AuthToken != null) volunteer.AuthToken = volunteerDTO.AuthToken;
            if (volunteerDTO.FirstName != null) volunteer.FirstName = volunteerDTO.FirstName;
            if (volunteerDTO.LastName != null) volunteer.LastName = volunteerDTO.LastName;
            if (volunteerDTO.Postcode != null) volunteer.Postcode = volunteerDTO.Postcode;
            if (volunteerDTO.Phonenumber != null) volunteer.Phonenumber = volunteerDTO.Phonenumber;
            if (volunteerDTO.Email != null) volunteer.Email = volunteerDTO.Email;

            //TODO Fix this
            if (volunteerDTO.VolunteerType != null) volunteer.VolunteerType = volunteerDTO.VolunteerType;

            _context.Update(volunteer);
            await _context.SaveChangesAsync();

            return Json(volunteer);
        }
    }
}
