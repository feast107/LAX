// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    internal sealed class CompilerFeatureRequiredAttribute : Attribute
    {
        public const string RefStructs = "RefStructs";

        public const string RequiredMembers = "RequiredMembers";

        public string FeatureName { get; }

        public bool IsOptional { get; init; }

        public CompilerFeatureRequiredAttribute(string featureName)
        {
            FeatureName = featureName;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    internal sealed class RequiredMemberAttribute : Attribute { }
    internal static class IsExternalInit { }
}

