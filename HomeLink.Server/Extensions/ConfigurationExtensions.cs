using Microsoft.Extensions.Configuration;
using System.IO;
using static HomeLink.Server.AppConstants;

namespace HomeLink.Server.Extensions {
    internal static class ConfigurationExtensions {
        internal static string GetRootPath(this IConfiguration configuration) =>
            Directory.GetCurrentDirectory() + configuration[ROOT_PATH];
    }
}
