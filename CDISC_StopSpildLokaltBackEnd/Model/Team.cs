using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CDISC_StopSpildLokaltBackEnd {
    public class Team {
        [Key]
        public int Id { get; set; }
        [Required]
        public Volunteer ContactPerson { get; set; }
        [Required]
        public Organization Organization { get; set; }
        public DateTime CreatedTs { get; set; }
        [Required, StringLength(4, ErrorMessage = "Postcodes must have a length of 4 digits.")]
        public string Postcode { get; set; }
        [Required]
        public string TeamName { get; set; }
        public string Description { get; set; }
        public string FacebookUrl { get; set; }

        public List<Volunteer> Volunteers { get; set; }
    }
}
