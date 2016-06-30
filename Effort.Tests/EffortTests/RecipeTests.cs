using System.Data.Entity;
using System.Linq;
using Effort.Data;
using Effort.Data.Database;
using Effort.Data.Entities;
using Effort.DataLoaders;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Effort.Tests
{
    [TestClass]
    public class RecipeTests
    {
        private LibraryContext _context;
        private Repository<Recipe> _recipeRepository;

        [TestInitialize]
        public void Initialize()
        {
            //Creates a DbConnection object that rely on an in-memory database instance that 
            //lives during the connection object lifecycle. If the connection object is disposed 
            //or garbage collected, then underlying database will be garbage collected too.
            //var connection = DbConnectionFactory.CreateTransient();
            var connection = DbConnectionFactory.CreateTransient(SetupTestData());

            _context = new LibraryContext(connection);
            _recipeRepository = new Repository<Recipe>(_context);
        }

        private IDataLoader SetupTestData()
        {
            var filePath = @"C:\Users\barehami\Documents\visual studio 2013\Projects\Effort.Data\Effort.Tests\Data\TestData";

            IDataLoader loader =
              new Effort.DataLoaders.CsvDataLoader(filePath);
 
            return loader;
        }


        [TestMethod]
        public void Effort_GetBook_WithNonExistingId_ReturnsNull()
        {
            // Arrange
            const int nonExistingId = 155;

            // Act
            var book = _recipeRepository.GetById(nonExistingId);

            // Assert
            book.Should().BeNull();
        }

        [TestMethod]
        public void Effort_GetRecipeContians_CorrectName()
        {
            // Arrange
            const string title = "Steak Pie";

            // Act
            var recipe =  _recipeRepository.GetById(5);

            // Assert
            recipe.Name.ShouldBeEquivalentTo(title);
        }

        [TestMethod]
        public void Effort_GetRecipe_ContainsCorrectIngredients()
        {
            // Arrange
            const string expectedIngredient = "Chicken";

            // Act
            var recipe = _recipeRepository.SearchFor(r => r.RecipeId == 1).Include("Ingredients").First();

            // Assert
            Assert.IsTrue(recipe.RecipeIngredients.Count > 0, "The ingredient count was 0");
            //recipe.Ingredients.Contains().ShouldBeEquivalentTo(expectedIngredient);
        }
    }
}
