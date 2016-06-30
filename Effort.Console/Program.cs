using System;
using System.Data.Entity;
using System.Linq;
using Effort.Data;
using Effort.Data.Database;
using Effort.Data.Entities;

namespace Effort.Console
{
    class Program
    {

        static void Main(string[] args)
        {

                DisplayRecipes();
                DisplayMeasurementTypes();


                System.Console.WriteLine("<Press any key to close>");

              
                //ctx.SaveChanges();
                System.Console.ReadLine();
          


        }

        public static void DisplayRecipes()
        {
            using (var ctx = new LibraryContext())
            {
                //Ingredient ingredient = new Student() { StudentName = "New Student" };
                Repository<Recipe> _recipeRepository = new Repository<Recipe>(ctx);
                var recipes = _recipeRepository.GetAll().Include("RecipeCategory").ToList(); //.Include("RecipeIngredients.MeasurmentType").ToList();
                //var recipes = _recipeRepository.GetAll().Include("RecipeCategory").ToList(); //.Include("RecipeIngredients.Ingredient").Include("RecipeIngredients.MeasurmentType").ToList();

                System.Console.WriteLine("Reading Recipes...");

                if (recipes.Any())
                {

                    foreach (var recipe in recipes)
                    {
                        System.Console.WriteLine("-----------------------------------------------------------------");
                        System.Console.WriteLine("Recipe : {0}, Category : {1},Ingredients No {2}", recipe.Name, recipe.RecipeCategory.Name, recipe.RecipeIngredients.Count());
                        System.Console.WriteLine("Ingredients");
                        foreach (RecipeIngredient recipeIngredient in recipe.RecipeIngredients)
                        {
                            System.Console.WriteLine(string.Format("{0} {1}", recipeIngredient.Quantity, recipeIngredient.Ingredient.Name));
                        }

                        System.Console.WriteLine("-----------------------------------------------------------------");

                    }

                }
                else
                {
                    System.Console.WriteLine("(0) Recipes found.");

                }

                System.Console.WriteLine("<Press any key to continue>");
                System.Console.ReadLine();
             }
        }

        public static void DisplayMeasurementTypes()
        {
            System.Console.WriteLine("Reading MeasurementTypes...");

            using (var ctx = new LibraryContext())
            {

                Repository<MeasurementType> _repository = new Repository<MeasurementType>(ctx);
                var measurementTypes = _repository.GetAll().ToList();

                foreach (var measruementType in measurementTypes)
                {
                    System.Console.WriteLine("{0} - {1}, {2}", measruementType.MeasurementTypeId, measruementType.Name, measruementType.Description);
                }

                System.Console.WriteLine("<Press any key to conttinue>");
                System.Console.ReadLine();
            }

        }

    }
}
