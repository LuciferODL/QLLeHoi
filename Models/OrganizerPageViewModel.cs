namespace QLLeHoi.Models
{
    public class OrganizerPageViewModel
    {
        public List<Organizer> Organizers { get; set; }
        public string? SearchString { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? SortOrder { get; set; }
    }
}
