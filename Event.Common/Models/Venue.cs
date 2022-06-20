using System.Collections.Generic;

namespace Event.Common.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string VenueId { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Name { get; set; }
        public List<Talk> Talks { get; set; }
    }
}
