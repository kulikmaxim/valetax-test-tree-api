using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using ValetaxTestTree.Application.Models;
using ValetaxTestTree.Application.Requests;
using ValetaxTestTree.Domain.Repositories;

namespace ValetaxTestTree.Application.Handlers
{
    public class GetJournalEventQueryHandler : IRequestHandler<GetJournalEventQuery, JournalEventResult>
    {
        private readonly IValidator<GetJournalEventQuery> validator;
        private readonly IJournalEventRepository journalEventRepository;
        private readonly IMapper mapper;

        public GetJournalEventQueryHandler(
            IValidator<GetJournalEventQuery> validator,
            IJournalEventRepository journalEventRepository,
            IMapper mapper)
        {
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.journalEventRepository = journalEventRepository
                 ?? throw new ArgumentNullException(nameof(journalEventRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<JournalEventResult> Handle(
            GetJournalEventQuery request,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            validator.ValidateAndThrow(request);

            var journalEvent = await journalEventRepository
                .FindAsync(new object[] { request.Id }, cancellationToken);
            var result = mapper.Map<JournalEventResult>(journalEvent);

            return result;
        }
    }
}
