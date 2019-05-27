using System.Threading;
using System.Threading.Tasks;

namespace HomeLink.Server.Application {
    public interface IFileData {
        string FileName { get; }

        Task Save(string path, CancellationToken cancellationToken);
    }
}