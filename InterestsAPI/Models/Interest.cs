using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterestsAPI.Models
{
    public class Interest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InterestId { get; set; }
        public string InterestName { get; set; }
        public string InterestDescription { get; set;}

        public List<Link>? Links { get; set; } = new List<Link>();

    }
}

