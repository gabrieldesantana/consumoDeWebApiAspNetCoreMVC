using Microsoft.AspNetCore.Mvc;
using PackageTracker.API.Entities;
using PackageTracker.API.Models;
using PackageTracker.API.Persistence.Repository;

namespace PackageTracker.API.Controllers
{
    [ApiController]
    [Route("api/packages")]
    public class PackagesController : ControllerBase
    {
        private readonly IPackageRepository _repository;
        public PackagesController(IPackageRepository repository)
        {
            _repository = repository;
        }

        // GET api/packages
        [HttpGet]
        public IActionResult GetAll()
        {
            var packages = _repository.GetAllPackages();
            return Ok(packages);
        }

        // GET api/packages/{code}
        [HttpGet("{code}")]
        public IActionResult GetByCode(string code)
        {
            var package = _repository.GetPackageByCode(code);
            if (package == null)
                return NotFound();

            return Ok(package);
        }
        
        // POST api/packages
        [HttpPost]
        public IActionResult Post(AddPackageInputModel model)
        {
            var package = new Package(model.Title, model.Weight);

            if (package == null)
                return BadRequest();

            _repository.AddPackage(package);

            return CreatedAtAction("GetByCode", new {code = package.Code}, package);
        }

        [HttpPut]
        public IActionResult Put(string code, AddPackageInputModel model)
        {
            var package = _repository.GetPackageByCode(code);
            if (package == null) 
                return NotFound();

            package.UpdatePackage(model.Title, model.Weight);
            _repository.AddUpdatePackage(package);
            return NoContent();
        }

        // POST api/packages/{code}/updates
        [HttpPost("{code}/updates")]
        public IActionResult PostUpdate(string code, AddPackageUpdateInputModel model)
        {
            var package = _repository.GetPackageByCode(code);
            
            if (package == null)
                return NotFound();

            package.AddUpdate(model.Status, model.Delivered);
            _repository.AddUpdatePackage(package);
            return NoContent();
        }

        [HttpDelete("{code}")]
        public IActionResult Delete(string code)
        {
            var package = _repository.GetPackageByCode(code);

            if (package == null)
                return NotFound();

            _repository.DeletePackage(package);
            return NoContent();
        }
    }
}