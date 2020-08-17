using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IClientNotifier
    {
        Task SendNotification(string message);
    }
}
