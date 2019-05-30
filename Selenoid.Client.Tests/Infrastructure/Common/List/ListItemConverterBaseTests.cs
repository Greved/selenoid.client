using System;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using Selenoid.Client.Infrastructure.Common.List;
using Selenoid.Client.Models;

namespace Selenoid.Client.Tests.Infrastructure.Common.List
{
    [TestFixture]
    public class ListItemConverterBaseTests
    {
        private ISelenoidClientSettings settings;
        private TestListItemConverter itemConverter;

        [OneTimeSetUp]
        public void Setup()
        {
            settings = A.Fake<ISelenoidClientSettings>();
            A.CallTo(() => settings.SelenoidHostUrl).Returns("http://selenoid-host.example.com:4444");
            itemConverter = new TestListItemConverter(settings);
        }

        [TestCase("")]
        [TestCase(null)]
        public void Convert_Should_Return_Null_If_Response_Item_Link_Is_Null_Or_Empty(string link)
        {
            var item = new ListResponseItem {Name = "2323", Link = link};
            var actual = itemConverter.Convert(item);
            actual.Should().BeNull();
        }

        [TestCase("name1")]
        [TestCase("name2")]
        public void Convert_Should_Return_Item_With_Name_From_Response_Item(string name)
        {
            var item = new ListResponseItem {Name = name, Link = "some_link"};
            var actual = itemConverter.Convert(item);
            actual.Name.Should().Be(name);
        }

        [TestCase("http://selenoid-host1.example.com:4444")]
        [TestCase("http://selenoid-host2.example.com:4444")]
        public void Convert_Should_Return_Item_With_Link_With_Url_Prefix_From_Settings(string url)
        {
            A.CallTo(() => settings.SelenoidHostUrl).Returns(url).Once();

            var item = new ListResponseItem {Name = "name1", Link = "link1"};
            var actual = itemConverter.Convert(item);
            actual.Link.Should().Be($"{url}/test1/link1");
        }

        [Test]
        public void Convert_Should_Return_Item_With_Name_And_Link_From_Response_Item()
        {
            var item = new ListResponseItem {Name = "name1", Link = "link1"};
            var expected = new SelenoidListItem
            {
                Name = "name1",
                Link = new Uri("http://selenoid-host.example.com:4444/test1/link1")
            };
            var actual = itemConverter.Convert(item);
            actual.Should().BeEquivalentTo(expected);
        }


    }
}