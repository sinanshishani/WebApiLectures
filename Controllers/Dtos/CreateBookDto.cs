using BasicsOfWebApi.Entities;
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace BasicsOfWebApi.Controllers.Dtos
{

    public class CreateBookDto
    {
        [Required(ErrorMessage = "This Field is Required")]
        [MaxLength(20, ErrorMessage = "Name cannot be more than 20 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Out Of Range")]
        public int AuthorId { get; set; }
    }
}
