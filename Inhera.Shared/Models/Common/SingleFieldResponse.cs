namespace Inhera.Shared.Models.Common
{
    public record SingleFieldResponse<T>
    {
        public T? Value { get; set; }
        public static SingleFieldResponse<T> Of(T value)
        {
            return new SingleFieldResponse<T> { Value = value };
        }
    }
}
