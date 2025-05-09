using Microsoft.EntityFrameworkCore;
using ValetaxTestTree.Domain.Entities;

namespace ValetaxTestTree.Infrastructure
{
    public class AppDbInitializer
    {
        public static async Task SeedData(AppDbContext appDbContext)
        {
            ArgumentNullException.ThrowIfNull(nameof(appDbContext));

            if (await appDbContext.TreeNodes.AnyAsync())
                return;

            var testTree = CreateTestTree();

            appDbContext.TreeNodes.AddRange(testTree);
            await appDbContext.SaveChangesAsync();
        }

        private static TreeNode CreateTestTree() =>
            new()
            {
                Name = "test root 1",
                Children = new[]
                {
                    new TreeNode
                    {
                        Name = "test child 1",
                        Children = new[]
                        {
                            new TreeNode
                            {
                                Name = "test child 1.1",
                            },
                            new TreeNode
                            {
                                Name = "test child 1.2",
                            }
                        }
                    },
                    new TreeNode
                    {
                        Name = "test child 2",
                    },
                    new TreeNode
                    {
                        Name = "test child 3",
                        Children = new[]
                        {
                            new TreeNode
                            {
                                Name = "test child 3.1",
                                Children = new[]
                                {
                                    new TreeNode
                                    {
                                        Name = "test child 3.1.1",
                                    },
                                    new TreeNode
                                    {
                                        Name = "test child 3.1.2",
                                    }
                                }
                            }
                        }
                    }
                }
            };
    }
}