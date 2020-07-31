using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Client.Services
{
    public class ClientState : IClientState
    {
        public User CurrentUser { get; set; }
    }
}
