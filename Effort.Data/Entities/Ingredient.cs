using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Effort.Data.Entities
{
    public class Ingredient
    {
        public Ingredient()
        {
            RecipeIngredients = new List<RecipeIngredient>();
        }

        [Key]
        public int IngredientId { get; set; }

        public string Name { get; set; }

        public int IngredientCategoryId { get; set; }
        public virtual IngredientCategory IngredientCategory { get; set; }

        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
