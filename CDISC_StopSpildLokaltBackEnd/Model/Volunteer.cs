using System;
using System.ComponentModel.DataAnnotations;

namespace CDISC_StopSpildLokaltBackEnd {
    public class Volunteer {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedTs { get; set; }
        public string AuthToken { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //[Required, StringLength(4, ErrorMessage = "Postcodes must have a length of 4 digits.")]
        public string Postcode { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public VolunteerType VolunteerType { get; set; }

        public Organization Organization { get; set; }

        public int IdentificationId { get; set; }
        public Identification Identification { get; set; }
    }

    public enum VolunteerType { VOLUNTEER, PASSIVE, CONTACT_PERSON, TRIAL }
}
