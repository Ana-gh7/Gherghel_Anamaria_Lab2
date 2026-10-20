
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gherghel_Anamaria_Lab2.Models;

namespace Gherghel_Anamaria_Lab2.Pages.BookPages;

public class CreateModel : PageModel
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
