using CK.Core;
using CK.StObj.TypeScript;
using System.Collections.Generic;

namespace CK.IO.AspNet.SiteMap
{
    [TypeScript]
    public interface ISiteMap : IPoco
    {
        public string Home { get; set; }

        public IList<(string Path, string Name)> Pages { get; }

        public IList<(string Path, string Name)> Pov { get; }
    }
}
