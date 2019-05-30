using System.IO;
using System.Xml.Serialization;

namespace Selenoid.Client.Infrastructure.Common.List
{
    public class ListResponseDeserializer : IListResponseDeserializer
    {
        private readonly XmlSerializer responseSerializer = new XmlSerializer(typeof(ListResponse));

        public ListResponse Deserialize(string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                return null;
            }

            using (TextReader reader = new StringReader(response))
            {
                var listResponse = (ListResponse) responseSerializer.Deserialize(reader);
                return listResponse;
            }
        }
    }
}