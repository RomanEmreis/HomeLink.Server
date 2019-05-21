using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using HomeLink.Server.Background;
using HomeLink.Server.Model;
using HomeLink.Server.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static HomeLink.Server.AppConstants;

namespace HomeLink.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase {
        private readonly string          _rootPath;
        private readonly IUploadingQueue _uploadingQueue;

        public FilesController(IConfiguration configuration, IUploadingQueue uploadingQueue) {
            _rootPath       = Directory.GetCurrentDirectory() + configuration[ROOT_PATH];
            _uploadingQueue = uploadingQueue;
        }

        [HttpGet]
        [ProducesResponseType(typeof(string[]), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetFiles() => Ok(
            await Task.FromResult(Directory.GetFiles(_rootPath))
        );

        [HttpGet("queue")]
        [ProducesResponseType(typeof(IUploadingFile[]), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetQueuedFiles() => Ok(
            await _uploadingQueue.GetQueuedFiles()
        );

        [HttpGet("{name}")]
        [ProducesResponseType(typeof(byte[]), (int) HttpStatusCode.OK)]
        [ServiceFilter(typeof(FileNameValidationFilter))]
        public async Task<IActionResult> DownloadFile(string name) {
            var file = new DownloadingFile(_rootPath, name);
            if (!file.IsExists) return NotFound();

            var memory = await file.Download();
            return File(memory, file.ContentType, file.FileName);
        }

        [HttpPost]
        [ServiceFilter(typeof(UploadingDataValidationFilter))]
        public async Task<IActionResult> UploadFiles(IList<IFormFile> files) {           
            foreach (var formFile in files) {
                var file = new UploadingFile(_rootPath, formFile);
                await _uploadingQueue.QueueFile(file);
            }

            return Ok();
        }

        [HttpDelete("{name}")]
        [ServiceFilter(typeof(FileNameValidationFilter))]
        public void Delete(string name) {
        }
    }
}
