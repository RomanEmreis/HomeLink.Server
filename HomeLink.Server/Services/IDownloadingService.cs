using HomeLink.Server.Model;
using System.Threading.Tasks;

namespace HomeLink.Server.Services {
    public interface IDownloadingService {
        ValueTask<string[]> GetStoredFilesList();

        Task<DownloadingData> Download(string name);
    }
}
