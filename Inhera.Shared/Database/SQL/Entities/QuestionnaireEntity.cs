namespace Inhera.Shared.Database.SQL.Entities
{
    public class QuestionnaireEntity : SqlEntity
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public ICollection<QuestionnaireQuestion> Questions { get; set; } = [];
    }
}
