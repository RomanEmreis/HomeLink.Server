using System;
using System.IO;

namespace HomeLink.Server.Extensions {
    internal static class PathExtensions {
        internal static string GetContentType(this string path) {
            var ext = Path.GetExtension(path)?.ToLowerInvariant();

            return ext switch {
                FileExtensions.Txt  => ContentTypes.Text,
                FileExtensions.Pdf  => ContentTypes.Pdf,
                FileExtensions.Doc  => ContentTypes.MsWord,
                FileExtensions.Docx => ContentTypes.MsWord,
                FileExtensions.Xls  => ContentTypes.MsExcel,
                FileExtensions.Xlsx => ContentTypes.SpreadSheet,
                FileExtensions.Png  => ContentTypes.Png,
                FileExtensions.Jpeg => ContentTypes.Jpg,
                FileExtensions.Jpg  => ContentTypes.Jpg,
                FileExtensions.Gif  => ContentTypes.Gif,
                FileExtensions.Csv  => ContentTypes.Csv,
                _                   => throw new InvalidOperationException()
            };
        }
    }
}
