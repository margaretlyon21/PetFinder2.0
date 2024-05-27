using PetFinder2._0.Models;

namespace PetFinder2._0.Data.Services
{
    public interface ICommentsService
    {
        Task Add(Comment comment);
    }
}
