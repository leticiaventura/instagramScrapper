using App.Domain.Features.Places;
using App.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Interfaces.Places
{
    public interface ICategoryService : IService<Category>
    {
        Category GetByFoursquareId(string foursquareId);
    }
}
