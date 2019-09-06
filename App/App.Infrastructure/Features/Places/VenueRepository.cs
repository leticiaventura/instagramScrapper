using App.Domain.Features.Places;
using App.Domain.Features.Users;
using App.Domain.Interfaces.Places;
using App.Domain.Models;
using App.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Features.Places
{
    public class VenueRepository : IVenueRepository
    {
        private readonly string _sqlSelectAll = @"SELECT Id, Name, Foursquare_Id FROM TBVenue";
        private readonly string _sqlSelectByFoursquareId = @"SELECT Id, Name, Foursquare_Id FROM TBVenue WHERE Foursquare_Id = @Foursquare_Id";
        private readonly string _sqlInsert = @"INSERT INTO TBVenue (Name, Foursquare_Id) VALUES (@Name, @Foursquare_Id)";
        private readonly string _sqlInsertCategory = @"INSERT INTO TBVenue_TBCategory (Venue_id, Category_id) VALUES (@Venue_id, @Category_id)";
        private readonly string _sqlInsertVisitor = @"INSERT INTO TBVenue_TBInstagramUser (Venue_id, InstagramUser_id) VALUES (@Venue_id, @InstagramUser_id)";
        private readonly string _sqlInsertSimilarity = @"INSERT INTO TBVenue_Similarity (Venue_1, Venue_2, Similarity, Distance) VALUES (@Venue_1, @Venue_2, @Similarity, @Distance)";
        private readonly string _sqlGetVenuesWithSimilarName = @"SELECT s.id, s.similarity, v2.id as venue_id, v2.name, l.id as location_id, l.lat, l.lng FROM TBVenue v
	                                                                    inner join TBVenue_Similarity s on v.id = s.venue_1
	                                                                    inner join TBVenue v2 on s.venue_2 = v2.id 
	                                                                    inner join TBLocation l on v2.location_id = l.id
	                                                                    where v.id = @id or v2.id = @id
	                                                                    order by similarity asc";
        private readonly string _deleteById = @"DELETE FROM TBVenue_TBCategory WHERE Venue_Id = @Id;
                                                DELETE FROM TBLocation WHERE Venue_Id = @Id;
                                                DELETE FROM TBVenue WHERE Id = @Id";

        public IList<Venue> GetAll()
        {
            return Db.GetAll(_sqlSelectAll, Make);
        }

        public Venue Insert(Venue entity)
        {
            entity.Id = Db.Insert(_sqlInsert, CreateParams(entity));
            return entity;
        }

        public int InsertCategory(Category category, Venue venue)
        {
            return Db.Insert(_sqlInsertCategory, CreateParams(category, venue));
        }

        public Venue GetByFoursquareId(string foursquareId)
        {
            return Db.Get(_sqlSelectByFoursquareId, Make, new object[] { "@Foursquare_Id", foursquareId });
        }

        public int InsertSimilarity(Venue a, VenueSimilarityDTO similar)
        {
            return Db.Insert(_sqlInsertSimilarity, CreateParams(a, similar));
        }

        public int InsertVisitor(InstagramUser visitor, Venue venue)
        {
            return Db.Insert(_sqlInsertSimilarity, CreateParams(visitor, venue));
        }

        public IList<VenueSimilarityDTO> GetVenuesWithSimilarName(Venue venue)
        {
            return Db.GetAll(_sqlGetVenuesWithSimilarName, MakeVenueSimilarityDTO, new object[] { "@Id", venue.Id });
        }

        public void DeleteById(Venue venue)
        {
            Db.Delete(_deleteById, new object[] { "@Id", venue.Id });
        }

        private object[] CreateParams(Venue venue)
        {
            return new object[]
            {
                "@Id", venue.Id,
                "@Name", venue.Name,
                "@Foursquare_Id", venue.FoursquareId
            };
        }

        private object[] CreateParams(Category category, Venue venue)
        {
            return new object[]
            {
                "@Category_Id", category.Id,
                "@Venue_Id", venue.Id
            };
        }

        private object[] CreateParams(InstagramUser instagramUser, Venue venue)
        {
            return new object[]
            {
                "@Instagram_Id", instagramUser.Id,
                "@Venue_Id", venue.Id
            };
        }

        private object[] CreateParams(Venue a, VenueSimilarityDTO similar)
        {
            return new object[]
            {
                "@Venue_1", a.Id,
                "@Venue_2", similar.Venue.Id,
                "@Similarity", similar.Similarity,
                "@Distance", similar.Distance
            };
        }

        private static Func<IDataReader, Venue> Make = reader =>
           new Venue
           {
               Id = Convert.ToInt32(reader["Id"]),
               Name = Convert.ToString(reader["Name"]),
               FoursquareId = Convert.ToString(reader["Foursquare_Id"])
           };

        private static Func<IDataReader, VenueSimilarityDTO> MakeVenueSimilarityDTO = reader =>
           new VenueSimilarityDTO
           {
               Id = Convert.ToInt32(reader["Id"]),
               Similarity = Convert.ToDecimal(reader["Similarity"]),
               Venue = new Venue
               {
                   Id = Convert.ToInt32(reader["Venue_Id"]),
                   Name = Convert.ToString(reader["Name"]),
                   Location = new Location
                   {
                       Id = Convert.ToInt32(reader["Location_Id"]),
                       Lat = Convert.ToString(reader["Lat"]),
                       Lng = Convert.ToString(reader["Lng"])
                   }
               }
           };
    }
}
