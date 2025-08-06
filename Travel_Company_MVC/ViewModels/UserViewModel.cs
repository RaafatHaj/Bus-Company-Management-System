namespace Travel_Company_MVC.ViewModels
{
    public class UserViewModel
    {

        public string Id { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string? StationName { get; set; }


    }
}
