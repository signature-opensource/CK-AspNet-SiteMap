using CK.Auth;
using CK.Cris;
using CK.StObj.TypeScript;

namespace CK.AspNet.SiteMap
{
    [TypeScript]
    public interface ISiteMapQCommand : ICommand<ISiteMap>, ICommandAuthUnsafe
    {
    }
}
