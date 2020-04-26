using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class PersistenceWrapper<T> : IHaveCommon
        where T : IIdentifiable 
    {
        public T Model { get; set; }
        public Common Common { get; set; }
    }

}
