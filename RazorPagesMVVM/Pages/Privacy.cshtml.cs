using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesMVVM.Pages
{
    public class PrivacyModel : PageModel
    {
        [TempData]
        public string Nombre { get; set; }

        public string Message { get; set; }

        public void OnGet(int id, int pg)
        {
            Message = $"Valores: id:{id} pg:{pg}";
        }
    }
}