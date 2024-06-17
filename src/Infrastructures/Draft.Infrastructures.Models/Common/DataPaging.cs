namespace Draft.Infrastructures.Models.Common;

public class DataPaging
{
    public IEnumerable<object>? Data { get; set; }

    public long? PaginationCount { get; set; }
}

public class DataPaging<T>
{
    public List<T>? Data { get; set; }

    public long? PaginationCount { get; set; }
}
