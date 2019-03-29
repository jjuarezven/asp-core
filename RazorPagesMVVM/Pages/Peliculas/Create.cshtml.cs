using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMVVM.Models;
using System.Threading.Tasks;

namespace RazorPagesMVVM.Pages.Peliculas
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesMVVM.Data.ApplicationDbContext _context;

        public CreateModel(RazorPagesMVVM.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Pelicula Pelicula { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Peliculas.Add(Pelicula);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}