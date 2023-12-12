using System.Diagnostics;
using DotNetOverflow.Core.Entity.Image;
using DotNetOverflow.Core.Helpers.Metric;
using DotNetOverflow.Core.Responses;
using DotNetOverflow.Identity.DAL.Database.Interfaces;
using DotNetOverflow.ImageAPI.Commands.Image.CreateImage;
using DotNetOverflow.ImageAPI.Commands.Image.CreateImages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotNetOverflow.ImageAPI.Controllers.V1;

[Route("api/v1/image")]
public class ImageController(IUnitOfWork unitOfWork,
        CreateMetricsHelper createMetricsHelper,
        IMediator mediator)
    : ApiBaseController(unitOfWork)
{
    [Obsolete]
    [HttpPost("create-images")]
    public async Task<IBaseResponse<List<ImageEntity>>> CreateImages(
        [FromBody] CreateImagesCommand createImagesCommand)
    {
        var stopwatch = Stopwatch.StartNew();

        var response = await mediator.Send(createImagesCommand);
        
        stopwatch.Stop();
        await createMetricsHelper.CreateMetrics(stopwatch);
        
        //if (response.StatusCode == )

        throw new AggregateException(response.Description);
    }
    
    [HttpPost("create-image")]
    public async Task<IBaseResponse<ImageEntity>> CreateImage(
        [FromBody] CreateImageCommand createImageCommand)
    {
        var stopwatch = Stopwatch.StartNew();
        
        var response = await mediator.Send(createImageCommand);
        
        stopwatch.Stop();
        await createMetricsHelper.CreateMetrics(stopwatch);

        if (response.StatusCode == Core.Enum.StatusCodes.StatusCode.Ok)
            return response;

        throw new AggregateException(response.Description);
    }
}