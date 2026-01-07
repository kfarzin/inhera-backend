using Inhera.Shared.Enums;
using Inhera.Shared.Util.Common;

namespace Inhera.Shared.Database.SQL.Entities
{
    public class QuestionnaireQuestion
    {
        public required string Section { get; set; }
        public string? Dimension { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        
        [EnumStringValue(typeof(QuestionTypes))]
        public required string QuestionType { get; set; }
        public ICollection<QuestionItem> Items { get; set; } = [];
    }
}
