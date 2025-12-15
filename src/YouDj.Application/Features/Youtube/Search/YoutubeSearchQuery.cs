using MediatR;
using YouDj.Application.Common.Results;
using YouDj.Application.Youtube;

namespace YouDj.Application.Features.Youtube.Search;

public sealed record YoutubeSearchQuery(string Term, int Limit)
    : IRequest<Result<YoutubeSearchResultDto>>;
