using FluentValidation;
using ValetaxTestTree.Application.Requests;

namespace ValetaxTestTree.Application.Validators
{
    public class GetJournalEventRangeQueryValidator : BaseValidator<GetJournalEventRangeQuery>
    {
        public GetJournalEventRangeQueryValidator()
        {
            RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Take)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(1000);
            RuleFor(x => x.From).LessThan(x => x.To);
        }
    }
}
