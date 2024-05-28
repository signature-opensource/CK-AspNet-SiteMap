using CK.Core;
using CK.Cris;
using CK.IO.AspNet.SiteMap;
using CK.SqlServer;
using System.Linq;
using System.Threading.Tasks;

namespace CK.AspNet.SiteMap
{
    public class SiteMapCommandHandler : IScopedAutoService
    {

        [CommandHandler]
        public async Task<ISiteMap> GetSiteMapAsync( ISqlCallContext ctx,
                                                     ISiteMapQCommand cmd,
                                                     SiteMapQueries queries )
        {
            var siteMap = await queries.GetUserSiteMapAsync( ctx, cmd.ActorId );
            var home = await queries.GetPreferredWorkspacePagePathAsync( ctx, cmd.ActorId, cmd.ActorId );
            return cmd.CreateResult( s =>
            {
                s.Home = home;
                s.Pages.AddRange( siteMap );
                s.Pov.AddRange( siteMap.Where( i => i.Path.IndexOf( '$' ) == i.Path.LastIndexOf( '/' ) + 1 ) // Is a POV page
                                       .Select( i => (i.Path.Substring( i.Path.LastIndexOf( '/' ) + 1 ), i.PageTitle) )
                                       .DistinctBy( i => i.Item1 ) );
            } );
        }
    }
}
