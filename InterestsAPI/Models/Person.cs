using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterestsAPI.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public int Age { get; set; }
        public int PhoneNumber { get; set; }
        public List<Interest>? Interests { get; set; } = new List<Interest>();

    }
}
