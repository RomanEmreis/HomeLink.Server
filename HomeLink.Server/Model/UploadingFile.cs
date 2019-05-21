using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HomeLink.Server.Model {
    public sealed class UploadingFile : IUploadingFile {
        private readonly IFormFile _formFile;
        private readonly string    _path;

        public UploadingFile(string rootPath, IFormFile formFile) {
            _formFile = formFile;
            _path     = Path.Combine(rootPath, _formFile.FileName);
        }

        public bool IsExists => File.Exists(_path);

        public async Task Upload(CancellationToken cancellationToken) {
            using var stream = new FileStream(_path, FileMode.Create);
            await _formFile.CopyToAsync(stream, cancellationToken);
        }

        public override string ToString() => _formFile.FileName;
    }
}
