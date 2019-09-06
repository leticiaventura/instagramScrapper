using App.Domain.Features.Places;
using App.Domain.Features.Users;
using App.Domain.Interfaces.Base;
using App.Domain.Models;
using System.Collections.Generic;

namespace App.Domain.Interfaces.Places
{
    public interface IVenueRepository : IRepository<Venue>
    {
        int InsertCategory(Category category, Venue venue);
        int InsertSimilarity(Venue a, VenueSimilarityDTO similar);
        Venue GetByFoursquareId(string foursquareId);
        IList<VenueSimilarityDTO> GetVenuesWithSimilarName(Venue venue);
        void DeleteById(Venue venue);
        int InsertVisitor(InstagramUser visitor, Venue venue);
    }
}
