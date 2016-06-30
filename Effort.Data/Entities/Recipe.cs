using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Effort.Data.Entities
{
    public class Recipe
    {
        public Recipe()
        {
            RecipeIngredients = new List<RecipeIngredient>();
        }

        [Key]
        public int RecipeId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int PrepTime { get; set; }
        public int CookingTime { get; set; }
        public decimal Cost { get; set; }

        public int RecipeCategoryId { get; set; }
        public virtual RecipeCategory RecipeCategory { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }


    }
}
