using PetFinder2._0.Models;

namespace PetFinder2._0.Data.Services
{
    public interface IPetsService
    {
        IQueryable<Pet> GetAll();
        Task Add(Pet pet);
        Task<Pet> GetByID(int? id);

    }
}
