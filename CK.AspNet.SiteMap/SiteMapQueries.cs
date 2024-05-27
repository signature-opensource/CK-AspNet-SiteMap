using CK.DB.HWorkspace;
using CK.Core;
using CK.SqlServer;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CK.AspNet.SiteMap
{
    public class SiteMapQueries : IAutoService
    {
        readonly WorkspaceTable _workspaceTable;

        public SiteMapQueries( WorkspaceTable workspaceTable )
        {
            _workspaceTable = workspaceTable;
        }

        public async Task<IEnumerable<(string Path, string PageTitle)>> GetUserSiteMapAsync( ISqlCallContext ctx, int userId )
        {
            return await ctx[_workspaceTable].QueryAsync<(string Path, string PageTitle)>(
                @"select substring( wp.ResPath, 3, len( wp.ResPath ) - 2 ) as [Path], wp.PageTitle
                  from CK.vWebPage wp
                  inner join CK.vAclActor aa on wp.AclId = aa.AclId and aa.ActorId = @UserId
                  where aa.GrantLevel >= 16
                      and wp.PageId > 0;",
                new { UserId = userId } );
        }

        public async Task<string> GetPreferredWorkspacePagePathAsync( ISqlCallContext ctx, int actorId, int userId )
        {
            return await ctx[_workspaceTable].QuerySingleOrDefaultAsync<string>(
                @"select substring( wp.ResPath, 3, len( wp.ResPath ) - 2 )
                  from CK.tUser u
                  inner join CK.tWorkspace w on u.PreferredWorkspaceId = w.WorkspaceId
                  inner join CK.vWebPage wp on w.PageId = wp.PageId
                  inner join CK.vAclActor aa on wp.AclId = aa.AclId and aa.ActorId = @ActorId
                  where u.UserId = @UserId
                      and aa.GrantLevel >= 16;",
                new { ActorId = actorId, UserId = userId } );
        }
    }
}
