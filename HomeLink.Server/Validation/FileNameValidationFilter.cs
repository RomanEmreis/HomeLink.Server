using HomeLink.Server.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace HomeLink.Server.Validation {
    internal sealed class FileNameValidationFilter : IActionFilter {
        private const    string         _errorMessage = "File name should not be null or empty or whitespace";
        private readonly IConfiguration _configuration;

        public FileNameValidationFilter(IConfiguration configuration) {
            _configuration = configuration;
        }

        public void OnActionExecuting(ActionExecutingContext context) {
            var param = context.ActionArguments
                .Select(a => a.Value)
                .OfType<string>()
                .SingleOrDefault();

            if (!string.IsNullOrWhiteSpace(param))                              return;
            if (File.Exists(Path.Combine(_configuration.GetRootPath(), param))) return;

            context.Result = new BadRequestObjectResult(_errorMessage);
        }

        public void OnActionExecuted(ActionExecutedContext context) {}
    }
}