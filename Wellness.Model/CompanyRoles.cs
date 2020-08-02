using System;
using System.Collections.Generic;

namespace Wellness.Model
{
    public class CompanyRoles 
    {
        public Guid CompanyId { get; set; }
        public List<string> Roles { get; set; }
    }
}
