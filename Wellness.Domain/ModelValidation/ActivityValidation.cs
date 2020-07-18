using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Domain.ModelValidation
{
    public class ActivityValidation : AbstractValidator<Activity>
    {
        private readonly IPersistanceReaderService<Activity> _activityManagementService;
        public ActivityValidation(IPersistanceReaderService<Activity> activityManagementService)
        {
            _activityManagementService = activityManagementService;
            RuleFor(e => e.Name).NotEmpty().MaximumLength(50).WithMessage("Please provide a value that is less than 50 characters");
            RuleFor(e => e.Name).MustAsync(ActivityNameIsUnique).WithMessage("Activity name must be unique");
        }

        private async Task<bool> ActivityNameIsUnique(string activityName, CancellationToken cancellationToken)
        {
            var events = await _activityManagementService.GetAll(cancellationToken);
            return !events.Any(i => i.Model.Name.ToLower() == activityName.ToLower());
        }
    }
}
