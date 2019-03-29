using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMVVM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RazorPagesMVVM.Pages.Peliculas
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMVVM.Data.ApplicationDbContext _context;

        public IndexModel(RazorPagesMVVM.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Pelicula> Pelicula { get;set; }

        public async Task OnGetAsync()
        {
            Pelicula = await _context.Peliculas.ToListAsync();
        }
    }
}
