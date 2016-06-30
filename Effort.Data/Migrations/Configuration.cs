using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using CsvHelper;
using Effort.Data.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;


namespace Effort.Data.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<Effort.Data.LibraryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Effort.Data.LibraryContext context)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            List<Recipe> recipesList = new List<Recipe>();
            List<RecipeIngredient> recipeIngredientsList = new List<RecipeIngredient>();
            List<Ingredient> ingredientList = new List<Ingredient>();


            //Measruement Types
            string resourceName = "Effort.Data.Domain.SeedData.MeasurementTypes.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.WillThrowOnMissingField = false;
                    var measurementType = csvReader.GetRecords<MeasurementType>().ToArray();
                    context.MeasurementTypes.AddOrUpdate(i => i.MeasurementTypeId, measurementType);
                }
            }


            //Ingredient Categories
            resourceName = "Effort.Data.Domain.SeedData.IngredientCategories.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.WillThrowOnMissingField = false;
                    var ingredientCategory = csvReader.GetRecords<IngredientCategory>().ToArray();
                    context.IngredientCategories.AddOrUpdate(i => i.IngredientCategoryId, ingredientCategory);
                }
            }





            //Ingredients
            resourceName = "Effort.Data.Domain.SeedData.Ingredients.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.WillThrowOnMissingField = false;
                    while (csvReader.Read())
                    {
                        var ingredient = csvReader.GetRecord<Ingredient>();
                        var ingredientCategoryId = Convert.ToInt16(csvReader.GetField<string>("IngredientCategoryId"));
                        ingredient.IngredientCategory = context.IngredientCategories.Local.Single(c => c.IngredientCategoryId == ingredientCategoryId);
                        ingredientList.Add(ingredient);
                        context.Ingredients.AddOrUpdate(p => p.IngredientId, ingredient);
                    }
                }
            }



            //Recipe Categories            
            resourceName = "Effort.Data.Domain.SeedData.RecipeCategories.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.WillThrowOnMissingField = false;
                    var recipeCategory = csvReader.GetRecords<RecipeCategory>().ToArray();
                    context.RecipeCategories.AddOrUpdate(i => i.RecipeCategoryId, recipeCategory);


                }
            }

            //Recipes
            resourceName = "Effort.Data.Domain.SeedData.Recipes.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.WillThrowOnMissingField = false;
                    while (csvReader.Read())
                    {
                        var recipe = csvReader.GetRecord<Recipe>();
                        var recipeCategoryId = Convert.ToInt16(csvReader.GetField<string>("RecipeCategoryId"));
                        recipe.RecipeCategoryId = context.RecipeCategories.Local.Single(c => c.RecipeCategoryId == recipeCategoryId).RecipeCategoryId; 
                        //recipesList.Add(recipe);
                        context.Recipes.AddOrUpdate(p => p.RecipeId, recipe);
                    }
                }
            }
            //recipesList.ForEach(r => context.Recipes.AddOrUpdate(x => x.RecipeId, r));
            //context.SaveChanges();




            //////Recipe Ingredients
            //resourceName = "Effort.Data.Domain.SeedData.RecipeIngredients.csv";
            //using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            //{
            //    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            //    {

            //        CsvReader csvReader = new CsvReader(reader);
            //        csvReader.Configuration.WillThrowOnMissingField = false;
            //        while (csvReader.Read())
            //        {
            //            var recipeIngredient = csvReader.GetRecord<RecipeIngredient>();
            //            recipeIngredient.Recipe = recipesList.Single(r => r.RecipeId == recipeIngredient.RecipeId);
            //            recipeIngredient.Ingredient = ingredientList.Single(i => i.IngredientId == recipeIngredient.IngredientId);
            //            context.RecipeIngredients.AddOrUpdate(ri => ri.RecipeId, recipeIngredient);
            //            //recipeIngredientsList.Add(recipeIngredient);
            //        }

            //    }
            //}
            //recipeIngredientsList.ForEach(ri => context.RecipeIngredients.AddOrUpdate(x => x.RecipeIngredientId == ri.RecipeIngredientId));
            context.SaveChanges();

            //foreach (var VARIABLE in COLLECTION)
            //{
            //    var recipeIngredient = csvReader.GetRecord<RecipeIngredient>();

            //    var recipe = recipesList.Single(r => r.RecipeId == recipeIngredient.RecipeId);
            //    recipe.RecipeIngredients.Add(recipeIngredient);   
            //}


            //foreach (Recipe recipe in recipesList)
            //{
            //    context.Recipes.AddOrUpdate(recipe);
            //}
            //context.SaveChanges();

       
        }

        //protected override void Seed(Effort.Data.LibraryContext context)
        //{
        //    //  This method will be called after migrating to the latest version.

        //    //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
        //    //  to avoid creating duplicate seed data. E.g.
        //    //
        //    //    context.People.AddOrUpdate(
        //    //      p => p.FullName,
        //    //      new Person { FullName = "Andrew Peters" },
        //    //      new Person { FullName = "Brice Lambson" },
        //    //      new Person { FullName = "Rowan Miller" }
        //    //    );
        //    //
        //}
    }
}
