using FluentAssertions;
using HomeLink.Server.Extensions;
using System;
using Xunit;

namespace HomeLink.Server.Tests {
    public class PathExtensionsTests {
        [Theory]
        [InlineData("/app/files/test.txt", ContentTypes.Text)]
        [InlineData("/app/files/test.pdf", ContentTypes.Pdf)]
        [InlineData("/app/files/test.doc", ContentTypes.MsWord)]
        [InlineData("/app/files/test.docx", ContentTypes.MsWord)]
        [InlineData("/app/files/test.xls", ContentTypes.MsExcel)]
        [InlineData("/app/files/test.xlsx", ContentTypes.SpreadSheet)]
        [InlineData("/app/files/test.png", ContentTypes.Png)]
        [InlineData("/app/files/test.jpg", ContentTypes.Jpg)]
        [InlineData("/app/files/test.jpeg", ContentTypes.Jpg)]
        [InlineData("/app/files/test.gif", ContentTypes.Gif)]
        [InlineData("/app/files/test.csv", ContentTypes.Csv)]
        public void GetContentTypeTest_EqualsTo(string path, string expected) => 
            path.GetContentType().Should().Be(expected);

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("/app/files/test.xxx")]
        public void GetContentTypeTest_Throws_InvaliOperationException(string path) {
            Action act = () => path.GetContentType();
            act.Should().ThrowExactly<InvalidOperationException>();
        }
    }
}
