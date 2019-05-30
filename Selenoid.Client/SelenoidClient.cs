namespace Selenoid.Client
{
    public class SelenoidClient: ISelenoidClient
    {
        public SelenoidClient(ISelenoidVideoClient video)
        {
            Video = video;
        }

        public ISelenoidVideoClient Video { get; }
    }
}