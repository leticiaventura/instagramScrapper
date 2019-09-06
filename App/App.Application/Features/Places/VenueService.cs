using App.Domain.Features.Places;
using App.Domain.Features.Users;
using App.Domain.Interfaces.Places;
using App.Domain.Interfaces.Users;
using App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Features.Places
{
    public class VenueService : IVenueService
    {
        private IVenueRepository _venueRepository;
        private ILocationRepository _locationRepository;
        private ICategoryRepository _categoryRepository;
        private IInstagramUserRepository _instagramUserRepository;

        public VenueService(IVenueRepository venueRepository, ILocationRepository locationRepository, ICategoryRepository categoryRepository, IInstagramUserRepository instagramUserRepository)
        {
            _venueRepository = venueRepository;
            _locationRepository = locationRepository;
            _categoryRepository = categoryRepository;
            _instagramUserRepository = instagramUserRepository;
        }

        public void DeleteById(Venue venue)
        {
            _venueRepository.DeleteById(venue);
        }

        public IList<Venue> GetAll()
        {
            return _venueRepository.GetAll();
        }

        public Venue GetByFoursquareId(string foursquareId)
        {
            return _venueRepository.GetByFoursquareId(foursquareId);
        }

        public IList<VenueSimilarityDTO> GetVenuesWithSimilarName(Venue venue)
        {
            return _venueRepository.GetVenuesWithSimilarName(venue);
        }

        public Venue Insert(Venue entity)
        {
            if (GetByFoursquareId(entity.FoursquareId) == null)
            {                
                entity = _venueRepository.Insert(entity);
                entity.Location.VenueId = entity.Id;
                entity.Location = _locationRepository.Insert(entity.Location);

                Category insertedCategory;
                foreach (var category in entity.Categories)
                {
                    var storedCategory = _categoryRepository.GetByFoursquareId(category.FoursquareId);
                    if (storedCategory == null)
                    {
                        insertedCategory = _categoryRepository.Insert(category);
                        _venueRepository.InsertCategory(insertedCategory, entity);
                    }
                    else
                    {
                        _venueRepository.InsertCategory(storedCategory, entity);
                    }
                }
            }
            return entity;
        }

        public int InsertSimilariry(Venue a, VenueSimilarityDTO similar)
        {
            return _venueRepository.InsertSimilarity(a, similar);
        }

        public void InsertVisitors(Venue entity)
        {
            InstagramUser insertedInstagramUser;
            foreach (var instagramUser in entity.Visitors)
            {
                var storedInstagramUser = _instagramUserRepository.GetByInstagramId(instagramUser.InstagramId);
                if (storedInstagramUser == null)
                {
                    insertedInstagramUser = _instagramUserRepository.Insert(instagramUser);
                    _venueRepository.InsertVisitor(insertedInstagramUser, entity);
                }
                else
                {
                    _venueRepository.InsertVisitor(storedInstagramUser, entity);
                }
            }
        }
    }
}
