using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Domain.ModelValidation
{
    public class CompanyValidation : AbstractValidator<Company>
    {
        private readonly IPersistanceReaderService<Company> _activityManagementService;
        private readonly IRequestDependencies<Company> _requestDependencies;
        public CompanyValidation(IPersistanceReaderService<Company> activityManagementService, IRequestDependencies<Company> requestDependencies)
        {
            _requestDependencies = requestDependencies;
            _activityManagementService = activityManagementService;
            RuleFor(e => e.Name).NotEmpty().MaximumLength(50).WithMessage("Please provide a value that is less than 50 characters");
            RuleFor(e => e.Name).MustAsync(CompanyNameIsUnique).WithMessage("Activity name must be unique");
        }

        private async Task<bool> CompanyNameIsUnique(Company activity, string activityName, CancellationToken cancellationToken)
        {
            var activities = await _activityManagementService.GetAll(_requestDependencies.CompanyId, cancellationToken);
            return !activities.Any(i => i.Model.Id != activity.Id && i.Model.Name.ToLower() == activityName.ToLower());
        }
    }
}
