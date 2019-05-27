using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HomeLink.Server.Application {
    internal sealed class FileData : IFileData {
        private readonly byte[] _fileBytes;

        internal FileData(string fileName, byte[] fileBytes) {
            _fileBytes = fileBytes;
            FileName   = fileName;
        }

        public string FileName { get; }

        public Task Save(string path, CancellationToken cancellationToken) =>
            File.WriteAllBytesAsync(path, _fileBytes, cancellationToken);
    }
}