namespace Event.Common.Models
{
    public class Talk
    {
        public int Id { get; set; }

        public string Topic { get; set; }

        public string Info { get; set; }

        public string Speaker { get; set; }
        public bool HasReservedSeat { get; set; }
    }
}
