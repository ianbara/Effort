using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Effort.Data;
using Effort.Data.Database;
using Effort.Data.Entities;
using Effort.DataLoaders;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Effort.Tests
{
    [TestClass]
    public class RecipeIntegrationTests
    {

        private LibraryContext _context;
        private Repository<Recipe> _recipeRepository;


        [TestInitialize]
        public void SetupTest()
        {
            // file path of the database to create
            var filePath = @"C:\Users\barehami\Documents\visual studio 2013\Projects\Effort.Data\Effort.Tests\Data\RealMyAppDb.sdf";

            // delete it if it already exists
            if (File.Exists(filePath))
                File.Delete(filePath);

            // create the SQL CE connection string - this just points to the file path
            string connectionString = "Datasource = " + filePath;

            // NEED TO SET THIS TO MAKE DATABASE CREATION WORK WITH SQL CE!!!
            Database.DefaultConnectionFactory =
                new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            using (var context = new LibraryContext(connectionString))
            {
                // this will create the database with the schema from the Entity Model
                context.Database.Create();
            }

            // initialise our DbContext class with the SQL CE connection string, 
            // ready for our tests to use it.
            _context = new LibraryContext(connectionString);
            _recipeRepository = new Repository<Recipe>(_context);



        }

        

        [TestMethod]
        public void SQLCE_GetBook_WithNonExistingId_ReturnsNull()
        {
            // Arrange
            const int nonExistingId = 155;

            // Act
            var recipe = _recipeRepository.GetById(nonExistingId);

            // Assert
            recipe.Should().BeNull();
        }
    }
}
