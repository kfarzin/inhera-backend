namespace Inhera.Shared.Util.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EnumStringValueAttribute : Attribute
    {
        public Type EnumType { get; }

        public EnumStringValueAttribute(Type enumType)
        {
            if (!enumType.IsEnum)
                throw new ArgumentException("Type must be an Enum.", nameof(enumType));
            EnumType = enumType;
        }
    }
}
