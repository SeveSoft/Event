using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Event.Common.Enum;
using Event.Common.Models;

namespace Event.Web.Models
{
    public class SignUpModel
    {
        public List<Convention> Conventions { get; set; }
        [Required]
        public int SelectedConventionId { get; set; }
        [Required]
        public int SelectedVenueId { get; set; }
        [Required]
        public UserRole SelectedUserRole { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }

        public bool IsTrue => true;
        [Required]
        [Compare(nameof(IsTrue))]
        public bool HasAcceptedTermsAndConditions { get; set; }
    }
}
