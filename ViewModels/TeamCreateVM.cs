namespace Bizland.ViewModels
{
    public class TeamCreateVM
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Linkedin { get; set; }
        
        public IFormFile Image { get; set; }
    }
}
