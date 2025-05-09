using FluentValidation;
using ValetaxTestTree.Application.Requests;

namespace ValetaxTestTree.Application.Validators
{
    public class GetOrCreateTreeCommandValidator : BaseValidator<GetOrCreateTreeCommand>
    {
        public GetOrCreateTreeCommandValidator()
        {
            RuleFor(x => x.TreeName).NotEmpty();
        }
    }
}
