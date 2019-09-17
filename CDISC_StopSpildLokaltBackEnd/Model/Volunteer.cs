using System;
namespace CDISC_StopSpildLokaltBackEnd {
    public class Volunteer {
        public Guid Id { get; set; }
        public DateTime CreatedTs { get; set; }
        public string firstName { get; set; }
        public string LastName { get; set; }
        public string Postcode { get; set; }
        public VolunteerType VolunteerType { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public Identification Identification { get; set; }
        public string AuthToken { get; set; }
    }

    public enum VolunteerType { VOLUNTEER, PASSIVE, CONTACT_PERSON, TRIAL }
}
