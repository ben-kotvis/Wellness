using System;
using System.Collections.Generic;

namespace Wellness.Model
{
    public class CompanyUserRoles : IIdentifiable
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
        public List<string> Roles { get; set; }
    }
}
