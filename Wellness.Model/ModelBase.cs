using System;

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
