using RestASPNET.Model;
using RestASPNET.Model.Base;
using System.Collections.Generic;

namespace RestASPNET.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindByID(long id);
        void Delete(long id);
        List<T> FindAll();
        T Update(T item);
        bool Exits(long id);
    }
}
