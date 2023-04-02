using System.Reflection;
using System.Text;

namespace LAX.Transmission
{
    public static class ExampleHelper<T> 
    {
        public static string ForExample => cache ??= typeof(T).ForExample();
        // ReSharper disable once StaticMemberInGenericType
        private static string? cache;
    }

    public static class ExampleHelper
    {
        public static string ForExample(this Type type) => type.GetProperties().Aggregate(
            new StringBuilder(), (sb, p) =>
            {
                var attr = p.GetCustomAttribute<DescriptionAttribute>();
                if (attr == null) return sb;
                var str = attr.Description.Replace("[this]", p.Name);
                if (attr.InnerType != null)
                {
                    str = str.Replace("[then]", attr.InnerType.ForExample());
                }
                return sb.Append(str);
            }).ToString();

        
    }
}
