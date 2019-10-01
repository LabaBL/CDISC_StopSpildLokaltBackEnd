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

        [HttpGet("{id}")]
        public async Task<Team> Get(int id) {
            return await _context.Teams.Where(t => t.Id == id).FirstOrDefaultAsync<Team>();
        }

        [HttpPost]
        public async Task<int> Post([FromBody]Team team) {
            var res = await _context.AddAsync(team); //TODO Procedurally generated id?

            team.CreatedTs = DateTime.Now;

            await _context.SaveChangesAsync();
            return team.Id;
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]Team team) {
            _context.Update(team);
            await _context.SaveChangesAsync();
        }
    }
}
