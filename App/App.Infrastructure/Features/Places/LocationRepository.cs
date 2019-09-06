using App.Domain.Features.Places;
using App.Domain.Interfaces.Places;
using App.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Features.Places
{
    public class LocationRepository : ILocationRepository
    {
        private readonly string _sqlSelectAll = @"SELECT Id, Crosstreet, City, Country, Postalcode, State, Lat, Lng, Venue_Id FROM TBLocation";
        private readonly string _sqlInsert = @"INSERT INTO TBLocation (Crosstreet, City, Country, Postalcode, State, Lat, Lng, Venue_Id) VALUES (@Crosstreet, @City, @Country, @Postalcode, @State, @Lat, @Lng, @Venue_Id)";
        private readonly string _sqlGetByVenueId = @"SELECT Id, Crosstreet, City, Country, Postalcode, State, Lat, Lng, Venue_Id FROM TBLocation WHERE Venue_Id = @Venue_Id";

        public IList<Location> GetAll()
        {
            return Db.GetAll(_sqlSelectAll, Make);
        }

        public Location GetByVenueId(int venueId)
        {
            return Db.Get(_sqlGetByVenueId, Make, new object[] { "@Venue_Id", venueId });
        }

        public Location Insert(Location entity)
        {
            entity.Id = Db.Insert(_sqlInsert, CreateParams(entity));
            return entity;
        }

        private object[] CreateParams(Location location)
        {
            return new object[]
            {
                "@Id", location.Id,
                "@Crosstreet", location.Crosstreet,
                "@City", location.City,
                "@Country", location.Country,
                "@Postalcode", location.Postalcode,
                "@State", location.State,
                "@Lat", location.Lat,
                "@Lng", location.Lng,
                "@Venue_Id", location.VenueId
            };
        }

        private static Func<IDataReader, Location> Make = reader =>
           new Location
           {
               Id = Convert.ToInt32(reader["Id"]),
               Crosstreet = Convert.ToString(reader["Crosstreet"]),
               City = Convert.ToString(reader["City"]),
               Country = Convert.ToString(reader["Country"]),
               Postalcode = Convert.ToString(reader["Postalcode"]),
               State = Convert.ToString(reader["State"]),
               Lat = Convert.ToString(reader["Lat"]),
               Lng = Convert.ToString(reader["Lng"]),
               VenueId = Convert.ToInt32(reader["Venue_Id"])
           };
    }
}
