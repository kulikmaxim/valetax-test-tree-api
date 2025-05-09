using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using ValetaxTestTree.Application.Requests;
using ValetaxTestTree.Domain.Entities;
using ValetaxTestTree.Domain.Repositories;

namespace ValetaxTestTree.Application.Validators
{
    public class DeleteTreeNodeCommandValidator : BaseValidator<DeleteTreeNodeCommand>
    {
        private const string ContextTreeKey = "TreeKey";

        public DeleteTreeNodeCommandValidator(ITreeNodeRepository treeNodeRepository)
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
                .WithMessage(x => $"Provided node with id '{x.NodeId}' doesn't exist in tree '{x.TreeName}'")
                .Must((_, nodeId, ctx) => !HasChildren(nodeId, ctx))
                .WithMessage("Provided node with id '{PropertyValue}' has children");
        }

        private static bool TreeExists(ValidationContext<DeleteTreeNodeCommand> context)
        {
            var tree = (ICollection<TreeNode>)context.RootContextData[ContextTreeKey];

            return tree.Any();
        }

        private static bool NodeExists(int nodeId, ValidationContext<DeleteTreeNodeCommand> context)
        {
            var tree = (ICollection<TreeNode>)context.RootContextData[ContextTreeKey];

            return tree.Any(x => x.Id == nodeId);
        }

        private static bool HasChildren(int nodeId, ValidationContext<DeleteTreeNodeCommand> context)
        {
            var tree = (ICollection<TreeNode>)context.RootContextData[ContextTreeKey];

            return tree.Any(x => x.Id == nodeId 
                && x.Children != null 
                && x.Children.Any());
        }
    }
}
