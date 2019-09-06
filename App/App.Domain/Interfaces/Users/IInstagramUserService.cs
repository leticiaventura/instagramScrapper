using App.Domain.Features.Users;
using App.Domain.Interfaces.Base;

namespace App.Domain.Interfaces.Users
{
    public interface IInstagramUserService : IService<InstagramUser>
    {
        InstagramUser GetByInstagramId(long instagramId);
    }
}
