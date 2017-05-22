using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Tastebook.Models;
using Tastebook.Models.EFModels;

namespace Tastebook.Controllers
{
    public class RecipeController : BaseController
    {
        public ActionResult RecipesList()
        {
            var model = new ListViewModel
            {
                Recipes = Db.Recipes.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddIngredient(Ingredient ingredient)
        {
            return HttpNotFound();
        }

        [Authorize]
        public ActionResult AddRecipe()
        {
            return View(Session["recipe"]);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRecipe(AddRecipeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var recipe = model.Recipe;
                recipe.Created = DateTime.Now;
                recipe.Edited = DateTime.Now;
                recipe.AuthorId = User.Identity.GetUserId();

                Db.Recipes.Add(recipe);
                Db.SaveChanges();

                return RedirectToAction("RecipesList");
            }

            Session["recipe"] = model;

            return View(model);
        }

        [Authorize]
        public ActionResult EditRecipe(Guid? id)
        {
            var recipe = Db.Recipes.Find(id);

            if (recipe == null) return HttpNotFound();

            var model = new AddRecipeViewModel
            {
                Recipe = recipe
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRecipe(AddRecipeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var recipe = model.Recipe;

                Db.Entry(recipe).State = EntityState.Modified;
                Db.SaveChanges();

                return RedirectToAction("RecipesList");
            }

            return View(model);
        }

        [Authorize]
        public ActionResult DeleteRecipe(Guid id)
        {
            var model = Db.Recipes.Find(id);

            if (model != null)
            {
                Db.Recipes.Remove(model);
                Db.SaveChanges();
            }

            return RedirectToAction("RecipesList");
        }
    }
}