using System;
using System.ComponentModel.DataAnnotations;

namespace CDISC_StopSpildLokaltBackEnd {
    public class TeamDTO {
        public TeamDTO() {
        }

        [RegularExpression(@"\d{4}")]
        [StringLength(4, ErrorMessage = "Postcodes must have a length of 4 digits.")]
        public string Postcode { get; set; }
        [StringLength(64)]
        public string Address { get; set; }
        [StringLength(32)]
        public string TeamName { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [Url]
        public string FacebookUrl { get; set; }
        [StringLength(32)]
        public string OrganizationName { get; set; }
    }
}
