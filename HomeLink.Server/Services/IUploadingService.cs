using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HomeLink.Server.Services {
    public interface IUploadingService {
        Task Upload(IFormFile file, CancellationToken cancellationToken);
    }
}