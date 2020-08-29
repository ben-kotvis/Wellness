using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Domain.ModelValidation
{
    public class EventValidation : AbstractValidator<Event>
    {
        private readonly IPersistanceReaderService<Event> _eventManagementService;
        private readonly IRequestDependencies<Event> _requestDependencies;

        public EventValidation(IPersistanceReaderService<Event> eventManagementService, IRequestDependencies<Event> requestDependencies)
        {
            _requestDependencies = requestDependencies;
            _eventManagementService = eventManagementService;
            RuleFor(e => e.Name).NotEmpty().MaximumLength(50).WithMessage("Please provide a value that is less than 50 characters");
            RuleFor(e => e.Name).MustAsync(EventNameIsUnique).WithMessage("Event name must be unique");
            RuleFor(e => e.Points).GreaterThanOrEqualTo(5).LessThanOrEqualTo(300);
            RuleFor(e => e.AnnualMaximum).GreaterThanOrEqualTo(e => e.Points).WithMessage("Annual maximum must be greater than or eqaul to than the points for the single event.");
        }

        private async Task<bool> EventNameIsUnique(Event eventObj, string eventName, CancellationToken cancellationToken)
        {
            var events = await _eventManagementService.GetAll(_requestDependencies.CompanyId, cancellationToken);
            return !events.Any(i => i.Model.Id != eventObj.Id && i.Model.Name.ToLower() == eventName.ToLower());
        }
    }
}
