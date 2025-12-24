namespace YouDj.Api.Contracts.Auth;

public sealed record LoginDjRequest(
    string Identify,
    string Password
);