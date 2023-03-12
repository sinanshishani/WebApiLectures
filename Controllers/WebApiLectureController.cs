using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace BasicsOfWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApiLectureController : ControllerBase
    {
        private static List<Book> Books = new List<Book>
            {
                new Book {Id = 1, Name = "48 rules of power", Author = new Author { Id = 10 ,Name = "Omar", Age = 26} },
                new Book {Id = 2, Name = "Calculas", Author = new Author { Id =  11,Name = "Mohammad", Age = 36} },
                new Book {Id = 3, Name = "physics", Author = new Author { Id = 12 ,Name = "Ahmad", Age = 46} },
                new Book {Id = 4, Name = "Maths", Author = new Author { Id = 13 ,Name = "Moath", Age = 56} },
                new Book {Id = 5, Name = "Arabic", Author = new Author { Id = 14, Name = "Laith", Age = 66 }}
            };

        private readonly ILogger<WebApiLectureController> _logger;

        private readonly IHttpContextAccessor _contextAccessor;

        public WebApiLectureController(ILogger<WebApiLectureController> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _contextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAll")]
        public IEnumerable<Book> GetAll()
        {
            return Books;
        }

        [HttpGet("GetById")]
        public Book GetBook(int id)
        {
            var book = Books.SingleOrDefault(a => a.Id == id);

            if(book == null) 
            {
                throw new NullReferenceException(string.Format("Did not find Book Entity With Id : {0}", id));
            }

            return book ?? throw new NullReferenceException(string.Format("Did not find Book Entity With Id : {0}", id));
        }

        [HttpPost("Create")]
        public Book PostForAmadAlMasri([FromBody] Book newBook)
        {
            // if the id is not 0 then its already a book entity and thus cannot be created
            if(newBook.Id > 0)
            {
                throw new Exception("Cannot Create An Entity with Id Larger Than Zero!!!");
            }

            var maximumIdInBooksList = Books.Max(a => a.Id);

            newBook.Id = maximumIdInBooksList + 1;

            Books.Add(newBook);

            return newBook;
        }

        [HttpPut("Update")]
        public Book UpdateBook(Book updatedBook)
        {
            // if the id is greater 1 then continue after the if
            if (updatedBook.Id < 1)
            {
                throw new Exception("Cannot Update An Entity with Id Less Than Zero!!!");
            }

            var ExistingBook = Books.SingleOrDefault(a => a.Id == updatedBook.Id);

            if (ExistingBook == null)
            {
                throw new NullReferenceException(string.Format("Did not find Book Entity With Id : {0}", updatedBook.Id));
            }

            var indexOfBook = Books.IndexOf(ExistingBook);

            Books[indexOfBook] = updatedBook;

            return updatedBook;
        }

        [HttpDelete("Delete")]
        public Book DeleteBook(int id)
        {
            var bookToDelete = Books.SingleOrDefault(a => a.Id == id);

            // if did not find the book with the id, in the list
            if (bookToDelete == null)
            {
                throw new NullReferenceException(string.Format("Did not find Book Entity With Id : {0}", id));
            }

            bookToDelete.IsDeleted = true;

            //Books.Remove(bookToDelete);

            return bookToDelete;
        }

    }
    public class Book : IAuditData
    {
        public Book()
        {
            CreationTime = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AuthoringTime { get; set; }

        public Author Author { get; set; }

        public DateTime CreationTime { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public interface IAuditData
    {
        public DateTime CreationTime { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}