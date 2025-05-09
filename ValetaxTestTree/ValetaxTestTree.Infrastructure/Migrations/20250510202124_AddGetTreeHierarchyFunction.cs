using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValetaxTestTree.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGetTreeHierarchyFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
create function get_tree_hierarchy(
   node_name text,
   max_level int
)
returns table (""Id"" int, ""ParentId"" int, ""Name"" text)
language plpgsql
as 
$$
begin
RETURN QUERY 
WITH RECURSIVE ""tree"" AS (
    SELECT ""tn"".""Id"", ""tn"".""ParentId"", ""tn"".""Name"", 1 AS ""Level""
    FROM ""TreeNodes"" ""tn""
    WHERE ""tn"".""ParentId"" IS NULL AND ""tn"".""Name"" = node_name

UNION ALL

    SELECT ""tn"".""Id"", ""tn"".""ParentId"", ""tn"".""Name"", ""tree"".""Level"" + 1
    FROM ""TreeNodes"" ""tn""
    JOIN ""tree"" ON ""tn"".""ParentId"" = ""tree"".""Id""
    WHERE ""tree"".""Level"" < max_level
)
SELECT ""tree"".""Id"", ""tree"".""ParentId"", ""tree"".""Name"" FROM ""tree"";
end;
$$;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP FUNCTION ""get_tree_hierarchy""(text, integer)");
        }
    }
}
