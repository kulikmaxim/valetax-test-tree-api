using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ValetaxTestTree.Application.Requests;
using ValetaxTestTree.Domain.Repositories;

namespace ValetaxTestTree.Application.Handlers
{
    public class RenameTreeNodeCommandHandler : IRequestHandler<RenameTreeNodeCommand>
    {
        private readonly IValidator<RenameTreeNodeCommand> validator;
        private readonly ITreeNodeRepository treeNodeRepository;

        public RenameTreeNodeCommandHandler(
            IValidator<RenameTreeNodeCommand> validator,
            ITreeNodeRepository treeNodeRepository)
        {
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.treeNodeRepository = treeNodeRepository
                ?? throw new ArgumentNullException(nameof(treeNodeRepository));
        }

        public async Task Handle(RenameTreeNodeCommand request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var node = await treeNodeRepository.FindAsync(
                new object[] { request.NodeId },
                cancellationToken);
            node.Name = request.NewNodeName;
            treeNodeRepository.Update(node);
            await treeNodeRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
