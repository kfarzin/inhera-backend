namespace Inhera.Shared.Database.SQL.Entities
{
    public class SqlEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
