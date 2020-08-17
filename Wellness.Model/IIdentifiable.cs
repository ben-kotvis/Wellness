using System;

namespace Wellness.Model
{
    public interface IIdentifiable
    {
        Guid Id { get; set; }
    }
}
