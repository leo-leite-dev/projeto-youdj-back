namespace YouDj.Application.Common.Results;

public static class ResultCodes
{
    public static class Generic
    {
        public const string Ok = "generic.ok";
        public const string Created = "generic.created";
        public const string NoContent = "generic.no_content";

        public const string BadRequest = "generic.bad_request";
        public const string ServerError = "generic.server_error";
    }

    public static class Auth
    {
        public const string Unauthorized = "auth.unauthorized";
        public const string Forbidden = "auth.forbidden";
    }

    public static class NotFound
    {
        public const string Generic = "notfound.generic";
    }

    public static class Conflict
    {
        public const string Generic = "conflict.generic";
    }
}
