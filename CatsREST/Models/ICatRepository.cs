

namespace CatsREST.Models
{
    public interface ICatsRepository
    {
       
            public Cat AddCat(Cat c);

            public IEnumerable<Cat> GetCats(string? nameStartsWith = null, int? minAge = null, string? sortOrder = null);

            public Cat? GetById(int id);

            public Cat? UpdateCat(int id, Cat data);

            public Cat? DeleteById(int id);
        }
    }

