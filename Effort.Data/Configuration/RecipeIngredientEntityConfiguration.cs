using System.Data.Entity.ModelConfiguration;
using Effort.Data.Entities;

namespace Effort.Data.Configuration
{
    public class RecipeIngredientEntityConfiguration : EntityTypeConfiguration<RecipeIngredient>
    {
        public RecipeIngredientEntityConfiguration()
        {

            HasRequired(ri => ri.Recipe)
                  .WithMany(x => x.RecipeIngredients)
                  .HasForeignKey(x => x.RecipeIngredientId);

            HasRequired(e => e.Ingredient);

        }
    }
}
