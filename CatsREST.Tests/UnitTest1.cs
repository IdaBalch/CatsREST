using CatsREST.Models;

namespace CatsREST.Tests
{
    public class CatsRepositoryTests
    {
        [Fact]
        public void GetCats_ReturnsAllCats()
        {
            // Arrange
            var repository = new CatsRepositoryList(true);

            // Act
            var cats = repository.GetCats().ToList();

            // Assert
            Assert.Equal(3, cats.Count);
        }

        [Fact]
        public void GetById_ReturnsCorrectCat()
        {
            // Arrange
            var repository = new CatsRepositoryList(true);

            // Act
            var cat = repository.GetById(1);

            // Assert
            Assert.NotNull(cat);
            Assert.Equal("Misser", cat.Name);
            Assert.Equal(2, cat.Age);
        }

        [Fact]
        public void GetById_ReturnsNullForUnknownId()
        {
            // Arrange
            var repository = new CatsRepositoryList(true);

            // Act
            var cat = repository.GetById(999);

            // Assert
            Assert.Null(cat);
        }

        [Fact]
        public void AddCat_AddsCatAndAssignsId()
        {
            // Arrange
            var repository = new CatsRepositoryList(true);
            var cat = new Cat
            {
                Name = "Luna",
                Age = 4
            };

            // Act
            var addedCat = repository.AddCat(cat);

            // Assert
            Assert.Equal(4, addedCat.Id);
            Assert.Equal("Luna", addedCat.Name);
            Assert.Equal(4, addedCat.Age);
        }

        [Fact]
        public void DeleteById_RemovesCat()
        {
            // Arrange
            var repository = new CatsRepositoryList(true);

            // Act
            var deletedCat = repository.DeleteById(1);
            var cat = repository.GetById(1);

            // Assert
            Assert.NotNull(deletedCat);
            Assert.Equal("Misser", deletedCat.Name);
            Assert.Null(cat);
        }

        [Fact]
        public void DeleteById_ReturnsNullForUnknownId()
        {
            // Arrange
            var repository = new CatsRepositoryList(true);

            // Act
            var deletedCat = repository.DeleteById(999);

            // Assert
            Assert.Null(deletedCat);
        }

        [Fact]
        public void UpdateCat_UpdatesExistingCat()
        {
            // Arrange
            var repository = new CatsRepositoryList(true);

            var updatedData = new Cat
            {
                Name = "Misser Updated",
                Age = 5
            };

            // Act
            var updatedCat = repository.UpdateCat(1, updatedData);

            // Assert
            Assert.NotNull(updatedCat);
            Assert.Equal(1, updatedCat.Id);
            Assert.Equal("Misser Updated", updatedCat.Name);
            Assert.Equal(5, updatedCat.Age);
        }

        [Fact]
        public void UpdateCat_ReturnsNullForUnknownId()
        {
            // Arrange
            var repository = new CatsRepositoryList(true);

            var updatedData = new Cat
            {
                Name = "Unknown",
                Age = 5
            };

            // Act
            var updatedCat = repository.UpdateCat(999, updatedData);

            // Assert
            Assert.Null(updatedCat);
        }

        [Fact]
        public void GetCats_FiltersByName()
        {
            // Arrange
            var repository = new CatsRepositoryList(true);

            // Act
            var cats = repository.GetCats(nameStartsWith: "M").ToList();

            // Assert
            Assert.Single(cats);
            Assert.Equal("Misser", cats[0].Name);
        }

        [Fact]
        public void GetCats_FiltersByMinimumAge()
        {
            // Arrange
            var repository = new CatsRepositoryList(true);

            // Act
            var cats = repository.GetCats(minAge: 2).ToList();

            // Assert
            Assert.Equal(1, cats.Count);
            Assert.Equal("Pus", cats[0].Name);
        }

        [Fact]
        public void GetCats_SortsByNameAscending()
        {
            // Arrange
            var repository = new CatsRepositoryList(true);

            // Act
            var cats = repository.GetCats(sortOrder: "name").ToList();

            // Assert
            Assert.Equal("Fido", cats[0].Name);
            Assert.Equal("Misser", cats[1].Name);
            Assert.Equal("Pus", cats[2].Name);
        }

        [Fact]
        public void GetCats_SortsByAgeDescending()
        {
            // Arrange
            var repository = new CatsRepositoryList(true);

            // Act
            var cats = repository.GetCats(sortOrder: "agedesc").ToList();

            // Assert
            Assert.Equal("Pus", cats[0].Name);
            Assert.Equal("Misser", cats[1].Name);
            Assert.Equal("Fido", cats[2].Name);
        }

        [Fact]
        public void GetCats_ThrowsExceptionForInvalidSortOrder()
        {
            // Arrange
            var repository = new CatsRepositoryList(true);

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                repository.GetCats(sortOrder: "invalid"));
        }
    }
}
