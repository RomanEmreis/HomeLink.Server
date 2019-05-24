using HomeLink.Server.Extensions;
using HomeLink.Server.Model;
using JM.LinqFaster;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace HomeLink.Server.Services {
    internal sealed class DownloadingService : IDownloadingService {
        private readonly string _path;

        public DownloadingService(IConfiguration configuration) {
            _path = configuration.GetRootPath();
            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);
        }

        public ValueTask<string[]> GetStoredFilesList() {
            var getStoredFilesTask = Task.Run(GetStoredFiles);
            if (getStoredFilesTask.IsCompleted) new ValueTask<string[]>(getStoredFilesTask.Result);

            return new ValueTask<string[]>(getStoredFilesTask);
        }  
        
        private string[] GetStoredFiles() => 
            Directory.GetFiles(_path)
                .SelectF(f => Path.GetFileName(f));

        public async Task<DownloadingData> Download(string name) {
            var       memory = new MemoryStream();
            using var stream = new FileStream(Path.Combine(_path, name), FileMode.Open);

            await stream.CopyToAsync(memory);
            memory.Position = 0;

            return new DownloadingData(name, name.GetContentType(), memory);
        }
    }
}