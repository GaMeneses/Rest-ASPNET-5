using RestASPNET.HyperMedia.Abstract;
using System.Collections.Generic;

namespace RestASPNET.HyperMedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
