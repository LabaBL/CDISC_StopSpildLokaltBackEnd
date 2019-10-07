﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CDISC_StopSpildLokaltBackEnd {
    public class Identification {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedTs { get; set; }
        public Guid UniqueToken { get; set; }

        public Volunteer Volunteer { get; set; }
    }
}
