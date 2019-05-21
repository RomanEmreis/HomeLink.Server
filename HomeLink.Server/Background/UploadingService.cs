using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HomeLink.Server.Background {
    public class UploadingService : BackgroundService {
        private readonly IUploadingQueue      _uploadingQueue;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UploadingService(IUploadingQueue uploadingQueue, IServiceScopeFactory serviceScopeFactory) {
            _uploadingQueue      = uploadingQueue;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected async override Task ExecuteAsync(CancellationToken cancellationToken) {
            while (!cancellationToken.IsCancellationRequested) {
                //using (var scope = _serviceScopeFactory.CreateScope()) {
                //
                //}
                var uploadingFile = await _uploadingQueue.Dequeue(cancellationToken);
                try {
                    await uploadingFile.Upload(cancellationToken);
                } catch {
                }
            }
        }
    }
}
