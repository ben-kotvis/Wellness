using AutoMapper;
using Wellness.Model;

namespace Wellness.Authorization.Domain
{
    public class MappingConfigurator
    {
        public static IMapper Configure()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
            });
            return configuration.CreateMapper();
        }

    }
}
