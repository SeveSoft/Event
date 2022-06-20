using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Event.Common.Models;

namespace Event.Web.Models
{
    public class ReserveSeatModel
    {
        public List<Convention> MyConventions { get; set; }

        [Required]
        public int SelectedConventionId { get; set; }

        [Required]
        public int SelectedVenueId { get; set; }

        [Required]
        public int SelectedTalkId { get; set; }
        public bool IsTrue => true;
        [Required]
        [Compare(nameof(IsTrue))]
        public bool HasAcceptedTermsAndConditions { get; set; }
    }
}
