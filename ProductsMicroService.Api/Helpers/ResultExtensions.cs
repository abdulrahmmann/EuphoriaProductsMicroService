using System.Net;
using ProductsMicroService.Application.Bases;

namespace ProductsMicroService.Helpers;

public static class ResultExtensions
{
    #region Standardized Response Handler
    public static IResult ToResult<T>(this BaseResponse<T> response)
    {
        return response.HttpStatusCode switch
        {
            HttpStatusCode.OK => Results.Ok(response),
            HttpStatusCode.Created => Results.Created(string.Empty, response),
            HttpStatusCode.NoContent => Results.NoContent(),
            HttpStatusCode.BadRequest => Results.BadRequest(response),
            HttpStatusCode.NotFound => Results.NotFound(response),
            HttpStatusCode.Unauthorized => Results.Unauthorized(),
            HttpStatusCode.Accepted => Results.Accepted(string.Empty, response),
            HttpStatusCode.UnprocessableEntity => Results.UnprocessableEntity(response),
            HttpStatusCode.Conflict => Results.Conflict(response),
            HttpStatusCode.InternalServerError => Results.Problem(response.Message),
            _ => Results.StatusCode((int)response.HttpStatusCode)
        };
    }
    #endregion
}