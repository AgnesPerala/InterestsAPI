using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterestsAPI.Models
{
    public class Link
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LinkId { get; set; }
        public string Webbsite { get; set; }
    }
}
