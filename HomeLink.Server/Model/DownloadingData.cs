using System.IO;

namespace HomeLink.Server.Model {
    public sealed class DownloadingData {
        public DownloadingData(string fileName, string contentType, MemoryStream data) =>
            (FileName, ContentType, Data) = (fileName, contentType, data);

        public string FileName { get; }

        public string ContentType { get; }

        public MemoryStream Data { get; }
    }
}
