using EMS.Model;
using EMS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace EMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IRepository<Department> _repository;
        public DepartmentController(IRepository<Department> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> AddDep(Department department)
        {
            await _repository.AddAsync(department);
            await _repository.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDep(int id,Department department)
        {
            var dep = await _repository.FindById(id);
            dep.Name = department.Name;
            await _repository.UpdateAsync(dep);
            await _repository.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllDep()
        {
            var dep = await _repository.GetAll();
            return Ok(dep);
        } 
        [HttpDelete]
        public async Task<IActionResult> DeleteDep(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
            return Ok();
        }
    }
}
