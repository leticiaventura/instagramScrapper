using App.Domain.Features.Places;
using App.Domain.Interfaces.Base;

namespace App.Domain.Interfaces.Places
{
    public interface ILocationService : IService<Location>
    {
        Location GetByVenueId(int id);
    }
}
