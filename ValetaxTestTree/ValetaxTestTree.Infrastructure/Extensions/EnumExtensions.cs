using System.ComponentModel;

namespace ValetaxTestTree.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var result = Attribute.GetCustomAttribute(
                value.GetType().GetField(value.ToString()),
                typeof(DescriptionAttribute)) as DescriptionAttribute;

            return result?.Description
;        }
    }
}
