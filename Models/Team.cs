using Bizland.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bizland.Models
{
    public class Team:BaseEntity
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Linkedin { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
