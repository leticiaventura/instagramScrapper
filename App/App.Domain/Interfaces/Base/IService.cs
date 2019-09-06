using App.Domain.Features.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Interfaces.Base
{
    public interface IService<T> where T : Entity
    {
        T Insert(T entity);
        IList<T> GetAll();
    }
}
