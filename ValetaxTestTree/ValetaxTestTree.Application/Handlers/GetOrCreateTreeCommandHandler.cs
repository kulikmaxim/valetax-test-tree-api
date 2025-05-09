using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using ValetaxTestTree.Application.Models;
using ValetaxTestTree.Application.Requests;
using ValetaxTestTree.Domain.Entities;
using ValetaxTestTree.Domain.Repositories;

namespace ValetaxTestTree.Application.Handlers
{
    public class GetOrCreateTreeCommandHandler : IRequestHandler<GetOrCreateTreeCommand, TreeResult>
    {
        private readonly ITreeNodeRepository treeNodeRepository;
        private readonly IMapper mapper;
        private readonly IValidator<GetOrCreateTreeCommand> validator;

        public GetOrCreateTreeCommandHandler(
            ITreeNodeRepository treeNodeRepository,
            IMapper mapper,
            IValidator<GetOrCreateTreeCommand> validator)
        {
            this.treeNodeRepository = treeNodeRepository ?? throw new ArgumentNullException(nameof(treeNodeRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
        public async Task<TreeResult> Handle(GetOrCreateTreeCommand request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            validator.ValidateAndThrow(request);

            var tree = await treeNodeRepository.GetTreeHierarchyAsync(request.TreeName, cancellationToken);
            var rootNode = tree.FirstOrDefault();
            if (rootNode == null)
            {
                rootNode = mapper.Map<TreeNode>(request);
                rootNode = treeNodeRepository.Add(rootNode);
                await treeNodeRepository.SaveChangesAsync(cancellationToken);
            }

            var result = mapper.Map<TreeResult>(rootNode);

            return result;
        }
    }
}
