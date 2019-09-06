using App.Domain.Features.Users;
using App.Domain.Interfaces.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Features.Users
{
    public class InstagramUserService : IInstagramUserService
    {
        private IInstagramUserRepository _instagramUserRepository;

        public InstagramUserService(IInstagramUserRepository instagramUserRepository)
        {
            _instagramUserRepository = instagramUserRepository;
        }

        public IList<InstagramUser> GetAll()
        {
            return _instagramUserRepository.GetAll();
        }

        public InstagramUser GetByInstagramId(long instagramId)
        {
            return _instagramUserRepository.GetByInstagramId(instagramId);
        }

        public InstagramUser Insert(InstagramUser entity)
        {
            return _instagramUserRepository.Insert(entity);
        }
    }
}
