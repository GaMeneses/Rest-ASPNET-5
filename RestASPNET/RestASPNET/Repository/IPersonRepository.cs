using RestASPNET.Model;

namespace RestASPNET.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);
    }
}
