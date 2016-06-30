using System.Data.Entity.ModelConfiguration;
using Effort.Data.Entities;

namespace Effort.Data.Configuration
{
    public class RecipeCategoriesEntityConfiguration : EntityTypeConfiguration<RecipeCategory>
    {
        public RecipeCategoriesEntityConfiguration()
        {
            HasMany(e => e.Recipes) // RecipeCategory entity includes many recipe entities
             .WithRequired(e => e.RecipeCategory) // Student entity requires Standard 
             .HasForeignKey(e => e.RecipeCategoryId)
             .WillCascadeOnDelete(false);

        }
    }
}
