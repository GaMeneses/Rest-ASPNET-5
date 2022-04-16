using RestASPNET.Model;
using System.Collections.Generic;

namespace RestASPNET.Repository
{
    public interface IPersonRepository
    {
        Person Create(Person person);
        Person FindByID(long id);
        void Delete(long id);
        List<Person> FindAll();
        Person Update(Person person);
        bool Exits(long id);
    }
}
