using System.Data.Entity.ModelConfiguration;
using Effort.Data.Entities;

namespace Effort.Data.Configuration
{
    public class IngredientCategoriesEntityConfiguration : EntityTypeConfiguration<IngredientCategory>
    {
        public IngredientCategoriesEntityConfiguration()
        {
            HasMany(e => e.Ingredients)  // IngredientCategory entity includes many ingredient entities
                 .WithRequired(e => e.IngredientCategory) // Ingredient required ingredient category
                 .HasForeignKey(e => e.IngredientCategoryId)
                 .WillCascadeOnDelete(false);

        }
    }
}
