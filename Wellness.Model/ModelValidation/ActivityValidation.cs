using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wellness.Model.ModelValidation
{
    public class ActivityValidation : AbstractValidator<Activity>
    {
        public ActivityValidation()
        {
            RuleFor(e => e.Name).NotEmpty().MaximumLength(50).WithMessage("Please provide a value that is less than 50 characters");     
        }
    }
}
