
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gherghel_Anamaria_Lab2.Models;
using Gherghel_Anamaria_Lab2;

namespace Gherghel_Anamaria_Lab2.Pages.BorrowingPages;

public class CreateModel : PageModel
{
    private readonly Gherghel_Anamaria_Lab2Context _context;

    public CreateModel(Gherghel_Anamaria_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    public IActionResult OnGet()
    {
        PopulateLists();
        return Page();
    }

    private void PopulateLists()
    {
        var bookList = _context.Book
            .Include(b => b.Authors)
            .Select(x => new
            {
                x.ID,
                BookFullName = x.Title + " - " +
                    x.Authors!.LastName + " " +
                    x.Authors.FirstName
            });

        ViewData["BookID"] = new SelectList(
            bookList, "ID", "BookFullName");

        ViewData["MemberID"] = new SelectList(
            _context.Member, "ID", "FullName");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            PopulateLists();
            return Page();
        }

        _context.Borrowing.Add(Borrowing);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
