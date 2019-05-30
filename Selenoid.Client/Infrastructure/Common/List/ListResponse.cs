using System.Xml.Serialization;

namespace Selenoid.Client.Infrastructure.Common.List
{
    [XmlRoot("pre")]
    public class ListResponse
    {
        [XmlElement("a")]
        public ListResponseItem[] Items { get; set; }
    }

    public class ListResponseItem
    {
        [XmlAttribute("href")]
        public string Link { get; set; }

        [XmlText]
        public string Name { get; set; }
    }
}