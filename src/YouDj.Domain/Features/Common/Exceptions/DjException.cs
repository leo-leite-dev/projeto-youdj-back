namespace YouDj.Domain.Features.Common.Exceptions;

public class DjException : DomainException
{
    public DjException(string message) : base(message) { }
}