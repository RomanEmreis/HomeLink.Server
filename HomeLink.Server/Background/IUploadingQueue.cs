using HomeLink.Server.Model;
using System.Threading;
using System.Threading.Tasks;

namespace HomeLink.Server.Background {
    public interface IUploadingQueue {
        ValueTask QueueFile(IUploadingFile file);

        Task<IUploadingFile> Dequeue(CancellationToken cancellationToken);

        ValueTask<string[]> GetQueuedFiles();
    }
}
