using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Effort.Data.Configuration;
using Effort.Data.Entities;

namespace Effort.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext()
            : base("Name=LibraryContext")
        {


            Configuration.LazyLoadingEnabled = false;

            //System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<LibraryContext, Effort.Data.Migrations.Configuration>());
        }

        public LibraryContext(string connectionString)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        //This constructor will allow us to pass in the connection created by Effort. 
        public LibraryContext(DbConnection connection)
            : base(connection, true)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual IDbSet<RecipeCategory> RecipeCategories { get; set; }
        public virtual IDbSet<Recipe> Recipes { get; set; }
        public virtual IDbSet<IngredientCategory> IngredientCategories { get; set; }
        public virtual IDbSet<Ingredient> Ingredients { get; set; }
        public virtual IDbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public virtual IDbSet<MeasurementType> MeasurementTypes { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //one-to-many 
            modelBuilder.Entity<Recipe>()
                .HasRequired<RecipeCategory>(r => r.RecipeCategory) // Recipe entity requires RecipeCategory
                .WithMany(rc => rc.Recipes)
                .HasForeignKey(r => r.RecipeCategoryId); ; // RecipeCategory entity includes many Recipe entities

            modelBuilder.Entity<Ingredient>()
                .HasRequired<IngredientCategory>(r => r.IngredientCategory) // Recipe entity requires RecipeCategory
                .WithMany(rc => rc.Ingredients)
                .HasForeignKey(r => r.IngredientCategoryId); ; // RecipeCategory entity includes many Recipe entities



            modelBuilder.Entity<RecipeIngredient>().HasKey(ri => ri.RecipeIngredientId);
            
            modelBuilder.Entity<RecipeIngredient>()
                .HasRequired<Recipe>(ri => ri.Recipe) // Recipe Ingredient entity requires Recipe
                .WithMany(r => r.RecipeIngredients);

            modelBuilder.Entity<RecipeIngredient>()
               .HasRequired<Ingredient>(ri => ri.Ingredient) // Recipe Ingredient entity requires Recipe
               .WithMany(i => i.RecipeIngredients);


            modelBuilder.Entity<RecipeIngredient>()
                .HasRequired<MeasurementType>(ri => ri.MeasurementType) // Recipe Ingredient entity requires Measurement Type
                .WithMany(ri => ri.RecipeIngredients);




            modelBuilder.Entity<MeasurementType>().HasKey(mt => mt.MeasurementTypeId); 
            
            modelBuilder.Entity<MeasurementType>()
                .HasMany(mt => mt.RecipeIngredients)
                .WithRequired(mt => mt.MeasurementType)
                .HasForeignKey(mt => mt.MeasurementTypeId);


            //base.OnModelCreating(modelBuilder);
        }

    }




}
