using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Selenoid.Client.Infrastructure.Common.List;

namespace Selenoid.Client.Tests.Infrastructure.Common.List
{
    [TestFixture]
    public class ListResponseDeserializerTests
    {
        private ListResponseDeserializer deserializer;

        [OneTimeSetUp]
        public void Setup()
        {
            deserializer = new ListResponseDeserializer();
        }

        [TestCaseSource(nameof(Deserialize_Should_Correctly_Deserialize_String_Response_Cases))]
        public void Deserialize_Should_Correctly_Deserialize_String_Response(string input, ListResponse expected)
        {
            var actual = deserializer.Deserialize(input);
            actual.Should().BeEquivalentTo(expected);
        }

        private static IEnumerable<TestCaseData> Deserialize_Should_Correctly_Deserialize_String_Response_Cases()
        {
            yield return new TestCaseData(
                @"<pre>
                    <a href=""link_name1"">name1</a>
                </pre>",
                new ListResponse
                {
                    Items = new[]
                    {
                        new ListResponseItem {Link = "link_name1", Name = "name1"},
                    }
                }).SetName("Single item");
            
            yield return new TestCaseData(
                @"<pre>
                    <a href=""link_name1"">name1</a>
                    <a href=""link_name2"">name2</a>
                </pre>",
                new ListResponse
                {
                    Items = new[]
                    {
                        new ListResponseItem {Link = "link_name1", Name = "name1"},
                        new ListResponseItem {Link = "link_name2", Name = "name2"},
                    }
                }).SetName("Multiple items");
            yield return new TestCaseData(
                @"<pre>
                </pre>",
                new ListResponse
                {
                    Items = null
                }).SetName("No items");
            yield return new TestCaseData(
                "",
                null).SetName("empty string response");
            yield return new TestCaseData(
                null,
                null).SetName("null string response");
        }
    }
}