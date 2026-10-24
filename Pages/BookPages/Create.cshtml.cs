
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gherghel_Anamaria_Lab2.Models;

namespace Gherghel_Anamaria_Lab2.Pages.BookPages;

public class CreateModel : BookCategoriesPageModel
{
    private readonly Gherghel_Anamaria_Lab2Context _context;

    public CreateModel(Gherghel_Anamaria_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public IActionResult OnGet()
    {
        ViewData["PublisherID"] =
            new SelectList(_context.Publisher, "ID", "PublisherName");

        ViewData["AuthorsID"] =
            new SelectList(_context.Authors, "ID", "LastName");

        var book = new Book();
        book.BookCategories = new List<BookCategory>();

        PopulateAssignedCategoryData(_context, book);

        return Page();
    }


    public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
    {
        var newBook = new Book();
        newBook.BookCategories = new List<BookCategory>();

        if (selectedCategories != null)
        {
            foreach (var category in selectedCategories)
            {
                newBook.BookCategories.Add(new BookCategory
                {
                    CategoryID = int.Parse(category)
                });
            }
        }

        Book.BookCategories = newBook.BookCategories;

        if (!ModelState.IsValid)
        {
            LoadLists();
            PopulateAssignedCategoryData(_context, Book);
            return Page();
        }

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }


    private void LoadLists()
    {
        ViewData["PublisherID"] =
            new SelectList(_context.Publisher, "ID", "PublisherName");

        ViewData["AuthorsID"] =
            new SelectList(_context.Authors, "ID", "LastName");
    }
}
