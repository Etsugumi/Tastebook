using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tastebook.Models.EFModels;

namespace Tastebook.Models
{
    public class ListViewModel
    {
        public List<Recipe> Recipes { get; set; }
    }

    public class IngredientViewModel
    {
        public List<Ingredient> Ingredients { get; set; }
        public Ingredient Ingredient { get; set; }
        public Recipe Recipe { get; set; }
        public Guid RecipeId { get; set; }
    }
}