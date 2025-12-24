using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouDj.Api.Helpers;
using YouDj.Api.Requests;
using YouDj.Application.Features.Payments;

namespace YouDj.Api.Controllers.Payments;

[ApiController]
[Route("api/public/payments")]
[Authorize]
public sealed class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("pix")]
    public async Task<IActionResult> CreatePix(
        [FromBody] CreatePixPaymentRequest request,
        CancellationToken ct)
    {
        var command = request.ToCommand(HttpContext);
        var result = await _mediator.Send(command, ct);

        return result.ToActionResult(this);
    }

    [HttpPost("{paymentId}/confirm")]
    public async Task<IActionResult> Confirm(
       Guid paymentId, CancellationToken ct)
    {
        var command = new ConfirmPixPaymentCommand
        {
            PaymentId = paymentId
        };

        var result = await _mediator.Send(command, ct);
        return result.ToActionResult(this);
    }
}