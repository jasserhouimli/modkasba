using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class User
    {

        [Key]
        public int Id { get; set; }
        public string name { get; set; } = String.Empty;
        public int Age { get; set; }
    }
}
