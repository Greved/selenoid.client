using Selenoid.Client.Infrastructure.Common.List;

namespace Selenoid.Client.Infrastructure.Video.List
{
    public class VideoListItemConverter: ListItemConverterBase
    {
        public VideoListItemConverter(ISelenoidClientSettings settings) : base(settings)
        {
        }

        protected override string ItemUrlSegment { get; } = "video";
    }
}