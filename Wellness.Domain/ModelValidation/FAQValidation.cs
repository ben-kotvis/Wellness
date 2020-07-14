using FluentValidation;
using Wellness.Model;

namespace Wellness.Domain.ModelValidation
{
    public class FAQValidation : AbstractValidator<FrequentlyAskedQuestion>
    {
        public FAQValidation()
        {
            RuleFor(e => e.Title).NotNull();
            RuleFor(e => e.Answer).NotEmpty();
        }
    }

}
