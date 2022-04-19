using Microsoft.AspNetCore.Mvc;
using RestASPNET.HyperMedia.Constants;
using RestASPNET.Data.VO;
using System.Text;
using System.Threading.Tasks;

namespace RestASPNET.HyperMedia.Enricher
{
    public class PersonEnricher : ContentResponseEnricher<PersonVO>
    {
        private readonly object _lock = new object();
        protected override Task EnrichModel(PersonVO content, IUrlHelper urlhelper)
        {
            var path = "api/person";
            string link = getLink(content.Id, urlhelper, path);

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPut
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = link,
                Rel = RelationType.self,
                Type = "int"
            });
            return Task.CompletedTask;
        }

        private string getLink(long id, IUrlHelper urlhelper, string path)
        {
            lock (_lock)
            {
                var url = new { Controller = path, id = id };
                return new StringBuilder(urlhelper.Link("DefaultApi", url)).Replace("%2F","/").ToString();
            }
        }
    }
}
