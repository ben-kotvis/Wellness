using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model.ModelValidation
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
