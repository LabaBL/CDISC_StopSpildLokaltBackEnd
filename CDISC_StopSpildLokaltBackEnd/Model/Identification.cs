using System;
using System.ComponentModel.DataAnnotations;

namespace CDISC_StopSpildLokaltBackEnd {
    public class Identification {
        [Key]
        public int Id { get; set; }
        public DateTime LastUpdatedTs { get; set; }
        public string UniqueToken { get; set; }
        public bool Active { get; set; }

        public Volunteer Volunteer { get; set; }
    }
}
