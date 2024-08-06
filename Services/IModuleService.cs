using Microsoft.AspNetCore.Mvc;
using myRestApiApp.Models;
namespace myRestApiApp.Services
{
    public interface IModuleService
    {
        ActionResult<IEnumerable<Module>> GetModules();
        ActionResult<Module> GetModule(int id);
        ActionResult<Module> AddModule(Module module);
        ActionResult UpdateModule(Module module);
        ActionResult DeleteModule(int id);
    }
}