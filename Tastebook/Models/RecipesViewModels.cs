using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tastebook.Models.EFModels;

namespace Tastebook.Models
{
    public class ListViewModel
    {
        public List<Recipe> Recipes { get; set; }
    }

    public class AddRecipeViewModel
    {
        public Recipe Recipe { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}