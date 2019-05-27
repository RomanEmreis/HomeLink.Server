using HomeLink.Server.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeLink.Server.Background {
    internal sealed class UploadingQueue : IUploadingQueue {
        private readonly IFileProvider              _fileProvider;
        private          ConcurrentQueue<IFileData> _uploadingFiles = new ConcurrentQueue<IFileData>();
        private          SemaphoreSlim              _signal         = new SemaphoreSlim(0);

        public UploadingQueue(IFileProvider fileProvider) => _fileProvider = fileProvider;

        public async Task QueueFile(IFormFile uploadingFile) {
            if (uploadingFile is null) throw new ArgumentNullException(nameof(uploadingFile));

            var fileData = await _fileProvider.GetFileData(uploadingFile, CancellationToken.None);

            _uploadingFiles.Enqueue(fileData);
            _signal.Release();
        }

        public async Task QueueFiles(IList<IFormFile> files) {
            foreach (var file in files)
                await QueueFile(file);
        }

        public async Task<IFileData> Dequeue(CancellationToken cancellationToken) {
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