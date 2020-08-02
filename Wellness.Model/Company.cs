using System;
using System.Collections.Generic;

namespace Wellness.Model
{
    public class Company : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AdministrativeRole { get; set; }
        public string InviteeRole { get; set; }
        public string StandardUserRole { get; set; }
    }
}
