using CK.Auth;
using CK.Cris;
using CK.StObj.TypeScript;

namespace CK.IO.AspNet.SiteMap
{
    /// <summary>
    /// Gets the <see cref="ISiteMap"/>.
    /// </summary>
    [TypeScript]
    public interface ISiteMapQCommand : ICommand<ISiteMap>, ICommandAuthUnsafe
    {
    }
}
