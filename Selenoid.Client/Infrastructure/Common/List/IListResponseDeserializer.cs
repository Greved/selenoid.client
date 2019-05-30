namespace Selenoid.Client.Infrastructure.Common.List
{
    public interface IListResponseDeserializer
    {
        ListResponse Deserialize(string response);
    }
}