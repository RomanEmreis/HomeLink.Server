﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace HomeLink.Server.Validation {
    public class UploadingDataValidationFilter : IActionFilter {
        private const string _errorMessage = "Uploading file list should not be empty";

        public void OnActionExecuting(ActionExecutingContext context) {
            var param = context.ActionArguments
                .Select(a => a.Value)
                .OfType<IList<IFormFile>>()
                .SingleOrDefault();

            if (param is {} && param.Count != 0) return;

            context.Result = new BadRequestObjectResult(_errorMessage);
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
