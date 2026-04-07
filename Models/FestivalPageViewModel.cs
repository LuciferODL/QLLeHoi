namespace QLLeHoi.Models
{
    public class FestivalPageViewModel
    {
        public List<Festival> Festivals { get; set; } 
        public string? SearchString { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? SortOrder { get; set; }
    }
}
