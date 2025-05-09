using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using ValetaxTestTree.Application.Models;
using ValetaxTestTree.Application.Requests;
using ValetaxTestTree.Domain.Models;
using ValetaxTestTree.Domain.Repositories;

namespace ValetaxTestTree.Application.Handlers
{
    public class GetJournalEventRangeQueryHandler : IRequestHandler<GetJournalEventRangeQuery, RangeResult<JournalItemResult>>
    {
        private readonly IValidator<GetJournalEventRangeQuery> validator;
        private readonly IMapper mapper;
        private readonly IJournalEventRepository journalEventRepository;

        public GetJournalEventRangeQueryHandler(
            IValidator<GetJournalEventRangeQuery> validator,
            IMapper mapper,
            IJournalEventRepository journalEventRepository)
        {
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.journalEventRepository = journalEventRepository
                 ?? throw new ArgumentNullException(nameof(journalEventRepository));
        }

        public async Task<RangeResult<JournalItemResult>> Handle(
            GetJournalEventRangeQuery request,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            validator.ValidateAndThrow(request);

            var filter = mapper.Map<BaseFilter<JournalEventCriteria>>(request);
            var journalEventResult = await journalEventRepository.GetAsync(filter, cancellationToken);
            var result = mapper.Map<RangeResult<JournalItemResult>>(journalEventResult);

            return result;
        }
    }
}
