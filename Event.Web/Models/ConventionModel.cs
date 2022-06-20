using System.Collections.Generic;
using Event.Common.Models;

namespace Event.Web.Models
{
    public class ConventionModel
    {
        public Convention SelectedConvention { get; set; }
        public List<Venue> Venues { get; set; }
        public List<Convention> Conventions { get; set; }
    }
}
