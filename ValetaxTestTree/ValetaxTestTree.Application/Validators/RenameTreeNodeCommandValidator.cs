using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using ValetaxTestTree.Application.Requests;
using ValetaxTestTree.Domain.Entities;
using ValetaxTestTree.Domain.Repositories;

namespace ValetaxTestTree.Application.Validators
{
    public class RenameTreeNodeCommandValidator : BaseValidator<RenameTreeNodeCommand>
    {
        private const string ContextTreeKey = "TreeKey";

        public RenameTreeNodeCommandValidator(ITreeNodeRepository treeNodeRepository)
        {
            RuleFor(x => x.TreeName)
                .NotEmpty()
                .CustomAsync(async (treeName, ctx, ct) =>
                {
                    var tree = await treeNodeRepository.GetTreeHierarchyAsync(treeName, ct);
                    ctx.RootContextData.Add(ContextTreeKey, tree);
                })
                .Must((_, _, ctx) => TreeExists(ctx))
                .WithMessage("Provided tree '{PropertyValue}' doesn't exist");

            RuleFor(x => x.NodeId)
                .GreaterThan(0)
                .Must((_, nodeId, ctx) => NodeExists(nodeId, ctx))
                .WithMessage(x => $"Provided node with id '{x.NodeId}' doesn't exist in tree '{x.TreeName}'");

            RuleFor(x => x.NewNodeName)
                .NotEmpty()
                .Must((renameTreeNodeCommand, newNodeName, ctx) => IsNodeNameUnique(
                    newNodeName,
                    renameTreeNodeCommand.NodeId,
                    ctx))
                .WithMessage("Provided name '{PropertyValue}' is not unique across parent siblings");
        }

        private static bool TreeExists(ValidationContext<RenameTreeNodeCommand> context)
        {
            var tree = (ICollection<TreeNode>)context.RootContextData[ContextTreeKey];

            return tree.Any();
        }

        private static bool NodeExists(int nodeId, ValidationContext<RenameTreeNodeCommand> context)
        {
            var tree = (ICollection<TreeNode>)context.RootContextData[ContextTreeKey];

            return tree.Any(x => x.Id == nodeId);
        }

        private static bool IsNodeNameUnique(
            string newNodeName,
            int nodeId,
            ValidationContext<RenameTreeNodeCommand> context)
        {
            var tree = (ICollection<TreeNode>)context.RootContextData[ContextTreeKey];
            var parentNodeId = tree.Single(x => x.Id == nodeId).ParentId;

            return !tree.Any(x => x.Name == newNodeName && x.ParentId == parentNodeId);
        }
    }
}
