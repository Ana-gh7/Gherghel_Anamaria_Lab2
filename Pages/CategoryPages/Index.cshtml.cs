using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gherghel_Anamaria_Lab2.Models;
using Gherghel_Anamaria_Lab2;

namespace Gherghel_Anamaria_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Gherghel_Anamaria_Lab2Context _context;

    public IndexModel(Gherghel_Anamaria_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Category = await _context.Category.ToListAsync();
    }
}
