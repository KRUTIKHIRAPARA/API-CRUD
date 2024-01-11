using APICRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Text;

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

        [HttpGet]
        [Route("GenAnthorApiData")]
        public async Task<string> GetAnthodApiData()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.43.21:5229");

                HttpResponseMessage response = await client.GetAsync("/User");

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response content
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
            }
            return "";
        }

        [HttpPost]
        [Route("AddUserAnthorApi")]
        public async Task<string> AddUser([FromBody]Users user)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.43.21:5229");

                var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(user);

                // Set the content type
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");


                HttpResponseMessage response = await client.PostAsync("/User", content);

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response content
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
            }
            return "";
        }
    }
}
