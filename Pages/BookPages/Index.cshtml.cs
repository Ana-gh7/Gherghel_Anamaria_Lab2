
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gherghel_Anamaria_Lab2.Models;

namespace Gherghel_Anamaria_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Gherghel_Anamaria_Lab2Context _context;

    public IndexModel(Gherghel_Anamaria_Lab2Context context)
    {
        _context = context;
    }

    public BookData BookD { get; set; } = new BookData();

    public int BookID { get; set; }

    public int CategoryID { get; set; }

    public async Task OnGetAsync(int? id, int? categoryID)
    {
        BookD.Books = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();

        if (id != null)
        {
            BookID = id.Value;

            var book = BookD.Books
                .Single(b => b.ID == id.Value);

            BookD.BookCategories = book.BookCategories
                ?? new List<BookCategory>();
        }

        if (categoryID != null)
        {
            CategoryID = categoryID.Value;
        }
    }
}
