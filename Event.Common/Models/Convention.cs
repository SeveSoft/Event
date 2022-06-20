using System.Collections.Generic;

namespace Event.Common.Models
{
    public class Convention
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Venue> Venues { get; set; }
    }
}
