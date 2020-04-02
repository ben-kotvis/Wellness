using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public abstract class ModelBase : IHaveCommon, IIdentifiable
    {
        protected ModelBase()
        {
        }

        public Guid Id { get; set; }
        public Common Common { get; set; }
    }
}
