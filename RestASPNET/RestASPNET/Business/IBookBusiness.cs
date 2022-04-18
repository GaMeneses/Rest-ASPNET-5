using RestASPNET.Model;
using System.Collections.Generic;

namespace RestASPNET.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO book);
        BookVO FindByID(long id);
        void Delete(long id);
        List<BookVO> FindAll();
        BookVO Update(BookVO book);
    }
}
