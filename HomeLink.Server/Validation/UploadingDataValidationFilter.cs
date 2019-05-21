using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace HomeLink.Server.Validation {
    public class UploadingDataValidationFilter : IActionFilter {
        private const string _errorMessage = "Uploading file list should not be empty";

        public void OnActionExecuting(ActionExecutingContext context) {
            var param = context.ActionArguments
                .Select(a => a.Value)
                .OfType<IFormFile>();

            if (param.Any()) return;

            context.Result = new BadRequestObjectResult(_errorMessage);
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
