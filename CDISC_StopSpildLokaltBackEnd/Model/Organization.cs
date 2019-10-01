using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CDISC_StopSpildLokaltBackEnd {
    public class Organization {

        public Organization() {
         
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Volunteer> Volunteers { get; set; }
        public List<Team> Teams { get; set; }
    }
}
