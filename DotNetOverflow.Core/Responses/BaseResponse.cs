using DotNetOverflow.Core.Enum.StatusCodes;

namespace DotNetOverflow.Core.Responses;

public class BaseResponse<T> : IBaseResponse<T>
{
    public required string Description { get; set; }

    public T Data { get; set; }

    public required StatusCode StatusCode { get; set; }
}