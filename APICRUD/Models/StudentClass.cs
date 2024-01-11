using System.ComponentModel.DataAnnotations;

namespace APICRUD.Models
{
    public class StudentClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set;}
        public int Standard { get; set;}
    }

    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfileUrl { get; set; }
    }
}
