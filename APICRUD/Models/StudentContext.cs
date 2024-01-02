using Microsoft.EntityFrameworkCore;

namespace APICRUD.Models
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions option) : base(option)
        {
            
        }

        public DbSet<StudentClass> studentData { get; set; }
    }
}
