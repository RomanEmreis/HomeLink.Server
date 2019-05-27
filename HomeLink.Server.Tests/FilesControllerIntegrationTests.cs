using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HomeLink.Server.Tests {
    public class FilesControllerIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>, IDisposable {
        private readonly HttpClient _client;
        private readonly string    _testPath;

        public FilesControllerIntegrationTests(WebApplicationFactory<Startup> factory) {
            _client   = factory.CreateClient();
            _testPath = Directory.GetCurrentDirectory() + "/Files";
            CreateTestFile();
        }

        private void CreateTestFile() {
            if (!Directory.Exists(_testPath))
                Directory.CreateDirectory(_testPath);

            var payload = Encoding.Unicode.GetBytes("test file");

            File.WriteAllBytes(Path.Combine(_testPath, "1.txt"), payload);
            File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "2.txt"), payload);
        }

        [Fact]
        public async Task GetFilesTest_Returns_StatusCode_Ok() {
            var httpResponse = await _client.GetAsync("/api/files");
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetQueuedFilesTest_Returns_StatusCode_Ok() {
            var httpResponse = await _client.GetAsync("/api/files/queue");
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task UploadFileTest_Returns_StatusCode_Ok() {
            var fileName     = "2.txt";
            var payload      = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), fileName));
            var content      = new ByteArrayContent(payload);

            var multiContent = new MultipartFormDataContent();
            multiContent.Add(content, "files", fileName);

            var httpResponse = await _client.PostAsync("/api/files/", multiContent);

            var uploadedFileInfo = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "2.txt"));
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            uploadedFileInfo.Exists.Should().BeTrue();
            uploadedFileInfo.Length.Should().NotBe(0);
        }

        [Fact]
        public async Task UploadFileTest_Returns_StatusCode_BadRequest() {
            var httpResponse = await _client.PostAsync("/api/files/", null);
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DownloadFileTest_Returns_StatusCode_BadRequest() {
            var httpResponse = await _client.GetAsync("/api/files/3.txt");
            var content      = await httpResponse.Content.ReadAsByteArrayAsync();

            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            content.Should().NotHaveCount(0);
        }

        [Fact]
        public async Task DownloadFileTest_Returns_StatusCode_Ok() {
            var httpResponse = await _client.GetAsync("/api/files/1.txt");
            var content      = await httpResponse.Content.ReadAsByteArrayAsync();

            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Should().NotHaveCount(0);
        }

        public void Dispose() {
            var testingPath = Directory.GetCurrentDirectory() + "/Files";
            Directory.Delete(testingPath, true);

            File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "2.txt"));
        }
    }
}
