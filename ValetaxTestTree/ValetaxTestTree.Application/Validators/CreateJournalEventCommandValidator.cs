using FluentValidation;
using ValetaxTestTree.Application.Requests;

namespace ValetaxTestTree.Application.Validators
{
    public class CreateJournalEventCommandValidator : BaseValidator<CreateJournalEventCommand>
    {
        public CreateJournalEventCommandValidator()
        {
            RuleFor(x => x.EventId).GreaterThan(0);
            RuleFor(x => x.Timestamp).NotEmpty();
            RuleFor(x => x.Info).NotEmpty();
        }
    }
}
