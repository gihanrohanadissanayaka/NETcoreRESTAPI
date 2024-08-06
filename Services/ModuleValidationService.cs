using myRestApiApp.Models;

namespace myRestApiApp.Services
{
    public interface IValidationService
    {
        void ValidateModule(Module module, IEnumerable<Module> existingModules);
    }

    public class ModuleValidationService : IValidationService
    {
        public void ValidateModule(Module module, IEnumerable<Module> existingModules)
        {
            if (module == null) throw new ArgumentNullException(nameof(module));

            if (string.IsNullOrWhiteSpace(module.Name) ||
                string.IsNullOrWhiteSpace(module.Code) )
            {
                throw new ArgumentException("All module attributes must be provided and not null.");
            }

            if (module.Code.Contains(" "))
            {
                throw new ArgumentException("Module code should not contain spaces.");
            }

            if (existingModules.Any(m => m.Code == module.Code && m.Id != module.Id))
            {
                throw new ArgumentException("Module code must be unique.");
            }

            if (existingModules.Any(m => m.Name == module.Name && m.Id != module.Id))
            {
                throw new ArgumentException("Module name must be unique.");
            }
        }
    }
}
