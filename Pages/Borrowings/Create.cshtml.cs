
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gherghel_Anamaria_Lab2.Models;
using Gherghel_Anamaria_Lab2;

namespace Gherghel_Anamaria_Lab2.Pages.Borrowings;

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
                    (x.Authors != null
                        ? x.Authors.LastName + " " + x.Authors.FirstName
                        : "Autor necunoscut")
            });

        ViewData["BookID"] = new SelectList(
            bookList, "ID", "BookFullName");

        var members = _context.Member
            .Select(m => new
            {
                m.ID,
                MemberName = m.FirstName + " " + m.LastName,
                m.Email
            })
            .ToList();

        ViewData["MemberID"] = new SelectList(
            members.Select(m => new
            {
                m.ID,
                MemberName = !string.IsNullOrWhiteSpace(m.MemberName.Trim())
                    ? m.MemberName
                    : m.Email
            }),
            "ID",
            "MemberName");
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
