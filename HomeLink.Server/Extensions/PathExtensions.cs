using System;
using System.Collections.Generic;
using System.IO;

namespace HomeLink.Server.Extensions {
    internal static class PathExtensions {
        internal static string GetContentType(this string path) {
            var ext = Path.GetExtension(path).ToLowerInvariant();
            if (_mimeTypes.TryGetValue(ext, out var type))
                return type;

            throw new ArgumentException();
        }

        private static readonly Dictionary<string, string> _mimeTypes = new Dictionary<string, string> {
            [".txt"] = "text/plain",
            [".pdf"] = "application/pdf",
            [".doc"] = "application/vnd.ms-word",
            [".docx"] = "application/vnd.ms-word",
            [".xls"] = "application/vnd.ms-excel",
            [".xlsx"] = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet",  
            [".png"] = "image/png",
            [".jpg"] = "image/jpeg",
            [".jpeg"] = "image/jpeg",
            [".gif"] = "image/gif",
            [".csv"] = "text/csv"
        };
    }
}
