using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IClientState
    {
        User CurrentUser { get; set; }
    }
}
