using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Cache;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Effort.Data.Entities
{
    public class RecipeIngredient
    {
        [Key]
        public int RecipeIngredientId { get; set; }

        public float Quantity { get; set; }

        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }

        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }

        public int MeasurementTypeId { get; set; }
        public virtual MeasurementType MeasurementType { get; set; }

    }
}
