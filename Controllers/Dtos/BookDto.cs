using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace BasicsOfWebApi.Controllers.Dtos
{
    public class BookDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; } 
    }
}
