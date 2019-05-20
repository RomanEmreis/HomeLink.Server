using System.IO;
using System.Net;
using System.Threading.Tasks;
using HomeLink.Server.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static HomeLink.Server.Constants;

namespace HomeLink.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase {
        private readonly string _rootPath;

        public FilesController(IConfiguration configuration) {
            _rootPath = Directory.GetCurrentDirectory() + configuration[ROOT_PATH];
        }

        [HttpGet]
        [ProducesResponseType(typeof(string[]), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Get() => Ok(
            await Task.FromResult(Directory.GetFiles(_rootPath))
        );

        [HttpGet("{name}")]
        [ProducesResponseType(typeof(byte[]), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string name) {
            if (string.IsNullOrWhiteSpace(name)) return NotFound();

            var       path   = Path.Combine(_rootPath, name);
            var       memory = new MemoryStream();

            using var stream = new FileStream(path, FileMode.Open);

            await stream.CopyToAsync(memory);
            memory.Position = 0;

            return File(memory, path.GetContentType(), Path.GetFileName(path));
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file) {
            if (file is null || file.Length == 0) return BadRequest();

            var       path   = Path.Combine(_rootPath, file.FileName);
            if (System.IO.File.Exists(path)) return BadRequest();

            using var stream = new FileStream(path, FileMode.Create);

            await file.CopyToAsync(stream);

            return Ok();
        }

        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
