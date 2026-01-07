namespace Inhera.Shared.Database.SQL.Entities
{
    public class QuestionItem : SqlEntity
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public int Order { get; set; }
    }
}
