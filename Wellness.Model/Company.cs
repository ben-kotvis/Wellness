using System;

namespace Wellness.Model
{
    public class Company : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
