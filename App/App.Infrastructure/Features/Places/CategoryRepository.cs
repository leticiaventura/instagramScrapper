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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _sqlSelectAll = @"SELECT Id, Name, Foursquare_Id FROM TBCategory";
        private readonly string _sqlSelectByFoursquareId = @"SELECT Id, Name, Foursquare_Id FROM TBCategory WHERE Foursquare_Id = @Foursquare_Id";
        private readonly string _sqlInsert = @"INSERT INTO TBCategory (Name, Foursquare_Id) VALUES (@Name, @Foursquare_Id)";

        public IList<Category> GetAll()
        {
            return Db.GetAll(_sqlSelectAll, Make);
        }

        public Category Insert(Category entity)
        {
            entity.Id = Db.Insert(_sqlInsert, CreateParams(entity));
            return entity;
        }

        public Category GetByFoursquareId(string foursquareId)
        {
            return Db.Get(_sqlSelectByFoursquareId, Make, new object[] { "@Foursquare_Id", foursquareId });
        }

        private object[] CreateParams(Category category)
        {
            return new object[]
            {
                "@Id", category.Id,
                "@Name", category.Name,
                "@Foursquare_Id", category.FoursquareId
            };
        }
        
        private static Func<IDataReader, Category> Make = reader =>
           new Category
           {
               Id = Convert.ToInt32(reader["Id"]),
               Name = Convert.ToString(reader["Name"]),
               FoursquareId = Convert.ToString(reader["Foursquare_Id"])
           };
    }
}
