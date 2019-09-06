using App.Domain.Features.Base;
using System;
using System.Collections.Generic;
using System.Data;

namespace App.Domain.Interfaces.Base
{
    public interface IRepository<T> where T : Entity
    {
        T Insert(T entity);
        IList<T> GetAll();
    }
}
