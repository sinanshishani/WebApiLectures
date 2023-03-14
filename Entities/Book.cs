using BasicsOfWebApi.Entities.Auditing;
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace BasicsOfWebApi.Entities
{
    public class Book : IAuditData
    {
        public Book()
        {
            CreationTime = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Author Author { get; set; }
        public DateTime AuthoringTime { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
