using System.Collections.Generic;
using Event.Common.Models;

namespace Event.Web.Models
{
    public class HomeModel
    {
        public List<Convention> UpcomingConventions { get; set; }
        public UserServiceModel User { get; set; }
    }
}
