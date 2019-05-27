using HomeLink.Server.Application;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HomeLink.Server.Background {
    public interface IUploadingQueue {
        Task QueueFile(IFormFile file);

        Task QueueFiles(IList<IFormFile> files);

        Task<IFileData> Dequeue(CancellationToken cancellationToken);

        ValueTask<string[]> GetQueuedFiles();
    }
}
