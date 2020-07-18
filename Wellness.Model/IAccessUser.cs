using System.Security.Claims;

namespace Wellness.Model
{
    public interface IAccessUser
    {
        ClaimsPrincipal User { get; }
    }
}
