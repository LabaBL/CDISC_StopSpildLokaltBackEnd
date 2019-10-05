using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CDISC_StopSpildLokaltBackEnd {
    public class Organization {

        public Organization() {
         
        }

        //public int Id { get; set; }
        [Key]
        public string Name { get; set; }
        public DateTime CreatedTs { get; set; }

        public List<Volunteer> Volunteers { get; set; }
        public List<Team> Teams { get; set; }
    }
}
