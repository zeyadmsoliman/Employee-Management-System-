using EMS.Data;
using EMS.Model;
using EMS.Repository;
using EMS.Sevice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepository<Employee> repo;
        private readonly IRepository<UserData> userrepo;

        public EmployeeController(IRepository<Employee> repository,IRepository<UserData> userrepo)
        {
            repo = repository;
            this.userrepo = userrepo;
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IEnumerable<Employee>> GetAllEmp()
        {
            var emp = await repo.GetAll();
            return emp;
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetId(int id)
        {
            var emp= await repo.FindById(id);
            return Ok(emp);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AddEmp(Employee employee)
        {
            {
                var user = new UserData() {
                    Email = employee.Email,
                    Password = (new PasswordHelper()).HashPassword("123"),
                    Role = "Employee,"

                };
                await userrepo.AddAsync(user);
                employee.UserId = user.Id;
                await repo.AddAsync(employee);
                await repo.SaveChangesAsync();
                return Ok();
            }

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateEmp(Employee employee, int id)
        {
            {
                var emp = await repo.FindById(id);
                emp.Name = employee.Name;
                emp.Gender = employee.Gender;
                emp.Phone= employee.Phone;
                emp.LastWorkingDate = employee.LastWorkingDate;
                emp.Department = employee.Department;
                emp.Email = employee.Email;
                emp.JobTitle= employee.JobTitle;

                repo.UpdateAsync(emp);
                await repo.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteEmp(int id)
        {
            var emp = await repo.FindById(id);
            await repo.DeleteAsync(id);
            await repo.SaveChangesAsync();
            return Ok();    
        }
     } 
}
