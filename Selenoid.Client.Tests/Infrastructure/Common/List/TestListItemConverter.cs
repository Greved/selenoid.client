using Selenoid.Client.Infrastructure.Common.List;

namespace Selenoid.Client.Tests.Infrastructure.Common.List
{
    public class TestListItemConverter : ListItemConverterBase
    {
        public TestListItemConverter(ISelenoidClientSettings settings) : base(settings)
        {
        }

        protected override string ItemUrlSegment { get; } = "test1";
    }
}