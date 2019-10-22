using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        [HttpGet("/identification/{identificationId}")]
        public async Task<IActionResult> Identification(int identifcationId) {
            var volunteer = await _context.Volunteers.Where(v => v.Id == identifcationId).FirstOrDefaultAsync<Volunteer>();
            if (volunteer == null) return NotFound();

            return Json(await _context.Identifications.FindAsync(volunteer.IdentificationId));
        }

        [HttpGet("{id}")]
        public async Task<Volunteer> Get(int id) {
            return await _context.Volunteers.Where(v => v.Id == id).FirstOrDefaultAsync<Volunteer>();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]VolunteerDTO volunteerDTO) {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            Organization org = await _context.Organizations.Where(o => o.Name == volunteerDTO.OrganizationName).FirstOrDefaultAsync();
            if (!string.IsNullOrWhiteSpace(volunteerDTO.OrganizationName) && org == null) {
                return BadRequest("Specified organization does not exist");
            }

            var identification = new Identification {
                LastUpdatedTs = DateTime.Now,
                Active = true
            };

            await _context.AddAsync(identification); // Create Identification

            var volunteer = new Volunteer {
                CreatedTs = DateTime.Now,
                AuthToken = volunteerDTO.AuthToken,
                FirstName = volunteerDTO.FirstName,
                LastName = volunteerDTO.LastName,
                Postcode = volunteerDTO.Postcode,
                Phonenumber = volunteerDTO.Phonenumber,
                Email = volunteerDTO.Email,
                VolunteerType = volunteerDTO.VolunteerType,
                Organization = org,
                Identification = identification
            };

            await _context.AddAsync(volunteer); // Create Volunteer

            using (SHA256 sha256Hash = SHA256.Create()) {

                string stringToBeHashed = $"{identification.Id}{volunteerDTO.LastName}{volunteerDTO.Phonenumber}{volunteerDTO.Email}{volunteer.Id}";
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(stringToBeHashed));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++) {
                    builder.Append(bytes[i].ToString("x2"));
                }

                identification.UniqueToken = builder.ToString();
                identification.Volunteer = volunteer; // Tie Volunteer to Identification
                //_context.Update(identification); //TODO Is this necessary?
            }

            await _context.SaveChangesAsync();
            return Ok(volunteer.Id);
        }

        //[HttpPut("/{id}?volunteerType={volunteerType}")]
        [HttpPut("/{id}")]
        public async Task<IActionResult> UpdateVolunteerType(int id, string volunteerType) {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer == null) return BadRequest("Specified Volunteer does not exist.");

            VolunteerType parsedVolunteerType;
            var validVolunteerType = Enum.TryParse(volunteerType, false, out parsedVolunteerType);
            if(!validVolunteerType || !Enum.IsDefined(typeof(VolunteerType), parsedVolunteerType)) return BadRequest("Invalid VolunteerType specified.");

            volunteer.VolunteerType = parsedVolunteerType;
            _context.Update(volunteer);
            await _context.SaveChangesAsync();

            return Json(volunteer);
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
            //if (volunteerDTO.VolunteerType != null) volunteer.VolunteerType = volunteerDTO.VolunteerType;

            _context.Update(volunteer);
            await _context.SaveChangesAsync();

            return Json(volunteer);
        }
    }
}
