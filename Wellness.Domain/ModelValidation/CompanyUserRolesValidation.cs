using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Domain.ModelValidation
{
    public class CompanyUserRolesValidation : AbstractValidator<CompanyUserRoles>
    {
        private readonly IReaderService<CompanyUserRoles> _activityManagementService;
        public CompanyUserRolesValidation(IReaderService<CompanyUserRoles> activityManagementService)
        {
            _activityManagementService = activityManagementService;
        }
    }
}
