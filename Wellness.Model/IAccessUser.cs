using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Wellness.Model
{
    public interface IAccessUser
    {
        ClaimsPrincipal User { get; }
    }
}
