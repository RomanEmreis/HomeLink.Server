using FluentAssertions;
using HomeLink.Server.Background;
using HomeLink.Server.Controllers;
using HomeLink.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HomeLink.Server.Tests {
    public class FilesControllerTests {
        private readonly Mock<IDownloadingService> _downloadingServiceMock = new Mock<IDownloadingService>();
        private readonly Mock<IUploadingQueue>     _uploadingQueueMock     = new Mock<IUploadingQueue>();

        [Fact]
        public async Task GetFilesTest_ReturnsNotEmpty() {
            _downloadingServiceMock
                .Setup(s => s.GetStoredFilesList())
                .ReturnsAsync(new[] { "test.test" });

            var controller  = new FilesController(_downloadingServiceMock.Object, _uploadingQueueMock.Object);
            var result      = await controller.GetFiles();
            var resultValue = ((OkObjectResult) result).Value as string[];

            resultValue.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetFilesTest_ReturnsEmpty() {
            _downloadingServiceMock
                .Setup(s => s.GetStoredFilesList())
                .ReturnsAsync(Array.Empty<string>());

            var controller  = new FilesController(_downloadingServiceMock.Object, _uploadingQueueMock.Object);
            var result      = await controller.GetFiles();
            var resultValue = ((OkObjectResult) result).Value as string[];

            resultValue.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetQueuedFilesTest_ReturnsNotEmpty() {
            _uploadingQueueMock
                .Setup(s => s.GetQueuedFiles())
                .ReturnsAsync(new[] { "test1.test", "test2.test" });

            var controller  = new FilesController(_downloadingServiceMock.Object, _uploadingQueueMock.Object);
            var result      = await controller.GetQueuedFiles();
            var resultValue = ((OkObjectResult) result).Value as string[];

            resultValue.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetQueuedFilesTest_ReturnsEmpty() {
            _uploadingQueueMock
                .Setup(s => s.GetQueuedFiles())
                .ReturnsAsync(Array.Empty<string>());

            var controller  = new FilesController(_downloadingServiceMock.Object, _uploadingQueueMock.Object);
            var result      = await controller.GetQueuedFiles();
            var resultValue = ((OkObjectResult) result).Value as string[];

            resultValue.Should().HaveCount(0);
        }
    }
}
