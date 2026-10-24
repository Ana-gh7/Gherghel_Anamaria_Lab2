
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gherghel_Anamaria_Lab2;
using Gherghel_Anamaria_Lab2.Models;

namespace Gherghel_Anamaria_Lab2.Pages.BookPages;

public class EditModel : BookCategoriesPageModel
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

        Book = await _context.Book
     .Include(b => b.BookCategories)
         .ThenInclude(bc => bc.Category)
     .FirstOrDefaultAsync(b => b.ID == id);

        if (Book == null)
        {
            return NotFound();
        }

        PopulateAssignedCategoryData(_context, Book);
        LoadLists();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(
        int? id, string[] selectedCategories)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bookToUpdate = await _context.Book
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync(b => b.ID == id);

        if (bookToUpdate == null)
        {
            return NotFound();
        }

        if (await TryUpdateModelAsync<Book>(
            bookToUpdate,
            "Book",
            b => b.Title,
            b => b.Price,
            b => b.AuthorsID,
            b => b.PublisherID))
        {
            UpdateBookCategories(
                _context, selectedCategories, bookToUpdate);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        Book = bookToUpdate;
        PopulateAssignedCategoryData(_context, Book);
        LoadLists();

        return Page();
    }

    private void LoadLists()
    {
        ViewData["PublisherID"] = new SelectList(
            _context.Publisher,
            "ID",
            "PublisherName",
            Book?.PublisherID);

        ViewData["AuthorsID"] = new SelectList(
            _context.Authors,
            "ID",
            "LastName",
            Book?.AuthorsID);
    }
}
