using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wellness.Model.ModelValidation
{
    public class EventValidation : AbstractValidator<Event>
    {
        public EventValidation()
        {
            RuleFor(e => e.Name).NotEmpty().MaximumLength(50).WithMessage("Please provide a value that is less than 50 characters");
            RuleFor(e => e.Points).GreaterThanOrEqualTo(5).LessThanOrEqualTo(300);
            RuleFor(e => e.AnnualMaximum).GreaterThanOrEqualTo(e => e.Points).WithMessage("Annual maximum must be greater than or eqaul to than the points for the single event.");            
        }
    }
}
