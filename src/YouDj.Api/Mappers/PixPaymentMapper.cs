using YouDj.Api.Requests;
using YouDj.Application.Features.Payments.CreatePixPayment;

public static class PixPaymentMapper
{
    public static CreatePixPaymentCommand ToCommand(
        this CreatePixPaymentRequest request,
        HttpContext httpContext)
    {
        var authHeader = httpContext.Request.Headers.Authorization.ToString();

        if (string.IsNullOrWhiteSpace(authHeader) ||
            !authHeader.StartsWith("Bearer "))
        {
            throw new InvalidOperationException("Authorization header inválido.");
        }

        var sessionToken = authHeader["Bearer ".Length..].Trim();

        return new CreatePixPaymentCommand
        {
            SessionToken = sessionToken,
            Credits = request.Credits,
            Phone = request.Phone
        };
    }
}