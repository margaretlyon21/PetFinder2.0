using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.EntityFrameworkCore;
using PetFinder2._0.Data;
using PetFinder2._0.Data.Services;
using PetFinder2._0.Models;
using SQLitePCL;

namespace PetFinder2._0.Controllers
{
    public class PetsController : Controller
    {
        private readonly IPetsService _PetsService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICommentsService _commentsService;
        private readonly UserManager<IdentityUser> _userManager;

        public PetsController(IPetsService PetsService, IWebHostEnvironment webHostEnvironment, ICommentsService commentsService, UserManager<IdentityUser> userManager)
        {
            _PetsService = PetsService;
            _webHostEnvironment = webHostEnvironment;
            _commentsService = commentsService;
            _userManager = userManager;
        }

        // GET: Pets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _PetsService.GetAll();
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pets/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _PetsService.GetByID(id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }


        // GET: Pets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PetVM pet)
        {
            if (pet.Image != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                string fileName = pet.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    pet.Image.CopyTo(fileStream);
                }
                var petObj = new Pet()
                {
                    Name = pet.Name,
                    Description = pet.Description,
                    Type = pet.Type,
                    Location = pet.Location,
                    IdentityUderID = pet.IdentityUderID,
                    ImagePath = filePath,
                };
                await _PetsService.Add(petObj);
                return RedirectToAction("Index");
            }
            return View(pet);
        }

        // GET: Pets/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // Get: Pets/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string searchName, string searchType, string searchLocation, string searchDescription)
        {
            // Retrieve all pets
            var allPets = await _PetsService.GetAll().ToListAsync();

            // Filter pets based on search criteria
            var filteredPets = allPets.Where(p =>
                (string.IsNullOrEmpty(searchName) || p.Name.Contains(searchName)) &&
                (string.IsNullOrEmpty(searchType) || p.Type.Contains(searchType)) &&
                (string.IsNullOrEmpty(searchLocation) || p.Location.Contains(searchLocation)) &&
                (string.IsNullOrEmpty(searchDescription) || p.Description.Contains(searchDescription))
            );

            // Return the view with filtered pets
            return View("Index", filteredPets);
        }

        // get: Pets/MyPets
        [Authorize]
        public async Task<IActionResult> MyPets()
        {
            var applicationDbContext = _PetsService.GetAll();
            var allPets = await _PetsService.GetAll().ToListAsync();
            return View("Index", allPets.Where(l=>l.IdentityUderID == User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
        [HttpPost]
        public async Task<ActionResult> AddComment([Bind("Id, Name, IdentityUderId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                await _commentsService.Add(comment);
            }
            var pet = await _PetsService.GetByID(comment.ID);
            return View("Details",pet);
        }


        /* // GET: Pets/Edit/5
         public async Task<IActionResult> Edit(int? id)
         {
             if (id == null || _context.Pets == null)
             {
                 return NotFound();
             }

             var pet = await _context.Pets.FindAsync(id);
             if (pet == null)
             {
                 return NotFound();
             }
             return View(pet);
         }

         // POST: Pets/Edit/5
         // To protect from overposting attacks, enable the specific properties you want to bind to.
         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,Type,Location,ImagePath,IsFound,IdentityUderID")] Pet pet)
         {
             if (id != pet.ID)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(pet);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!PetExists(pet.ID))
                     {
                         return NotFound();
                     }
                     else
                     {
                         throw;
                     }
                 }
                 return RedirectToAction(nameof(Index));
             }
             return View(pet);
         }

         // GET: Pets/Delete/5
         public async Task<IActionResult> Delete(int? id)
         {
             if (id == null || _context.Pets == null)
             {
                 return NotFound();
             }

             var pet = await _context.Pets
                 .FirstOrDefaultAsync(m => m.ID == id);
             if (pet == null)
             {
                 return NotFound();
             }

             return View(pet);
         }

         // POST: Pets/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(int id)
         {
             if (_context.Pets == null)
             {
                 return Problem("Entity set 'ApplicationDbContext.Pets'  is null.");
             }
             var pet = await _context.Pets.FindAsync(id);
             if (pet != null)
             {
                 _context.Pets.Remove(pet);
             }

             await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
         }

         private bool PetExists(int id)
         {
           return (_context.Pets?.Any(e => e.ID == id)).GetValueOrDefault();
         }*/




    }


}

