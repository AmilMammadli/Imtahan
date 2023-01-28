using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Imtahan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashBoardController : Controller
    {
     
        public IActionResult Index()
        {
            return View();
        }
    }
}
