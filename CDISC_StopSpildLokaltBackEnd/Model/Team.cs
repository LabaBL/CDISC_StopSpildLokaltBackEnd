using System;
using System.Collections.Generic;

namespace CDISC_StopSpildLokaltBackEnd {
    public class Team {
        public Guid Id { get; set; }
        public Volunteer ContactPerson { get; set; }
        public DateTime CreatedTs { get; set; }
        public string Postcode { get; set; }
        public string TeamName { get; set; }
        public string Description { get; set; }
        public string FacebookUrl { get; set; }

        public List<Volunteer> Volunteers { get; set; }
    }
}
