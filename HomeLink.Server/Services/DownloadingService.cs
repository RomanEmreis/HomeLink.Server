using HomeLink.Server.Extensions;
using HomeLink.Server.Model;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace HomeLink.Server.Services {
    internal sealed class DownloadingService : IDownloadingService {
        private readonly string _path;

        public DownloadingService(IConfiguration configuration) => 
            _path = configuration.GetRootPath();

        public ValueTask<string[]> GetStoredFilesList() =>
            new ValueTask<string[]>(Directory.GetFiles(_path));

        public async Task<DownloadingData> Download(string name) {
            var       memory = new MemoryStream();
            using var stream = new FileStream(Path.Combine(_path, name), FileMode.Open);

            await stream.CopyToAsync(memory);
            memory.Position = 0;

            return new DownloadingData(name, name.GetContentType(), memory);
        }
    }
}