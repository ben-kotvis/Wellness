using FluentValidation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Domain.ModelValidation
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(e => e.FirstName).NotEmpty().MaximumLength(50).WithMessage("Please provide a value that is less than 50 characters");
            RuleFor(e => e.LastName).NotEmpty().MaximumLength(50).WithMessage("Please provide a value that is less than 50 characters");
        }

    }
}
