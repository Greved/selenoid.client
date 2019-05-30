using System.Collections.Generic;
using System.Linq;
using Selenoid.Client.Models;

namespace Selenoid.Client.Infrastructure.Common.List
{
    public class ListItemsConverter<TItemConverter> : IListItemsConverter<TItemConverter>
        where TItemConverter: IListItemConverter
    {
        private readonly TItemConverter itemConverter;
        private readonly IListResponseDeserializer responseDeserializer;

        public ListItemsConverter(TItemConverter itemConverter, 
                                  IListResponseDeserializer responseDeserializer)
        {
            this.itemConverter = itemConverter;
            this.responseDeserializer = responseDeserializer;
        }

        public List<SelenoidListItem> Convert(string response)
        {
            var listResponse = responseDeserializer.Deserialize(response);
            var items = listResponse?.Items?
                .Select(itemConverter.Convert)
                .Where(x => x != null)
                .ToList() ?? new List<SelenoidListItem>(0);
            return items;
        }
    }
}