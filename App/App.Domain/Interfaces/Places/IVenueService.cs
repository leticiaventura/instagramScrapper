using App.Domain.Features.Places;
using App.Domain.Interfaces.Base;
using App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Interfaces.Places
{
    public interface IVenueService : IService<Venue>
    {
        Venue GetByFoursquareId(string foursquareId);
        int InsertSimilariry(Venue a, VenueSimilarityDTO similar);        
        IList<VenueSimilarityDTO> GetVenuesWithSimilarName(Venue venue);
        void DeleteById(Venue venue);
        void InsertVisitors(Venue venue);
    }
}
