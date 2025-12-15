namespace YouDj.Application.Youtube;

public class YoutubeVideoDto
{
    public string VideoId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string ThumbnailUrl { get; set; } = default!;
}

public class YoutubeSearchResultDto
{
    public List<YoutubeVideoDto> Items { get; set; } = new();
}