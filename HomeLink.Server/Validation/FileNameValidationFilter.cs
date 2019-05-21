using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace HomeLink.Server.Validation {
    internal sealed class FileNameValidationFilter : IActionFilter {
        private const string _errorMessage = "File name should not be null or empty or whitespace";

        public void OnActionExecuting(ActionExecutingContext context) {
            var param = context.ActionArguments
                .Select(a => a.Value)
                .OfType<string>()
                .SingleOrDefault();

            if (!string.IsNullOrWhiteSpace(param)) return;

            context.Result = new BadRequestObjectResult(_errorMessage);
        }

        public void OnActionExecuted(ActionExecutedContext context) {}
    }
}