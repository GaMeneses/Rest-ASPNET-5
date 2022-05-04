using RestASPNET.Data.Converter.Implementations;
using RestASPNET.Data.VO;
using RestASPNET.HyperMedia.Utils;
using RestASPNET.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestASPNET.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;
        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {           
            var sort = (!string.IsNullOrWhiteSpace(sortDirection) 
                && !sortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase)) ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            StringBuilder query = new StringBuilder();
            query.AppendLine("select * from person p where 1 = 1");
            if(!string.IsNullOrWhiteSpace(name))
                query.AppendLine("and p.first_name like '%" + name + "%'");
            query.AppendLine("order by");
            query.AppendLine("p.first_name "+ sort +" limit "+ size +" offset " + offset);

            StringBuilder countQuery = new StringBuilder();
            countQuery.AppendLine("select count(*) from person p where 1 = 1");
            if (!string.IsNullOrWhiteSpace(name))
                countQuery.AppendLine("and p.first_name like '%" + name + "%'");


            var people = _repository.FindWithPagedSearch(query.ToString());
            int totalResults = _repository.GetCount(countQuery.ToString());
            return new PagedSearchVO<PersonVO> {
                CurrentPage = page,
                SearchResults = _converter.Parse(people),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }

        public PersonVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_repository.FindByName(firstName, lastName));
        }

        public PersonVO Create(PersonVO person)
        {   
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

        public PersonVO Disable(long id)
        {
            var personEntity = _repository.Disable(id);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }       
    }
}
