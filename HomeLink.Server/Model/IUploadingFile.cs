using System.Threading;
using System.Threading.Tasks;

namespace HomeLink.Server.Model {
    public interface IUploadingFile {
        bool IsExists { get; }

        Task Upload(CancellationToken cancellationToken);
    }
}
