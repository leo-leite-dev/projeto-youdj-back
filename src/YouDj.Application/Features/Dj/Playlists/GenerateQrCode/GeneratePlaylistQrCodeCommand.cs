using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Playlists.GenerateQrCode;
public sealed record GeneratePlaylistQrCodeCommand
    : IRequest<Result<GeneratePlaylistQrCodeResult>>;
