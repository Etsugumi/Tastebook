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
    [Authorize]
    public class RecipeController : BaseController
    {
        [AllowAnonymous]
        public ActionResult RecipesList()
        {
            var model = new ListViewModel
            {
                Recipes = Db.Recipes.Where(r => r.isCompleted == true).ToList()
            };

            return View(model);
        }

        //-------------------------------------------------------------- SHOW RECIPE

        [AllowAnonymous]
        public ActionResult ShowRecipe(Guid id)
        {
            var recipe = Db.Recipes.Find(id);

            var ingredientMaps = Db.IngredientMaps.Where(im => im.RecipeId.Equals(id)).ToList();
            var ingredients = new List<Ingredient>();

            foreach (var map in ingredientMaps)
            {
                var ingredient = Db.Ingredients.Find(map.IngredientId);
                ingredients.Add(ingredient);
            }
            
            var commentsMaps = Db.CommentMaps.Where(im => im.RecipeId.Equals(id)).ToList();
            var comments = new List<Comment>();

            foreach (var map in commentsMaps)
            {
                var comment = Db.Comments.Find(map.CommentId);
                comments.Add(comment);
            }

            var model = new RecipeDetailsViewModel
            {
                Recipe = recipe,
                Ingredients = ingredients,
                Comments = comments
            };

            return View(model);
        }

        //-------------------------------------------------------------- ADD RECIPE

        public ActionResult AddRecipe()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRecipe(Recipe model)
        {
            if (ModelState.IsValid)
            {
                model.isCompleted = false;
                model.AuthorId = User.Identity.GetUserId();

                Db.Recipes.Add(model);
                Db.SaveChanges();

                return RedirectToAction("AddIngredient", new { recipeId = model.RecipeId });
            }

            return View(model);
        }

        //-------------------------------------------------------------- EDIT RECIPE

        public ActionResult EditRecipe(Guid? id)
        {
            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRecipe(Recipe model)
        {
            return HttpNotFound();
        }

        //-------------------------------------------------------------- DELETE RECIPE

        public ActionResult DeleteRecipe(Guid id)
        {
            var model = Db.Recipes.Find(id);

            if (model != null)
            {
                RemoveIngredients(id);
                RemoveComments(id);

                Db.Recipes.Remove(model);
                Db.SaveChanges();
            }

            return RedirectToAction("RecipesList");
        }

        private void RemoveComments(Guid recipeId)
        {
            var mappings = Db.CommentMaps.Where(m => m.RecipeId.Equals(recipeId)).ToList();

            foreach (var map in mappings)
            {
                var comment = Db.Comments.Find(map.CommentId);
                Db.Comments.Remove(comment);
                Db.CommentMaps.Remove(map);
                Db.SaveChanges();
            }
        }

        private void RemoveIngredients(Guid recipeId)
        {
            var mappings = Db.IngredientMaps.Where(m => m.RecipeId.Equals(recipeId)).ToList();

            foreach (var map in mappings)
            {
                var ingredient = Db.Ingredients.Find(map.IngredientId);
                Db.Ingredients.Remove(ingredient);
                Db.IngredientMaps.Remove(map);
                Db.SaveChanges();
            }
        }

        //-------------------------------------------------------------- COMPLETE RECIPE CREATION

        public ActionResult CompleteRecipe(Guid? id)
        {
            var recipe = Db.Recipes.Find(id);
            if (recipe == null)
                return HttpNotFound();

            recipe.isCompleted = true;
            recipe.Created = DateTime.Now;
            Db.Entry(recipe).State = EntityState.Modified;
            Db.SaveChanges();

            return RedirectToAction("RecipesList");
        }

        //-------------------------------------------------------------- ADD INGREDIENT

        public ActionResult AddIngredient(Guid recipeId)
        {
            var model = new IngredientViewModel
            {
                Recipe = Db.Recipes.Find(recipeId),
                RecipeId = recipeId,
                Ingredients = new List<Ingredient>()
            };

            Session["ingrBag"] = model;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddIngredient(IngredientViewModel model)
        {
            var item = Session["ingrBag"] as IngredientViewModel;

            if (ModelState.IsValid)
            {
                Db.Ingredients.Add(model.Ingredient);
                Db.SaveChanges();

                CreateIngredientMap(model);

                if (item.Ingredients.FirstOrDefault(x => x.Name.Equals(model.Ingredient.Name)) == null)
                {
                    item.Ingredients.Add(model.Ingredient);
                    item.Ingredient = null;
                }

            }

            Session["ingrBag"] = item;

            return View(item);
        }

        public void CreateIngredientMap(IngredientViewModel model)
        {
            RecipeIngredientMap map = new RecipeIngredientMap
            {
                RecipeId = model.RecipeId,
                IngredientId = model.Ingredient.IngredientId
            };

            Db.IngredientMaps.Add(map);
            Db.SaveChanges();
        }
    }
}