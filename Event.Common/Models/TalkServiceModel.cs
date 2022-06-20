namespace Event.Common.Models
{
    public class TalkServiceModel
    {
        public int ConventionId { get; set; }
        public int VenueId { get; set; }
        public int UserId { get; set; }
        public string Topic { get; set; }
        public string Info { get; set; }
    }
}
