using CK.Core;
using CK.StObj.TypeScript;
using System.Collections.Generic;

namespace CK.AspNet.SiteMap
{
    [TypeScript]
    public interface ISiteMap : IPoco
    {
        public string Home { get; set; }

        public List<(string, string)> Pages { get; }

        public List<(string, string)> Pov { get; }
    }
}
