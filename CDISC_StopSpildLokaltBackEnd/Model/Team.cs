using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CDISC_StopSpildLokaltBackEnd {
    public class Team {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedTs { get; set; }
        public string Postcode { get; set; }
        public string Address { get; set; }
        public string TeamName { get; set; }
        public string Description { get; set; }
        public string FacebookUrl { get; set; }

        public Organization Organization { get; set; }
        public Volunteer ContactPerson { get; set; }
        public List<Volunteer> Volunteers { get; set; }
    }
}
