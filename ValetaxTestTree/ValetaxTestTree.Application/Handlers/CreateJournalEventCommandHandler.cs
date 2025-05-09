using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using ValetaxTestTree.Application.Requests;
using ValetaxTestTree.Domain.Entities;
using ValetaxTestTree.Domain.Repositories;

namespace ValetaxTestTree.Application.Handlers
{
    public class CreateJournalEventCommandHandler : IRequestHandler<CreateJournalEventCommand>
    {
        private  readonly IValidator<CreateJournalEventCommand> validator;
        private readonly IMapper mapper;
        private readonly IReadWriteRepository<JournalEvent> journalEventRepository;

        public CreateJournalEventCommandHandler(
            IValidator<CreateJournalEventCommand> validator,
            IMapper mapper,
            IReadWriteRepository<JournalEvent> journalEventRepository)
        {
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.journalEventRepository = journalEventRepository
                 ?? throw new ArgumentNullException(nameof(journalEventRepository));
        }

        public async Task Handle(CreateJournalEventCommand request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var journalEvent = mapper.Map<JournalEvent>(request);
            journalEventRepository.Add(journalEvent);
            await journalEventRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
