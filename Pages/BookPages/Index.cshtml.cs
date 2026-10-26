
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gherghel_Anamaria_Lab2.Models;
using System.Linq;

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


    public string TitleSort { get; set; } = "";

    public string AuthorSort { get; set; } = "";

    public string? CurrentFilter { get; set; }

    public async Task OnGetAsync(
     int? id,
     int? categoryID,
     string? sortOrder,
     string? searchString)
    {
        TitleSort = string.IsNullOrEmpty(sortOrder)
            ? "title_desc"
            : "";

        AuthorSort = sortOrder == "author"
            ? "author_desc"
            : "author";

        CurrentFilter = searchString;

        BookD.Books = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();


        if (!string.IsNullOrEmpty(searchString))
        {
            BookD.Books = BookD.Books
                .Where(b =>
                    b.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    (b.Authors != null &&
                     (b.Authors.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                      b.Authors.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))));
        }

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

        switch (sortOrder)
        {
            case "title_desc":
                BookD.Books = BookD.Books
                    .OrderByDescending(b => b.Title);
                break;

            case "author":
                BookD.Books = BookD.Books
                    .OrderBy(b => b.Authors!.LastName)
                    .ThenBy(b => b.Authors!.FirstName);
                break;

            case "author_desc":
                BookD.Books = BookD.Books
                    .OrderByDescending(b => b.Authors!.LastName)
                    .ThenByDescending(b => b.Authors!.FirstName);
                break;

            default:
                BookD.Books = BookD.Books
                    .OrderBy(b => b.Title);
                break;
        }
    }
}
