﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class Activity : NamedEntity, IHaveCommon, IIdentifiable
    {        
        public bool Active { get; set; }
        public Common Common { get; set; }
    }
}
