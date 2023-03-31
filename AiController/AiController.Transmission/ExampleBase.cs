using System.Reflection;
using System.Text;

namespace AiController.Transmission
{
    public static class ExampleBase<TReal>
    {
        private static readonly PropertyInfo[] Properties = typeof(TReal).GetProperties();
        public static string Example => Properties.Aggregate(new StringBuilder(), (sb, p) =>
        {
            var attr = p.GetCustomAttribute<DescriptionAttribute>();
            return attr == null ? sb : sb.Append(attr.Description.Replace("{0}", p.Name));
        }).ToString();
    }
}
