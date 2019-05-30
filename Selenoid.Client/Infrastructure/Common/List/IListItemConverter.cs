using Selenoid.Client.Models;

namespace Selenoid.Client.Infrastructure.Common.List
{
    public interface IListItemConverter
    {
        SelenoidListItem Convert(ListResponseItem responseItem);
    }
}