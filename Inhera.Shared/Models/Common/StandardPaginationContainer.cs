namespace Inhera.Shared.Models.Common
{
    public class StandardPaginationContainer<T>
    {
        public IList<T> Items { get; set; } = [];
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public int GlobalTotal { get; set; }
        public int LocalTotal { get; set; }
        public int Start { get; set; }
        public int Size { get; set; }

        public static StandardPaginationContainer<T> CreateContainer(List<T> items, int total, int start, int size, bool isAlternative = false)
        {
            start = start < 1 ? 1 : start;
            var hasNext = start * size < total ? true : false;
            var hasPrevious = (start - 1) > 0;
            return new StandardPaginationContainer<T>
            {
                Items = items,
                HasNext = hasNext,
                HasPrevious = hasPrevious,
                GlobalTotal = total,
                LocalTotal = items.Count(),
                Start = start,
                Size = size,                
            };
        }
    }
}
