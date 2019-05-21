using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeLink.Server.Background {
    internal sealed class UploadingQueue : IUploadingQueue {
        private ConcurrentQueue<IFormFile> _uploadingFiles = new ConcurrentQueue<IFormFile>();
        private SemaphoreSlim              _signal         = new SemaphoreSlim(0);

        public ValueTask QueueFile(IFormFile uploadingFile) {
            if (uploadingFile is null) throw new ArgumentNullException(nameof(uploadingFile));

            _uploadingFiles.Enqueue(uploadingFile);
            _signal.Release();

            return new ValueTask();
        }

        public async Task<IFormFile> Dequeue(CancellationToken cancellationToken) {
            await _signal.WaitAsync(cancellationToken);

            if (_uploadingFiles.TryDequeue(out var uploadingFile))
                return uploadingFile;

            throw new InvalidOperationException(); //TODO: add custom ex
        }

        public ValueTask<string[]> GetQueuedFiles() => 
            new ValueTask<string[]>(
                _uploadingFiles
                    .Select(f => f.FileName)
                    .ToArray()
            );
    }
}