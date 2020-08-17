using AutoMapper;
using Wellness.Model;

namespace Wellness.Domain
{
    public class Mapping : IMap
    {
        private readonly IMapper _mapper;
        public Mapping(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            _mapper.Map(source, destination);
        }
    }
}
