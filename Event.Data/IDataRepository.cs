using System.Collections.Generic;
using System.Threading;
using Event.Common.Models;

namespace Event.Data
{
    public interface IDataRepository
    {
        void AddVenueToConvention(AddVenueServiceModel model, CancellationToken httpContextRequestAborted);
        List<Convention> GetConventions(CancellationToken httpContextRequestAborted);
        int CreateConvention(string conventionName, CancellationToken httpContextRequestAborted);
        UserServiceModel RegisterUser(UserServiceModel model, CancellationToken httpContextRequestAborted);
        UserServiceModel GetUser(UserServiceModel model, CancellationToken httpContextRequestAborted);
        void SignUpForConvention(SignUpServiceModel model, CancellationToken httpContextRequestAborted);
        List<Convention> GetMyConventions(UserServiceModel user, CancellationToken httpContextRequestAborted);
        void CreateTalk(TalkServiceModel model, CancellationToken httpContextRequestAborted);
        void RegisterSeat(RegisterSeatServiceModel model, CancellationToken httpContextRequestAborted);
        
    }
}
