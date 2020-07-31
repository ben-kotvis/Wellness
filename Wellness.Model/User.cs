﻿using System;

namespace Wellness.Model
{
    public class User : IIdentifiable
    {
        public Guid Id { get; set; }
        public string ProviderObjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualTotal { get; set; }
        public decimal AveragePointsPerMonth { get; set; }
    }
}
