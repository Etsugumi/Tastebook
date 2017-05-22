using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tastebook.Models.EFModels
{
    public class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IngredientId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Amount { get; set; }

        public Guid RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}