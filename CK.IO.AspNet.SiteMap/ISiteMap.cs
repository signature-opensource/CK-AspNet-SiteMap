using CK.Core;
using CK.StObj.TypeScript;
using System.Collections.Generic;

namespace CK.IO.AspNet.SiteMap
{
    /// <summary>
    /// Models a site map. This lists all the <see cref="Pages"/> based on the current user.
    /// </summary>
    [TypeScript]
    public interface ISiteMap : IPoco
    {
        /// <summary>
        /// Gets the path of the user's home.
        /// </summary>
        public string Home { get; set; }

        /// <summary>
        /// Gets the sitemap pages.
        /// </summary>
        public IList<(string Path, string Name)> Pages { get; }

        /// <summary>
        /// Gets the Point Of Views.
        /// The suffix starts with '$'.
        /// </summary>
        public IList<(string Suffix, string Name)> PointOfViews { get; }
    }
}
