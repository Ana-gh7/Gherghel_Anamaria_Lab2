
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gherghel_Anamaria_Lab2.Models;

namespace Gherghel_Anamaria_Lab2.Pages.BookPages;

public class EditModel : PageModel
{
    private readonly Gherghel_Anamaria_Lab2Context _context;

    public EditModel(Gherghel_Anamaria_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Book.FirstOrDefaultAsync(b => b.ID == id);

        if (book == null)
        {
            return NotFound();
        }

        Book = book;
        LoadLists();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            LoadLists();
            return Page();
        }

        var bookToUpdate = await _context.Book.FindAsync(Book.ID);

        if (bookToUpdate == null)
        {
            return NotFound();
        }

        bookToUpdate.Title = Book.Title;
        bookToUpdate.Price = Book.Price;
        bookToUpdate.AuthorsID = Book.AuthorsID;
        bookToUpdate.PublisherID = Book.PublisherID;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    private void LoadLists()
    {
        ViewData["PublisherID"] =
            new SelectList(_context.Publisher, "ID", "PublisherName", Book?.PublisherID);

        ViewData["AuthorsID"] =
            new SelectList(_context.Authors, "ID", "LastName", Book?.AuthorsID);
    }
}
