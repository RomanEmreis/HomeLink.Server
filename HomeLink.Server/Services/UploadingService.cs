using HomeLink.Server.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HomeLink.Server.Services {
    internal sealed class UploadingService : IUploadingService {
        private readonly string _path;

        public UploadingService(IConfiguration configuration) => 
            _path = configuration.GetRootPath();

        public async Task Upload(IFormFile file, CancellationToken cancellationToken) {
            using var stream = new FileStream(Path.Combine(_path, file.FileName), FileMode.Create);
            await file.CopyToAsync(stream, cancellationToken);
        }
    }
}