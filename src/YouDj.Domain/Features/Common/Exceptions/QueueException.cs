namespace YouDj.Domain.Features.Queue.Exceptions;

using YouDj.Domain.Features.Common.Exceptions;

public sealed class QueueException : DomainException
{
    public QueueException(string message) : base(message) { }
}