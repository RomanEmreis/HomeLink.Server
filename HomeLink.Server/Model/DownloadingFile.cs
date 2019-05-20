using HomeLink.Server.Extensions;
using System.IO;
using System.Threading.Tasks;

namespace HomeLink.Server.Model {
    public sealed class DownloadingFile : IDownloadingFile {
        private readonly string _path;

        public DownloadingFile(string path) => _path = path;

        public string FileName => Path.GetFileName(_path);

        public string ContentType => _path.GetContentType();

        public bool IsExists => File.Exists(_path);

        public async Task<MemoryStream> Download() {
            var       memory = new MemoryStream();
            using var stream = new FileStream(_path, FileMode.Open);

            await stream.CopyToAsync(memory);
            memory.Position = 0;

            return memory;
        }
    }
}