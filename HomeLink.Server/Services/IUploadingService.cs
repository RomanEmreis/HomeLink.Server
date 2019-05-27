using HomeLink.Server.Application;
using System.Threading;
using System.Threading.Tasks;

namespace HomeLink.Server.Services {
    public interface IUploadingService {
        Task Upload(IFileData file, CancellationToken cancellationToken);
    }
}