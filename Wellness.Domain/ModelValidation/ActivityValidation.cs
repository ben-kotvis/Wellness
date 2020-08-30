using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Domain.ModelValidation
{
    public class ActivityValidation : AbstractValidator<Activity>
    {
        private readonly IReaderService<Activity> _activityManagementService;
        public ActivityValidation(IReaderService<Activity> activityManagementService)
        {
            _activityManagementService = activityManagementService;
            RuleFor(e => e.Name).NotEmpty().MaximumLength(50).WithMessage("Please provide a value that is less than 50 characters");
            RuleFor(e => e.Name).MustAsync(ActivityNameIsUnique).WithMessage("Activity name must be unique");
        }

        private async Task<bool> ActivityNameIsUnique(Activity activity, string activityName, CancellationToken cancellationToken)
        {
            var activities = await _activityManagementService.GetAll(cancellationToken);
            return !activities.Any(i => i.Model.Id != activity.Id && i.Model.Name.ToLower() == activityName.ToLower());
        }
    }
}
