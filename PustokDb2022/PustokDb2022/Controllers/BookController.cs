using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokDb2022.DAL;
using PustokDb2022.Models;
using PustokDb2022.ViewModels;

namespace PustokDb2022.Controllers
{
    public class BookController : Controller
    {
        private readonly PustokDbContext _context;

        public BookController(PustokDbContext context)
        {
            _context = context;
        }

        public IActionResult GetBook(int id)
        {
            Book book = _context.Books.Include(x => x.Genres).Include(x => x.BookImages).FirstOrDefault(x => x.Id == id);

            return PartialView("_BookModalPartial", book);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Book book = _context.Books
                .Include(x => x.Genres)
                .Include(x => x.Authors)
                .Include(x => x.BookImages)
                .Include(x => x.BookTags).ThenInclude(x => x.Tag)
                .FirstOrDefault(x => x.Id == id);

            BookDetailViewModel detailVM = new BookDetailViewModel
            {
                Book = book,
                ReviewVM = new ReviewCreateViewModel { BookId = id },
                RelatedBooks = _context.Books.Where(x => x.GenreId == book.GenreId || x.AuthorId == book.AuthorId).Take(8).ToList()
            };

            if (book == null)
                return NotFound();

            return View(detailVM);
        }

        [HttpPost]
        public IActionResult Review(ReviewCreateViewModel review)
        {

            if (!ModelState.IsValid)
            {
                Book book = _context.Books
               .Include(x => x.Genres)
               .Include(x => x.Authors)
               .Include(x => x.BookImages)
               .Include(x => x.BookTags).ThenInclude(x => x.Tag)
               .FirstOrDefault(x => x.Id == review.BookId);

                BookDetailViewModel detailVM = new BookDetailViewModel
                {
                    Book = book,
                    RelatedBooks = _context.Books.Where(x => x.GenreId == book.GenreId || x.AuthorId == book.AuthorId).Take(8).ToList(),
                    ReviewVM = review
                };

                return View("detail", detailVM);
            }

            return Ok(review);
        }
    }
}
