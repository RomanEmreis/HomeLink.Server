using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using HomeLink.Server.Background;
using HomeLink.Server.Extensions;
using HomeLink.Server.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static HomeLink.Server.Constants;

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
        public async Task<IActionResult> Get() => Ok(
            await Task.FromResult(Directory.GetFiles(_rootPath))
        );

        [HttpGet]
        [ProducesResponseType(typeof(IUploadingFile[]), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetQueuedFiles() => Ok(
            await _uploadingQueue.GetQueuedFiles()
        );

        [HttpGet("{name}")]
        [ProducesResponseType(typeof(byte[]), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string name) {
            if (string.IsNullOrWhiteSpace(name)) return BadRequest();

            var path = Path.Combine(_rootPath, name);
            var file = new DownloadingFile(path);
            if (!file.IsExists) return NotFound();

            var memory = await file.Download();

            return File(memory, file.ContentType, file.FileName);
        }

        [HttpPost]
        public async Task<IActionResult> Post(IList<IFormFile> files) {
            if (files.Count == 0) return BadRequest();
            
            foreach (var formFile in files) {
                var file = new UploadingFile(_rootPath, formFile);
            
                await _uploadingQueue.QueueFile(file);
            }

            return Ok();
        }

        [HttpDelete("{name}")]
        public void Delete(string name) {
        }
    }
}
