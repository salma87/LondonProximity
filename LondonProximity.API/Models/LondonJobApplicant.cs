using System;
using System.ComponentModel.DataAnnotations;

namespace LondonProximity.API
{
    public class LondonJobApplicant
    {
        [Required]
        public long ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
    }
}
