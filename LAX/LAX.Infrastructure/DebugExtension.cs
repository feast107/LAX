using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace LAX.Infrastructure
{
    public static class DebugExtension
    {
        public static T With<T>(this T then, params object[] args)
        {
            args.ForEach(Console.WriteLine);
            return then;
        }

        public static T WithError<T>(this T then, params object[] args)
        {
            args.ForEach(Console.Error.WriteLine);
            return then;
        }

        /// <summary>
        /// 缓解措施
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string Ease(this string json)
        {
            if (json.Trim().Length != 0)
            {
                var start = json.IndexOf('{');
                var end = json.LastIndexOf('}');
                if (end > start && start >= 0)
                {
                    return json[start..(end + 1)];
                }
            }
            return "{}";
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}
