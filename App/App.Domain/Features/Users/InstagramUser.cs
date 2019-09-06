using App.Domain.Features.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Features.Users
{
    public class InstagramUser : Entity
    {
        public long InstagramId { get; set; }
        public string Username { get; set; }
        public IList<InstagramUser> Friends { get; set; }
    }
}
