namespace Inhera.Shared.Models.Common
{
    public class StandardPagination
    {
        public int Start { get; set; }
        public int Size { get; set; } = 10;

        public int GetSkips()
        {
            var start = Start > 0 ? Start - 1 : 0;
            return start * Size;
        }

        public int GetTakes()
        {
            return Size;
        }
    }
}
