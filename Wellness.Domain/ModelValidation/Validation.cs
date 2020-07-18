using FluentValidation;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Domain.ModelValidation
{
    public class Validation<T> : IValidate<T>
    {
        private readonly IValidator<T> _validator;
        public Validation(IValidator<T> validator)
        {
            _validator = validator;
        }
        public async Task ValidateAndThrowAsync(T valadatee)
        {
            await _validator.ValidateAndThrowAsync(valadatee);
        }
    }
}
