using HomeLink.Server.Application;
using HomeLink.Server.Extensions;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HomeLink.Server.Services {
    internal sealed class UploadingService : IUploadingService {
        private readonly string _path;

        public UploadingService(IConfiguration configuration) => 
            _path = configuration.GetRootPath();

        public Task Upload(IFileData file, CancellationToken cancellationToken) => 
            file.Save(Path.Combine(_path, file.FileName), cancellationToken);
    }
}