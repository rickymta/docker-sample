namespace Salvation.Services.Draft.Infrastructures.Models.Request;

/// <summary>
/// FileBase64Request
/// </summary>
public class FileBase64Request
{
    /// <summary>
    /// FileString
    /// </summary>
    public string FileString { get; set; } = null!;

    /// <summary>
    /// AccountId
    /// </summary>
    public string? AccountId { get; set; }

    /// <summary>
    /// FileName
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// FileSize
    /// </summary>
    public int FileSize { get; set; }

    /// <summary>
    /// FileType
    /// </summary>
    public string? FileType { get; set; }

    /// <summary>
    /// ClientSystem
    /// </summary>
    public string? ClientSystem { get; set; }

    /// <summary>
    /// UploadTime
    /// </summary>
    public DateTime? UploadTime { get; set; } = DateTime.Now;
}
