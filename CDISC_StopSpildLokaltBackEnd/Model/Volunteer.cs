using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CDISC_StopSpildLokaltBackEnd {
    public class Volunteer {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedTs { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, StringLength(4, ErrorMessage = "Postcodes must have a length of 4 digits.")]
        public string Postcode { get; set; }
        [Required]
        public VolunteerType VolunteerType { get; set; }
        [Required]
        public string Phonenumber { get; set; }
        [Required]
        public Organization Organization { get; set; }
        [Required]
        public string Email { get; set; }
        public Identification Identification { get; set; }
        public string AuthToken { get; set; }
    }

    public enum VolunteerType { VOLUNTEER, PASSIVE, CONTACT_PERSON, TRIAL }
}
