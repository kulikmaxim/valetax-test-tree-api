using FluentValidation;
using ValetaxTestTree.Application.Requests;

namespace ValetaxTestTree.Application.Validators
{
    public class GetJournalEventQueryValidator : BaseValidator<GetJournalEventQuery>
    {
        public GetJournalEventQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
