using System.ComponentModel.DataAnnotations;

namespace QLLeHoi.Models
{
    public class Organizer
    {
        public int OrganizerId { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Experience { get; set; } 
      
        //Navigation property
        public List<Festival>? Festivals { get; set; }
    }
}
