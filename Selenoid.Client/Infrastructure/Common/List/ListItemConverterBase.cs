using System;
using Selenoid.Client.Models;

namespace Selenoid.Client.Infrastructure.Common.List
{
    public abstract class ListItemConverterBase : IListItemConverter
    {
        private readonly ISelenoidClientSettings settings;
        
        protected abstract string ItemUrlSegment { get; }

        public ListItemConverterBase(ISelenoidClientSettings settings)
        {
            this.settings = settings;
        }

        public SelenoidListItem Convert(ListResponseItem responseItem)
        {
            if (string.IsNullOrEmpty(responseItem.Link))
            {
                return null;
            }

            var linkUrl = new Uri($"{settings.SelenoidHostUrl}/{ItemUrlSegment}/{responseItem.Link}");
            
            return new SelenoidListItem
            {
                Link = linkUrl,
                Name = responseItem.Name
            };
        }
    }
}