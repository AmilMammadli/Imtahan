using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Imtahan.DAL;
using Imtahan.DTOs.CardDTOs;
using Imtahan.Models;
using Imtahan.Extentions.CreateFileExtr;
using Microsoft.AspNetCore.Authorization;

namespace Imtahan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CardController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CardController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
              return View(await _context.Cards.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            var cards = await _context.Cards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cards == null)
            {
                return NotFound();
            }

            return View(cards);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CardPostDto cardPostDto)
        {
           
                _context.Add(new Card()
                {
                    Description= cardPostDto.Description,
                    Title= cardPostDto.Title,
                    Icon = cardPostDto.File.CreateFile(_env.WebRootPath,"assets/img")
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            CardUpdateDto cardUpdateDto = new CardUpdateDto()
            {
                cardGetDto = new CardGetDto()
                {
                    Id = card.Id,
                    Description = card.Description,
                    Title = card.Title,
                    Icon = card.Icon,
                    Image = card.Image
                }
            };
            return View(cardUpdateDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CardUpdateDto cardUpdateDto)
        {
            Card? card = await _context.Cards.FindAsync(cardUpdateDto.cardGetDto.Id);
            if (card == null)
            {
                return NotFound();
            }
            card.Title = cardUpdateDto.cardPostDto.Title;
            card.Description = cardUpdateDto.cardPostDto.Description;
            if(cardUpdateDto.cardPostDto != null)
            {
                card.Image = cardUpdateDto.cardPostDto.File.CreateFile(_env.WebRootPath, "assets/img");
            } 
             await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            }

        // GET: Admin/Card/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            var Card = await _context.Cards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Card == null)
            {
                return NotFound();
            }

            return View(Card);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cards == null)
            {
                return Problem("Entity set 'AppDbContext.CardGetDto'  is null.");
            }
            var card = await _context.Cards.FindAsync(id);
            if (card != null)
            {
                _context.Cards.Remove(card);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardGetDtoExists(int id)
        {
          return _context.Cards.Any(e => e.Id == id);
        }
    }
}
