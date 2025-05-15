using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningApp.Data;
using LearningApp.Models;
using LearningApp.CommandQueries;
using AutoMapper;
using Humanizer;

namespace LearningApp.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DepartmentsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentQuery>>> GetDepartments()
        {
            var result= await _context.Departments
                .Where(c=>!c.IsDeleted)
                .ToListAsync();
            return _mapper.Map<List<Department>, List<DepartmentQuery>>(result);
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentQuery>> GetDepartment(int id)
        {
            var entity = await _context.Departments
                .SingleOrDefaultAsync(c=>c.Id==id && !c.IsDeleted);

            if (entity == null)
            {
                return NotFound();
            }

            return _mapper.Map<DepartmentQuery>(entity);
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, DepartmentCommand command)
        {
            var appUser = User.Identity.Name;

            if (id != command.Id)
            {
                return BadRequest();
            }

            var entity = await _context.Departments
                .SingleOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
            if (entity == null)
                return NotFound();

            var createBy = entity.CreateBy;
            var createDate = entity.CreateAt;
            _mapper.Map<DepartmentCommand, Department>(command, entity);
            entity.CreateBy=createBy;
            entity.CreateAt=createDate;
            entity.EditBy = appUser;
            entity.EditAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok(entity.Id);
        }

        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<int>> PostDepartment(DepartmentCommand command)
        {
            var appUser = User.Identity.Name;

            var entity = _mapper.Map<DepartmentCommand, Department>(command);
            entity.Id = 0;
            entity.CreateBy = appUser;
            entity.CreateAt = DateTime.Now;

            _context.Departments.Add(entity);
            await _context.SaveChangesAsync();

            return Ok(entity.Id);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var appUser = User.Identity.Name;

            var entity = await _context.Departments.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.IsDeleted = true;  
            entity.DeleteBy = appUser;
            entity.DeleteAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }
    }
}
