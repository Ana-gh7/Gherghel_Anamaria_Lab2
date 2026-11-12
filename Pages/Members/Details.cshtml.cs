using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gherghel_Anamaria_Lab2.Models;
using Gherghel_Anamaria_Lab2;

namespace Gherghel_Anamaria_Lab2.Pages.Members;

public class DetailsModel : PageModel
{
    private readonly Gherghel_Anamaria_Lab2Context _context;
    public DetailsModel(Gherghel_Anamaria_Lab2Context context)
    {
        _context = context;
    }

    public Member Member { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var member = await _context.Member.FirstOrDefaultAsync(m => m.ID == id);
        if (member is null)
        {
            return NotFound();
        }
        else
        {
            Member = member;
        }

        return Page();
    }
}
