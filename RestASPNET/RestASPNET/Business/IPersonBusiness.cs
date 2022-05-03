using RestASPNET.Data.VO;
using System.Collections.Generic;

namespace RestASPNET.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindByID(long id);
        PersonVO Disable(long id);
        void Delete(long id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO person);
    }
}
