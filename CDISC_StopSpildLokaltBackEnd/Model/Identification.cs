using System;
namespace CDISC_StopSpildLokaltBackEnd {
    public class Identification {
        public Guid Id { get; set; }
        public Volunteer Volunteer { get; set; }
        public DateTime CreatedTs { get; set; }
    }
}
