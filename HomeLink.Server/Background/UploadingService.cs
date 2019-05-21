using HomeLink.Server.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HomeLink.Server.Background {
    public class UploadingQueueService : BackgroundService {
        private readonly IUploadingQueue      _uploadingQueue;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UploadingQueueService(IUploadingQueue uploadingQueue, IServiceScopeFactory serviceScopeFactory) {
            _uploadingQueue      = uploadingQueue;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected async override Task ExecuteAsync(CancellationToken cancellationToken) {
            using var scope = _serviceScopeFactory.CreateScope();

            while (!cancellationToken.IsCancellationRequested) {
                var uploadingFile    = await _uploadingQueue.Dequeue(cancellationToken);
                var uploadingService = scope.ServiceProvider.GetRequiredService<IUploadingService>();

                try {
                    await uploadingService.Upload(uploadingFile, cancellationToken);
                } catch {
                }
            }
        }
    }
}
