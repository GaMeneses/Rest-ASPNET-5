using System.Collections.Generic;

namespace RestASPNET.HyperMedia.Abstract
{
    public interface ISupportsHypermedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}
