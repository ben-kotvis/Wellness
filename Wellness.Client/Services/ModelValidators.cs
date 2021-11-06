using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wellness.Client.Services
{
    public class ModelValidators<T>
    {
        public IValidator<T> Validator { get; private set; }
        public IValidator<T> AsyncValidator { get; private set; }
        public ModelValidators(IValidator<T> validator, IValidator<T> asyncValidator)
        {
            Validator = validator;
            AsyncValidator = asyncValidator;
        }
    }
}
