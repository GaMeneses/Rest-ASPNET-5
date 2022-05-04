using RestASPNET.Data.VO;
using RestASPNET.HyperMedia.Utils;
using System.Collections.Generic;

namespace RestASPNET.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindByID(long id);
        List<PersonVO> FindByName(string firstName, string lastName);
        PersonVO Disable(long id);
        void Delete(long id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO person);
        PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);

    }
}
