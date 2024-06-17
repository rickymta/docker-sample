namespace Draft.Infrastructures.Models.Filter;

public class FilterBase
{
    public int? Limit { get; set; }

    public int? Page { get; set; }

    public int? Offset { get; set; }

    public bool? IsActived { get; set; }
}
