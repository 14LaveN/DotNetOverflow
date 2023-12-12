using System.Diagnostics;
using DotNetOverflow.Core.Entity.Question;
using DotNetOverflow.Core.Helpers.Metric;
using DotNetOverflow.Core.Responses;
using DotNetOverflow.Identity.DAL.Database.Interfaces;
using DotNetOverflow.QuestionAPI.Commands.Question.CreateQuestion;
using DotNetOverflow.QuestionAPI.Commands.Question.DeleteQuestion;
using DotNetOverflow.QuestionAPI.Commands.Question.UpdateQuestion;
using DotNetOverflow.QuestionAPI.Queries.Question.GetQuestionDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DotNetOverflow.QuestionAPI.Controllers.V1;

[Route("api/v1/question")]
public class QuestionController(IUnitOfWork unitOfWork,
        IMediator _mediator,
        CreateMetricsHelper _createMetricsHelper)
    : ApiBaseController(unitOfWork)
{
    /// <summary>
    /// Create question
    /// </summary>
    /// <param name="createQuestionCommand"></param>
    /// <returns>Base information about create question method</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">Return base response where data - question entity and Status Code</response>
    /// <response code="400"></response>
    /// <response code="500">Internal server error</response>
    ///
    
    [HttpPost("create-question")]
    public async Task<IBaseResponse<QuestionEntity>> CreateQuestion(
        [FromBody] CreateQuestionCommand createQuestionCommand)
    {
        var stopwatch = Stopwatch.StartNew();

        var profile = await GetProfile();

        if (profile is not null)
        {
            createQuestionCommand.AuthorId = profile.Id!;
            
            var response = await _mediator.Send(createQuestionCommand);
        
            stopwatch.Stop();
            await _createMetricsHelper.CreateMetrics(stopwatch);

            if (response.StatusCode == Core.Enum.StatusCodes.StatusCode.Ok)
            {
                return response;
            }
        }
        
        throw new AggregateException();
    }
    
    /// <summary>
    /// Delete question
    /// </summary>
    /// <param name="deleteQuestionCommand"></param>
    /// <returns>Base information about delete question method</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">Return base response where data - DeleteQuestionCommand and Status Code</response>
    /// <response code="400"></response>
    /// <response code="500">Internal server error</response>
    ///
    
    [HttpDelete("delete-question/{deleteQuestionCommand.Id}")]
    public async Task<IBaseResponse<DeleteQuestionCommand>> DeleteQuestion(
        [FromRoute] DeleteQuestionCommand deleteQuestionCommand)
    {
        var stopwatch = Stopwatch.StartNew();

        var name = GetName();

        if (!name.IsNullOrEmpty())
        {
            deleteQuestionCommand.Author = name;
            var response = await _mediator.Send(deleteQuestionCommand);
        
            stopwatch.Stop();
            await _createMetricsHelper.CreateMetrics(stopwatch);

            if (response.StatusCode == Core.Enum.StatusCodes.StatusCode.Ok)
            {
                return response;
            }
        }
        
        throw new AggregateException();
    }
    
    /// <summary>
    /// Update question
    /// </summary>
    /// <param name="updateQuestionCommand"></param>
    /// <returns>Base information about update question method</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">Return base response where data - QuestionEntity and Status Code</response>
    /// <response code="400"></response>
    /// <response code="500">Internal server error</response>
    ///
    
    [HttpPatch("update-question")]
    public async Task<IBaseResponse<QuestionEntity>> UpdateQuestion(
        [FromBody] UpdateQuestionCommand updateQuestionCommand)
    {
        var stopwatch = Stopwatch.StartNew();

        var profile = await GetProfile();

        if (profile.Id == updateQuestionCommand.AuthorId)
        {
            var response = await _mediator.Send(updateQuestionCommand);
        
            stopwatch.Stop();
            await _createMetricsHelper.CreateMetrics(stopwatch);

            if (response.StatusCode == Core.Enum.StatusCodes.StatusCode.Ok)
            {
                return response;
            }
        }
        
        throw new AggregateException();
    }
    
    /// <summary>
    /// Get question by id
    /// </summary>
    /// <param name="=getQuestionDetailsQuery"></param>
    /// <returns>Base information about get question by id method</returns>
    /// <remarks>
    /// Example request:
    /// </remarks>
    /// <response code="200">Return base response where data - QuestionEntity and Status Code</response>
    /// <response code="400"></response>
    /// <response code="500">Internal server error</response>
    ///

    [HttpGet("get-question/{getQuestionDetailsQuery.Id}")]
    public async Task<QuestionEntity> GetQuestionById(
        [FromRoute] GetQuestionDetailsQuery getQuestionDetailsQuery)
    {
        var stopwatch = Stopwatch.StartNew();
            
        var response = await _mediator.Send(getQuestionDetailsQuery);
        
        stopwatch.Stop();
        await _createMetricsHelper.CreateMetrics(stopwatch);
        
        if (response.StatusCode == Core.Enum.StatusCodes.StatusCode.Ok)
        {
            return response.Data;
        }
        
        throw new AggregateException();
    }
}