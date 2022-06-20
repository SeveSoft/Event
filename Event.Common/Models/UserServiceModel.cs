using Event.Common.Enum;

namespace Event.Common.Models
{
    public class UserServiceModel
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public string IdentityName { get; set; }
        public UserRole UserRole { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
