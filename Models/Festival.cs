using System.ComponentModel.DataAnnotations;

namespace QLLeHoi.Models
{
    public class Festival
    {
      
        public int FestivalId { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Location { get; set; }
        DateTime Date { get; set; }
        //Navigation property
        public int OrganizerId { get; set; }
    }
}
