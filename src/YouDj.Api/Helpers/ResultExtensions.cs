using Microsoft.AspNetCore.Mvc;
using YouDj.Application.Common.Results;

namespace YouDj.Api.Helpers;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result, ControllerBase controller)
    {
        if (result.IsSuccess)
            return controller.Ok(result.Value);

        return controller.BadRequest(new { error = result.Error, code = result.Code });
    }

    public static IActionResult ToActionResult(this Result result, ControllerBase controller)
    {
        if (result.IsSuccess)
            return controller.Ok();

        return controller.BadRequest(new { error = result.Error, code = result.Code });
    }
}