namespace HomeLink.Server {
    internal static class AppConstants {
        internal const string
                              ROOT_PATH             = "RootPath",
                              SWAGGER_JSON          = "/swagger/v1/swagger.json",
                              APP_NAME              = "HomeLink",
                              APP_VERSION           = "v1",
                              APP_NAME_WITH_VERSION = APP_NAME + " " + APP_VERSION;
    }

    internal static class ContentTypes {
        internal const string
                              Text        = "text/plain",
                              Pdf         = "application/pdf",
                              MsWord      = "application/vnd.ms-word",
                              MsExcel     = "application/vnd.ms-excel",
                              SpreadSheet = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet",
                              Png         = "image/png",
                              Jpg         = "image/jpeg",
                              Gif         = "image/gif",
                              Csv         = "text/csv";
    }

    internal static class FileExtensions {
        internal const string
                              Txt  = ".txt",
                              Pdf  = ".pdf",
                              Doc  = ".doc",
                              Docx = ".docx",
                              Xls  = ".xls",
                              Xlsx = ".xlsx",
                              Png  = ".png",
                              Jpg  = ".jpg",
                              Jpeg = ".jpeg",
                              Gif  = ".gif",
                              Csv  = ".csv";
    }
}