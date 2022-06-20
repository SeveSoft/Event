using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Event.Common.Models;

namespace Event.Web.Models
{
    public class RegisterTalkModel
    {
        public List<Convention> MyConventions { get; set; }
        public List<Topic> Topics { get; set; }
        
        [Required]
        public int SelectedConventionId { get; set; }
        
        [Required]
        public int SelectedVenueId { get; set; }
        
        [Required]
        public int SelectedTopicId { get; set; }
        
        [Required]
        public string Info { get; set; }
    }
}
