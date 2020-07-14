using System;
using System.Collections.Generic;
using System.Text;

namespace Wellness.Model
{
    public interface IMap
    {
        void Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
