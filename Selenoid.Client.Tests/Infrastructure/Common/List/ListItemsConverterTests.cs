using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using Selenoid.Client.Infrastructure.Common.List;
using Selenoid.Client.Models;

namespace Selenoid.Client.Tests.Infrastructure.Common.List
{
    [TestFixture]
    public class ListItemsConverterTests
    {
        private IListResponseDeserializer deserializer;
        private IListItemConverter singleItemConverter;
        private ListItemsConverter<IListItemConverter> itemsConverter;

        [OneTimeSetUp]
        public void Setup()
        {
            singleItemConverter = A.Fake<IListItemConverter>();
            deserializer = A.Fake<IListResponseDeserializer>();
            itemsConverter = new ListItemsConverter<IListItemConverter>(singleItemConverter, deserializer);
        }

        [TestCaseSource(nameof(Convert_Should_Return_Empty_List_On_Incorrectly_Deserialized_Response_Cases))]
        public void Convert_Should_Return_Empty_List_On_Incorrectly_Deserialized_Response(ListResponse listResponse)
        {
            A.CallTo(() => deserializer.Deserialize(A<string>.Ignored)).Returns(listResponse);
            var actual = itemsConverter.Convert("some");
            actual.Should().BeEquivalentTo(new List<SelenoidListItem>());
        }

        private static IEnumerable<TestCaseData> Convert_Should_Return_Empty_List_On_Incorrectly_Deserialized_Response_Cases()
        {
            yield return new TestCaseData(null).SetName("Null response");
            yield return new TestCaseData(new ListResponse()).SetName("Null items");
            yield return new TestCaseData(new ListResponse{Items = new ListResponseItem[0]}).SetName("Empty items list");
        }

        [Test]
        public void Convert_Should_Filter_Null_Items_From_Item_Converter()
        {
            var listResponse = new ListResponse
            {
                Items = new[] {
                    new ListResponseItem(), 
                    new ListResponseItem(), 
                    new ListResponseItem(),
                    new ListResponseItem()
                }
            };
            A.CallTo(() => deserializer.Deserialize(A<string>.Ignored)).Returns(listResponse);

            var firstActualItem = new SelenoidListItem{Name = "1"};
            var thirdActualItem = new SelenoidListItem{Name = "3"};
            A.CallTo(() => singleItemConverter.Convert(A<ListResponseItem>.Ignored)).Returns(firstActualItem).Once();
            A.CallTo(() => singleItemConverter.Convert(A<ListResponseItem>.Ignored)).Returns(null).Once();
            A.CallTo(() => singleItemConverter.Convert(A<ListResponseItem>.Ignored)).Returns(thirdActualItem).Once();
            A.CallTo(() => singleItemConverter.Convert(A<ListResponseItem>.Ignored)).Returns(null).Once();
            var actual = itemsConverter.Convert("some");
            var expected = new List<SelenoidListItem>
            {
                firstActualItem, thirdActualItem
            };
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Convert_Should_Call_Deserializer_On_String_Response()
        {
            itemsConverter.Convert("some");
            A.CallTo(() => deserializer.Deserialize("some")).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Convert_Should_Item_Converter_On_Every_Item_From_Deserialized_Response()
        {
            var firstResponseItem = new ListResponseItem();
            var secondResponseItem = new ListResponseItem();
            var listResponse = new ListResponse
            {
                Items = new[] {
                    firstResponseItem, 
                    secondResponseItem, 
                }
            };
            A.CallTo(() => deserializer.Deserialize(A<string>.Ignored)).Returns(listResponse);
            
            var actual = itemsConverter.Convert("some");
            A.CallTo(() => singleItemConverter.Convert(firstResponseItem)).MustHaveHappenedOnceExactly();
            A.CallTo(() => singleItemConverter.Convert(secondResponseItem)).MustHaveHappenedOnceExactly();
        }
    }
}