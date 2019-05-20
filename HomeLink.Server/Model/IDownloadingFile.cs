using System.IO;
using System.Threading.Tasks;

namespace HomeLink.Server.Model {
    internal interface IDownloadingFile {
        string FileName { get; }

        string ContentType { get; }

        bool IsExists { get; }

        Task<MemoryStream> Download();
    }
}
