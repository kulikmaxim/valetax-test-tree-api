using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using ValetaxTestTree.Application.Exceptions;

namespace ValetaxTestTree.Application.Validators
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        protected override bool PreValidate(ValidationContext<T> context, ValidationResult result)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            if (context.InstanceToValidate == null)
            {
                var entityName = typeof(T).Name;
                result.Errors.Add(new ValidationFailure(entityName, $"{entityName} object must be provided"));
                return false;
            }

            return true;
        }

        protected override void RaiseValidationException(ValidationContext<T> context, ValidationResult result)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage);

            throw new SecureException(string.Join(Environment.NewLine, errorMessages));
        }
    }
}
