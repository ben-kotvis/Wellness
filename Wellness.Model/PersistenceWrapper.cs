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
        public PersistenceWrapper(T model, Common common)
        {
            Model = model;
            Common = common;
        }

        public PersistenceWrapper()
        {

        }

        public T Model { get; set; }
        public Common Common { get; set; }
    }

}
