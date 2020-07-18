namespace Wellness.Model
{
    public interface IMap
    {
        void Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
