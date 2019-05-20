using System.IO;
using System.Threading.Tasks;
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
        [ProducesResponseType(typeof(string[]), 200)]
        public async Task<IActionResult> Get() => Ok(
            await Task.FromResult(Directory.GetFiles(_rootPath))
        );

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id) {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value) {
        }

        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
