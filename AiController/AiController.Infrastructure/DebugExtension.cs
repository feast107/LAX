using System;
using System.Collections.Generic;
using System.Text;

namespace AiController.Infrastructure
{
    public static class DebugExtension
    {
        public static T With<T>(this T then, params object[] args)
        {
            args.ForEach(Console.WriteLine);
            return then;
        }

        public static string Ease(this string json) => json.Trim().Length == 0 ? "{}" : json;

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}
