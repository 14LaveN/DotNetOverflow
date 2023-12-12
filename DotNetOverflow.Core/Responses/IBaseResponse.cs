using DotNetOverflow.Core.Enum.StatusCodes;

namespace DotNetOverflow.Core.Responses;

public interface IBaseResponse<T>
{
    public StatusCode StatusCode { get; set; }

    public string Description { get; set; }
    
    public T Data { get; set; }
}