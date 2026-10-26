
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gherghel_Anamaria_Lab2.Models;
using Gherghel_Anamaria_Lab2.Models.ViewModels;

namespace Gherghel_Anamaria_Lab2.Pages_Publishers
{
    public class IndexModel : PageModel
    {
        private readonly Gherghel_Anamaria_Lab2Context _context;

        public IndexModel(Gherghel_Anamaria_Lab2Context context)
        {
            _context = context;
        }

        public IList<Publisher> Publisher { get; set; } = default!;

        public PublisherIndexData PublisherData { get; set; }
            = new PublisherIndexData();

        public int PublisherID { get; set; }

        public async Task OnGetAsync(int? id)
        {
            PublisherData = new PublisherIndexData();

            PublisherData.Publishers = await _context.Publisher
                .Include(p => p.Books)
                    .ThenInclude(b => b.Authors)
                .OrderBy(p => p.PublisherName)
                .ToListAsync();

            if (id != null)
            {
                PublisherID = id.Value;

                Publisher publisher = PublisherData.Publishers
                    .Single(p => p.ID == id.Value);

                PublisherData.Books = publisher.Books
                    ?? new List<Book>();
            }

            Publisher = PublisherData.Publishers.ToList();
        }
    }
}
