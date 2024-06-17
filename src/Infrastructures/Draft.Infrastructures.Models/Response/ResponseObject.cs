namespace Draft.Infrastructures.Models.Response;

/// <summary>
/// ResponseObject
/// </summary>
public class ResponseObject
{
    /// <summary>
    /// Code
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Data
    /// </summary>
    public object? Data { get; set; }
}

/// <summary>
/// ResponseObject<T>
/// </summary>
/// <typeparam name="T"></typeparam>
public class ResponseObject<T>
{
    /// <summary>
    /// Code
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Data
    /// </summary>
    public T? Data { get; set; }
}
