using Imtahan.DAL;
using Imtahan.DTOs.CardDTOs;
using Imtahan.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Imtahan.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var querible = _context.Cards.ToList();
            List<CardGetDto> list2 = querible.Select(x => new CardGetDto() { Description = x.Description, Icon = x.Icon, Image = x.Image, Title = x.Title}).ToList();
            return View(list2);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}