using App.Domain.Features.Users;
using App.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Interfaces.Users
{
    public interface IInstagramUserRepository : IRepository<InstagramUser>
    {
        InstagramUser GetByInstagramId(long instagramId);
    }
}
