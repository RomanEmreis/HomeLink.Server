using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HomeLink.Server.Application {
    public class FileProvider : IFileProvider {
        public async Task<IFileData> GetFileData(IFormFile file, CancellationToken cancellationToken) {
            using var memory = new MemoryStream();

            await file.CopyToAsync(memory, cancellationToken);
            return new FileData(file.FileName, memory.ToArray());
        }
    }
}
