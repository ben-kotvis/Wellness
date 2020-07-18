using AutoMapper;
using Wellness.Model;

namespace Wellness.Domain
{
    public class MappingConfigurator
    {
        public static IMapper Configure()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EventParticipation, PersistenceWrapper<EventParticipation>>()
                    .ForMember(i => i.Model, opt => opt.MapFrom(src => src));
                cfg.CreateMap<PersistenceWrapper<EventParticipation>, EventParticipation>();
                cfg.CreateMap<ActivityParticipation, PersistenceWrapper<ActivityParticipation>>()
                    .ForMember(i => i.Model, opt => opt.MapFrom(src => src));
                cfg.CreateMap<PersistenceWrapper<ActivityParticipation>, ActivityParticipation>();
                cfg.CreateMap<FrequentlyAskedQuestion, PersistenceWrapper<FrequentlyAskedQuestion>>()
                    .ForMember(i => i.Model, opt => opt.MapFrom(src => src));
                cfg.CreateMap<Activity, PersistenceWrapper<Activity>>()
                    .ForMember(i => i.Model, opt => opt.MapFrom(src => src));
                cfg.CreateMap<Activity, Activity>();
                cfg.CreateMap<Event, Event>();
                cfg.CreateMap<FrequentlyAskedQuestion, FrequentlyAskedQuestion>();
                cfg.CreateMap<EventParticipation, EventParticipation>();
                cfg.CreateMap<ActivityParticipation, ActivityParticipation>();
            });
            return configuration.CreateMapper();
        }

    }
}
