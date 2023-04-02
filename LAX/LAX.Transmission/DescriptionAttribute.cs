namespace LAX.Transmission
{
    /// <summary>
    /// <list type="">
    /// <c>[this]</c> as this property name.
    /// </list>
    /// <list type="">
    /// <c>[then]</c> as description of inner type
    /// </list>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = false)]
    public class DescriptionAttribute : Attribute
    {
        public readonly string Description;

        public readonly Type? InnerType;

        /// <summary>
        /// <see cref="DescriptionAttribute"/>
        /// </summary>
        /// <param name="description"></param>
        /// <param name="innerType"></param>
        public DescriptionAttribute(string description,Type? innerType = null)
        {
            Description = description;
            InnerType = innerType;
        }
    }
}
