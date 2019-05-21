using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HomeLink.Server.Background {
    public interface IUploadingQueue {
        ValueTask QueueFile(IFormFile file);

        Task<IFormFile> Dequeue(CancellationToken cancellationToken);

        ValueTask<string[]> GetQueuedFiles();
    }
}
