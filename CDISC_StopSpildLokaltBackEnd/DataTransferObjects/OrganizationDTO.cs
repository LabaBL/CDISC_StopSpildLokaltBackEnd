using System;
using System.ComponentModel.DataAnnotations;

namespace CDISC_StopSpildLokaltBackEnd {
    public class OrganizationDTO {
        public OrganizationDTO() {
        }

        [StringLength(32)]
        public string Name { get; set; }
    }
}
