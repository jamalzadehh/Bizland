namespace Bizland.ViewModels
{
    public class TeamUpdateVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Linkedin { get; set; }
        public string  ImageUrl { get; set; }

        public IFormFile Image { get; set; }
    }
}
