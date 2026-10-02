namespace CatsREST.Models
{
    
        public class CatsRepositoryList : ICatsRepository
        {

            private List<Cat> _cats = new List<Cat>();

            private int _nextId = 1;

            public CatsRepositoryList(bool includeTestData = false)
            {
                if (includeTestData)
                {
                    _cats.Add(new Cat { Id = _nextId++, Name = "Misser", Age = 2 });
                    _cats.Add(new Cat { Id = _nextId++, Name = "Pus", Age = 3 });
                    _cats.Add(new Cat { Id = _nextId++, Name = "Fido", Age = 1 });
                }
            }

            public Cat AddCat(Cat c)
            {
                c.Id = _nextId++;
                _cats.Add(c);
                return c;
            }

            public Cat? DeleteById(int id)
            {
                Cat? cat = GetById(id);
                if (cat != null)
                {
                    _cats.Remove(cat);
                }
                return cat;
            }

            public Cat? GetById(int id)
            {
                return _cats.FirstOrDefault(c => c.Id == id);
            }

            public IEnumerable<Cat> GetCats(string? nameStartsWith = null, int? minAge = null, string? sortOrder = null)
            {
                IEnumerable<Cat> result = _cats.ToList();


                if (minAge != null)
                {
                    result = result.Where(c => c.Age > minAge).ToList();
                }

                if (nameStartsWith != null)
                {
                    result = result.Where(c => c.Name != null && c.Name.StartsWith(nameStartsWith)).ToList();
                }
                if (sortOrder != null)
                {
                    switch (sortOrder.ToLower())
                    {
                        case "name":
                        case "nameasc":
                            result = result.OrderBy(c => c.Name);
                            break;
                        case "namedesc":
                            result = result.OrderByDescending(c => c.Name);
                            break;
                        case "age":
                            result = result.OrderBy(c => c.Age);
                            break;
                        case "agedesc":
                            result = result.OrderByDescending(c => c.Age);
                            break;
                        default:
                            throw new ArgumentException("Invalid sort order");





                    }

                }
                return result;
            }

            public Cat? UpdateCat(int id, Cat data)
            {
                var cat = GetById(id);

                if (cat != null)
                {
                    cat.Name = data.Name;
                    cat.Age = data.Age;
                    return cat;

                }
                else
                {
                    return cat;


                }
            }
        }
    }

