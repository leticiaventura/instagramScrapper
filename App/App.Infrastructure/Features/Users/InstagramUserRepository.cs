using App.Domain.Features.Users;
using App.Domain.Interfaces.Users;
using App.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Data;

namespace App.Infrastructure.Features.Users
{
    public class InstagramUserRepository : IInstagramUserRepository
    {
        private readonly string _sqlSelectAll = @"SELECT Id, Instagram_Id, Username FROM TBInstagramUser";
        private readonly string _sqlSelectByInstagramId = @"SELECT Id, Username, Instagram_Id FROM TBInstagramUser WHERE Instagram_Id = @Instagram_Id";
        private readonly string _sqlInsert = @"INSERT INTO TBInstagramUser (Username, Instagram_Id) VALUES (@Username, @Instagram_Id)";

        public IList<InstagramUser> GetAll()
        {
            return Db.GetAll(_sqlSelectAll, Make);
        }

        public InstagramUser GetByInstagramId(long instagramId)
        {
            return Db.Get(_sqlSelectByInstagramId, Make, new object[] { "@Instagram_Id", instagramId });
        }

        public InstagramUser Insert(InstagramUser entity)
        {
            entity.Id = Db.Insert(_sqlInsert, CreateParams(entity));
            return entity;
        }

        private object[] CreateParams(InstagramUser entity)
        {
            return new object[]
            {
                "@Id", entity.Id,
                "@Username", entity.Username,
                "@Instagram_Id", entity.InstagramId
            };
        }

        private static Func<IDataReader, InstagramUser> Make = reader =>
           new InstagramUser
           {
               Id = Convert.ToInt32(reader["Id"]),
               Username = Convert.ToString(reader["Username"]),
               InstagramId = Convert.ToInt64(reader["Instagram_Id"])
           };
    }
}
