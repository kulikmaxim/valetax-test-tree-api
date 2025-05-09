using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using ValetaxTestTree.Application.Requests;
using ValetaxTestTree.Domain.Entities;
using ValetaxTestTree.Domain.Repositories;

namespace ValetaxTestTree.Application.Validators
{
    public class CreateTreeNodeCommandValidator : BaseValidator<CreateTreeNodeCommand>
    {
        private const string ContextTreeKey = "TreeKey";

        public CreateTreeNodeCommandValidator(ITreeNodeRepository treeNodeRepository)
        {
            ArgumentNullException.ThrowIfNull(treeNodeRepository, nameof(treeNodeRepository));

            RuleFor(x => x.TreeName)
                .NotEmpty()
                .CustomAsync(async (treeName, ctx, ct) => 
                {
                    var tree = await treeNodeRepository.GetTreeHierarchyAsync(treeName, ct);
                    ctx.RootContextData.Add(ContextTreeKey, tree);
                })
                .Must((_, _, ctx) => TreeExists(ctx))
                .WithMessage("Provided tree '{PropertyValue}' doesn't exist");

            RuleFor(x => x.ParendNodeId)
                .GreaterThan(0)
                .Must((_, parentNodeId, ctx) => ParentNodeExists(parentNodeId, ctx))
                .WithMessage(x => $"Provided parent node with id '{x.ParendNodeId}' doesn't exist in tree '{x.TreeName}'");

            RuleFor(x => x.NodeName)
                .NotEmpty()
                .Must((createTreeNodeComand, nodeName, ctx) => IsNodeNameUnique(
                    nodeName,
                    createTreeNodeComand.ParendNodeId,
                    ctx))
                .WithMessage(x => $"Provided parent node  with id '{x.ParendNodeId}' contains child with the same name '{x.NodeName}'");
        }

        private static bool TreeExists(ValidationContext<CreateTreeNodeCommand> context)
        {
            var tree = (ICollection<TreeNode>)context.RootContextData[ContextTreeKey];

            return tree.Any();
        }

        private static bool ParentNodeExists(int parentNodeId, ValidationContext<CreateTreeNodeCommand> context)
        {
            var tree = (ICollection<TreeNode>)context.RootContextData[ContextTreeKey];

            return tree.Any(x => x.Id == parentNodeId);
        }

        private static bool IsNodeNameUnique(
            string nodeName,
            int parentNodeId,
            ValidationContext<CreateTreeNodeCommand> context)
        {
            var tree = (ICollection<TreeNode>)context.RootContextData[ContextTreeKey];

            return !tree.Any(x => x.Name == nodeName && x.ParentId == parentNodeId);
        }
    }
}
