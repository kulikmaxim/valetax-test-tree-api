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
    public class CreateTreeNodeCommandHandler : IRequestHandler<CreateTreeNodeCommand>
    {
        private readonly IValidator<CreateTreeNodeCommand> validator;
        private readonly IReadWriteRepository<TreeNode> treeNodeRepository;
        private readonly IMapper mapper;

        public CreateTreeNodeCommandHandler(
            IValidator<CreateTreeNodeCommand> validator,
            IReadWriteRepository<TreeNode> treeNodeRepository,
            IMapper mapper)
        {
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.treeNodeRepository = treeNodeRepository
                ?? throw new ArgumentNullException(nameof(treeNodeRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Handle(CreateTreeNodeCommand request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var treeNode = mapper.Map<TreeNode>(request);
            treeNodeRepository.Add(treeNode);
            await treeNodeRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
