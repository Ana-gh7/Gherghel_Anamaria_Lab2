
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gherghel_Anamaria_Lab2.Models;
using Gherghel_Anamaria_Lab2;

namespace Gherghel_Anamaria_Lab2.Pages.Borrowings;

public class EditModel : PageModel
{
    private readonly Gherghel_Anamaria_Lab2Context _context;

    public EditModel(Gherghel_Anamaria_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    public List<SelectListItem> MemberList { get; set; } = new();

    public List<SelectListItem> BookList { get; set; } = new();

    private async Task PopulateListsAsync()
    {
        var members = await _context.Member.ToListAsync();

        MemberList = members.Select(m => new SelectListItem
        {
            Value = m.ID.ToString(),
            Text = !string.IsNullOrWhiteSpace(m.FirstName + m.LastName)
    ? m.FirstName + " " + m.LastName
    : m.Email
        }).ToList();

        var books = await _context.Book
            .Include(b => b.Authors)
            .ToListAsync();

        BookList = books.Select(b => new SelectListItem
        {
            Value = b.ID.ToString(),
            Text = b.Title + " - " +
                   (b.Authors != null
                       ? b.Authors.FirstName + " " + b.Authors.LastName
                       : "Autor necunoscut")
        }).ToList();
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var borrowing = await _context.Borrowing
            .FirstOrDefaultAsync(m => m.ID == id);

        if (borrowing == null)
        {
            return NotFound();
        }

        Borrowing = borrowing;

        await PopulateListsAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await PopulateListsAsync();
            return Page();
        }

        _context.Attach(Borrowing).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BorrowingExists(Borrowing.ID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool BorrowingExists(int id)
    {
        return _context.Borrowing.Any(e => e.ID == id);
    }
}
