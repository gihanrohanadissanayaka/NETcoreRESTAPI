using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myRestApiApp.Data;
using myRestApiApp.Models;
using myRestApiApp.Services;

namespace myRestApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleService _moduleService;

        public ModulesController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        // GET: api/modules
        [HttpGet]
        public ActionResult<IEnumerable<Module>> GetModules()
        {
            var result = _moduleService.GetModules();
            if (result.Result is ObjectResult errorResult)
            {
                return errorResult;
            }
            return result;
        }

        // GET: api/modules/5
        [HttpGet("{id}")]
        public ActionResult<Module> GetModule(int id)
        {
            var result = _moduleService.GetModule(id);
            if (result.Result is ObjectResult errorResult)
            {
                return errorResult;
            }
            return result;
        }

        // POST: api/modules
        [HttpPost]
        public ActionResult<Module> PostModule(Module module)
        {
            var result = _moduleService.AddModule(module);
            if (result.Result is ObjectResult errorResult)
            {
                return errorResult;
            }
            return CreatedAtAction(nameof(GetModule), new { id = module.Id }, module);
        }

        // PUT: api/modules/5
        [HttpPut("{id}")]
        public IActionResult PutModule(int id, Module module)
        {
            if (id != module.Id)
            {
                return BadRequest();
            }

            var result = _moduleService.UpdateModule(module);
            if (result is ObjectResult errorResult)
            {
                return errorResult;
            }
            return result;
        }

        // DELETE: api/modules/5
        [HttpDelete("{id}")]
        public IActionResult DeleteModule(int id)
        {
            var result = _moduleService.DeleteModule(id);
            if (result is ObjectResult errorResult)
            {
                return errorResult;
            }
            return result;
        }
    }
}
