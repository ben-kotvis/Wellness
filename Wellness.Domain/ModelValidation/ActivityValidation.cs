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
        private readonly IRequestDependencies<Activity> _requestDependencies;
        public ActivityValidation(IPersistanceReaderService<Activity> activityManagementService, IRequestDependencies<Activity> requestDependencies)
        {
            _activityManagementService = activityManagementService;
            _requestDependencies = requestDependencies;
            RuleFor(e => e.Name).NotEmpty().MaximumLength(50).WithMessage("Please provide a value that is less than 50 characters");
            RuleFor(e => e.Name).MustAsync(ActivityNameIsUnique).WithMessage("Activity name must be unique");
        }

        private async Task<bool> ActivityNameIsUnique(Activity activity, string activityName, CancellationToken cancellationToken)
        {
            var activities = await _activityManagementService.GetAll(_requestDependencies.CompanyId, cancellationToken);
            return !activities.Any(i => i.Model.Id != activity.Id && i.Model.Name.ToLower() == activityName.ToLower());
        }
    }
}
