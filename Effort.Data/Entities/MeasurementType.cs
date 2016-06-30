using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Effort.Data.Entities
{
    public class MeasurementType
    {
        [Key]
        public int MeasurementTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Disabled { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
