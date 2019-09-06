using App.Domain.Features.Places;
using App.Domain.Interfaces.Places;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Application.Features.Places
{
    public class LocationService : ILocationService
    {
        private ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public IList<Location> GetAll()
        {
            return _locationRepository.GetAll();
        }

        public Location GetByVenueId(int venueId)
        {
            return _locationRepository.GetByVenueId(venueId);
        }

        public Location Insert(Location entity)
        {
            return _locationRepository.Insert(entity);
        }

    }
}
