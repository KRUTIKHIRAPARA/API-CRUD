using APICRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICRUD.Controllers
{
    [Route("student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentContext _db;

        public StudentController(StudentContext context)
        {
            this._db = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentClass>>> GetStudents()
        {
            var data = await _db.studentData.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentClass>> GetStudentById(int id)
        {
            var student = await _db.studentData.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<StudentClass>> CreateStudent(StudentClass student)
        {
            await _db.studentData.AddAsync(student);
            await _db.SaveChangesAsync();
            return Ok(student);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StudentClass>> UpdateStudent(int id, StudentClass student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _db.Entry(student).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return Ok(student);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentClass>> DeleteStudent(int id){
            var std = await _db.studentData.FindAsync(id);

            if(std == null)
            {
                return NotFound();
            }

            _db.studentData.Remove(std);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}
