using System;
using System.ComponentModel.DataAnnotations;

namespace CDISC_StopSpildLokaltBackEnd {
    public class VolunteerDTO {
        public VolunteerDTO() {
        }

        public string AuthToken { get; set; }
        [RegularExpression(@"\D*")]
        public string FirstName { get; set; }
        [RegularExpression(@"\D*")]
        public string LastName { get; set; }
        [RegularExpression(@"\d{4}")]
        [StringLength(4, ErrorMessage = "Postcodes must have a length of 4 digits.")]
        public string Postcode { get; set; }
        [StringLength(8)]
        public string Phonenumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public VolunteerType VolunteerType { get; set; }
        [StringLength(32)]
        public string OrganizationName { get; set; }
    }
}
