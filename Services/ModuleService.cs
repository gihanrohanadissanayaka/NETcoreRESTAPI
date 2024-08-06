using Microsoft.AspNetCore.Mvc;
using myRestApiApp.Data;
using myRestApiApp.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
namespace myRestApiApp.Services
{
    public class ModuleService : IModuleService
    {
        private readonly AppDbContext _context;
        private readonly IValidationService _validationService;

        public ModuleService(AppDbContext context, IValidationService validationService)
        {
            _context = context;
            _validationService = validationService;
        }

        public ActionResult<IEnumerable<Module>> GetModules()
        {
            try
            {
                return _context.Modules.ToList();
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { Message = ex.Message }) { StatusCode = 500 };
            }
        }

        public ActionResult<Module> GetModule(int id)
        {
            try
            {
                var module = _context.Modules.Find(id);
                if (module == null)
                {
                    return new NotFoundResult();
                }
                return new OkObjectResult(module);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { Message = ex.Message }) { StatusCode = 500 };
            }
        }

        public ActionResult<Module> AddModule(Module module)
        {
            try
            {
                var existingModules = _context.Modules.ToList();
                _validationService.ValidateModule(module, existingModules);

                _context.Modules.Add(module);
                _context.SaveChanges();
                return new OkObjectResult(module);
            }
            catch (ValidationException ex)
            {
                return new BadRequestObjectResult(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { Message = ex.Message }) { StatusCode = 500 };
            }
        }

        public ActionResult UpdateModule(Module module)
        {
            try
            {
                var existingModules = _context.Modules.ToList();
                _validationService.ValidateModule(module, existingModules);

                var existingModule = _context.Modules.Find(module.Id);
                if (existingModule != null)
                {
                    existingModule.Name = module.Name;
                    existingModule.Code = module.Code;
                    _context.SaveChanges();
                    return new OkResult();
                }
                else
                {
                    return new NotFoundResult();
                }
                
            }
            catch (ValidationException ex)
            {
                return new BadRequestObjectResult(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { Message = ex.Message }) { StatusCode = 500 };
            }
        }


        public ActionResult DeleteModule(int id)
        {
            try
            {
                var module = _context.Modules.Find(id);
                if (module != null)
                {
                    _context.Modules.Remove(module);
                    _context.SaveChanges();
                    return new OkResult();
                }
                else
                {
                    return new NotFoundResult();
                }
                
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { Message = ex.Message }) { StatusCode = 500 };
            }
        }
    }
}
