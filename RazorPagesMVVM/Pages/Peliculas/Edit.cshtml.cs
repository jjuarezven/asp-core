using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMVVM.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesMVVM.Pages.Peliculas
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesMVVM.Data.ApplicationDbContext _context;

        public EditModel(RazorPagesMVVM.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pelicula Pelicula { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pelicula = await _context.Peliculas.FirstOrDefaultAsync(m => m.Id == id);

            if (Pelicula == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Pelicula).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeliculaExists(Pelicula.Id))
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

        private bool PeliculaExists(int id)
        {
            return _context.Peliculas.Any(e => e.Id == id);
        }
    }
}
