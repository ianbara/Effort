using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Effort.Data.Entities
{
    public class IngredientCategory
    {
        [Key]
        public int IngredientCategoryId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; } 
    }
}
