using Microsoft.EntityFrameworkCore;
using PetFinder2._0.Models;
using SQLitePCL;

namespace PetFinder2._0.Data.Services
{
    public class PetsService : IPetsService
    {
        private readonly ApplicationDbContext _context;

        public PetsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Pet pet)
        {
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Pet> GetAll()
        {
            var applicationDbContext = _context.Pets.Include(i => i.User);
            return applicationDbContext;
        }

        public async Task<Pet> GetByID(int? id)
        {
            var pet = await _context.Pets
            .Include(l => l.User)
            .Include(l => l.Comments)
            .ThenInclude(l => l.User)
            .FirstOrDefaultAsync(m => m.ID == id);
            return pet;
        }
    }
}
