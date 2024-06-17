using Microsoft.AspNetCore.Http;

namespace Draft.Infrastructures.Models.Request;

/// <summary>
/// FileUploadRequest
/// </summary>
public class FileUploadRequest
{
    /// <summary>
    /// FileString
    /// </summary>
    public IFormFile FileUpload { get; set; } = null!;

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
