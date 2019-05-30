using System.Collections.Generic;
using Selenoid.Client.Models;

namespace Selenoid.Client.Infrastructure.Common.List
{
    public interface IListItemsConverter<TItemConverter>
        where TItemConverter: IListItemConverter
    {
        List<SelenoidListItem> Convert(string response);
    }
}