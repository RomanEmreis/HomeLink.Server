using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HomeLink.Server.Application {
    public interface IFileProvider {
        Task<IFileData> GetFileData(IFormFile file, CancellationToken cancellationToken);
    }
}