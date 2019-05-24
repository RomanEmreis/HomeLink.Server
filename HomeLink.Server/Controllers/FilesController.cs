using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using HomeLink.Server.Background;
using HomeLink.Server.Services;
using HomeLink.Server.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeLink.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase {
        private readonly IDownloadingService _downloadingService;
        private readonly IUploadingQueue     _uploadingQueue;

        public FilesController(IDownloadingService downloadingService, IUploadingQueue uploadingQueue) {
            _downloadingService = downloadingService;
            _uploadingQueue     = uploadingQueue;
        }

        [HttpGet]
        [ProducesResponseType(typeof(string[]), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetFiles() => Ok(
            await _downloadingService.GetStoredFilesList()
        );

        [HttpGet("queue")]
        [ProducesResponseType(typeof(string[]), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetQueuedFiles() => Ok(
            await _uploadingQueue.GetQueuedFiles()
        );

        [HttpGet("{name}")]
        [ProducesResponseType(typeof(byte[]), (int) HttpStatusCode.OK)]
        [ServiceFilter(typeof(FileNameValidationFilter))]
        public async Task<IActionResult> DownloadFile(string name) {
            var file = await _downloadingService.Download(name);
            return File(file.Data, file.ContentType, file.FileName);
        }

        [HttpPost]
        [ServiceFilter(typeof(UploadingDataValidationFilter))]
        public async Task<IActionResult> UploadFiles(IList<IFormFile> files) {
            foreach (var file in files)
                await _uploadingQueue.QueueFile(file);

            return Ok();
        }
    }
}