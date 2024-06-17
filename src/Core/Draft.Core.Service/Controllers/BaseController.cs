using Draft.Infrastructures.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace Draft.Core.Service.Controllers;

/// <summary>
/// BaseController
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    /// <summary>
    /// ErrorMessage
    /// </summary>
    /// <param name="message"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    protected ResponseObject ErrorMessage(string message, int code = -1)
    {
        return new ResponseObject { Message = message, Code = code };
    }

    /// <summary>
    /// ErrorMessage
    /// </summary>
    /// <param name="data">Dữ liệu sẽ trả về</param>
    /// <param name="message">Message sẽ trả về</param>
    /// <param name="code">Mã lỗi sẽ trả về</param>
    /// <returns></returns>
    protected ResponseObject ErrorMessage(object data, string message, int code = -1)
    {
        return new ResponseObject { Message = message, Code = code, Data = data };
    }

    /// <summary>
    /// SuccessData
    /// </summary>
    /// <returns></returns>
    protected ResponseObject SuccessData()
    {
        return new ResponseObject { Code = 0 };
    }

    /// <summary>
    /// SuccessData
    /// </summary>
    /// <param name="data">Dữ liệu sẽ trả về</param>
    /// <returns></returns>
    protected ResponseObject SuccessData(object data)
    {
        return new ResponseObject { Code = 0, Data = data };
    }

    /// <summary>
    /// SuccessData
    /// </summary>
    /// <param name="data">Dữ liệu sẽ trả về</param>
    /// <param name="message">Message sẽ trả về</param>
    /// <returns></returns>
    protected ResponseObject SuccessData(object data, string message)
    {
        return new ResponseObject { Code = 0, Data = data, Message = message };
    }

    /// <summary>
    /// SuccessMessage
    /// </summary>
    /// <param name="message">Message sẽ trả về</param>
    /// <returns></returns>
    protected ResponseObject SuccessMessage(string message)
    {
        return new ResponseObject { Code = 0, Message = message };
    }
}
