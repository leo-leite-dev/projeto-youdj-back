namespace YouDj.Domain.Features.Common.Exceptions;

public class UserException : DomainException
{
    public UserException(string message) : base(message) { }
}