using System.Collections.Generic;
using System.Threading.Tasks;
using Event.Common.Models;

namespace Event.Web.Services
{
    public interface IEventDataService
    {
        Task<List<Topic>> GetSpeakerTopicsAsync();
        Task<List<Venue>> GetVenuesAsync();
        Task<List<Convention>> GetConventionsAsync();
        Task AddVenueToConventionAsync(Venue venue, int conventionId);
        Task<int> CreateConventionAsync(Convention convention);
        Task<UserServiceModel> RegisterUserAsync(UserServiceModel user);
        Task<UserServiceModel> GetUserAsync(UserServiceModel user);
        Task SignUpForConventionAsync(SignUpServiceModel model);
        Task RegisterTalkAsync(TalkServiceModel model);
        Task<List<Convention>> GetMyConventionsAsync(UserServiceModel model);
        Task RegisterSeatAsync(RegisterSeatServiceModel registerSeatServiceModel);
    }
}
