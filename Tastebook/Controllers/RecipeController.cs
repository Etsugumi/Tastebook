using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
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
                Comments = comments,
                CanLike = CanLike(recipe.RecipeId),
                Likes = Db.Likes.Count(l => l.RecipeId.Equals(recipe.RecipeId))
            };

            return View(model);
        }

        private bool CanLike(Guid recipeId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var likes = Db.Likes.FirstOrDefault(l => l.UserId.Equals(userId) && l.RecipeId.Equals(recipeId));

                if (likes == null)
                    return true;
            }

            return false;
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
                RemoveLikes(id);

                Db.Recipes.Remove(model);
                Db.SaveChanges();
            }

            return RedirectToAction("RecipesList");
        }

        private void RemoveLikes(Guid recipeId)
        {
            var likesToRemove = Db.Likes.Where(l => l.RecipeId.Equals(recipeId)).ToList();

            foreach (var like in likesToRemove)
            {
                Db.Likes.Remove(like);
                Db.SaveChanges();
            }
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

        //-------------------------------------------------------------- LIKE RECIPE

        public ActionResult LikeRecipe(Guid recipeId)
        {
            var like = new Like
            {
                UserId = User.Identity.GetUserId(),
                RecipeId = recipeId
            };

            Db.Likes.Add(like);
            Db.SaveChanges();

            return RedirectToAction("ShowRecipe", new { id = recipeId });
        }

        //-------------------------------------------------------------- DISLIKE RECIPE

        public ActionResult DisLikeRecipe(Guid recipeId)
        {
            var userId = User.Identity.GetUserId();
            var like = Db.Likes.FirstOrDefault(l => l.RecipeId.Equals(recipeId) && l.UserId.Equals(userId));

            Db.Likes.Remove(like);
            Db.SaveChanges();

            return RedirectToAction("ShowRecipe", new { id = recipeId });
        }

        //-------------------------------------------------------------- ADD INGREDIENT

        public ActionResult AddIngredient(Guid recipeId)
        {
            var imagesMaps = Db.ImagesMaps.Where(im => im.RecipeId.Equals(recipeId)).ToList();
            var images = new List<Image>();

            foreach (var map in imagesMaps)
            {
                var image = Db.Images.Find(map.ImageId);
                if (image != null)
                    images.Add(image);
            }

            var ingredientsMaps = Db.IngredientMaps.Where(im => im.RecipeId.Equals(recipeId)).ToList();
            var ingredients = new List<Ingredient>();

            foreach (var map in ingredientsMaps)
            {
                var ing = Db.Ingredients.Find(map.IngredientId);
                if (ing != null)
                    ingredients.Add(ing);
            }

            var model = new IngredientViewModel
            {
                Recipe = Db.Recipes.Find(recipeId),
                RecipeId = recipeId,
                Ingredients = ingredients,
                Images = images
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddIngredient(IngredientViewModel model)
        {
            if (ModelState.IsValid)
            {
                Db.Ingredients.Add(model.Ingredient);
                Db.SaveChanges();

                CreateIngredientMap(model);
            }

            return RedirectToAction("AddIngredient", new { recipeId = model.RecipeId });
        }

        public ActionResult RemoveIngredient(Guid id, Guid recipeId)
        {
            var ingredient = Db.Ingredients.Find(id);

            if (ingredient == null)
                return HttpNotFound();

            var ingedientMap = Db.IngredientMaps.FirstOrDefault(im => im.IngredientId.Equals(id));

            if (ingedientMap == null)
                return HttpNotFound();

            Db.Ingredients.Remove(ingredient);
            Db.IngredientMaps.Remove(ingedientMap);
            Db.SaveChanges();

            return RedirectToAction("AddIngredient", new { recipeId = recipeId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FileUpload(IngredientViewModel model)
        {
            var isSavedSuccessfully = true;
            var message = "";
            var fName = Guid.Empty;

            try
            {
                foreach (string fileName in Request.Files)
                {
                    var file = Request.Files[fileName];
                    fName = Guid.NewGuid();

                    if (file != null && file.ContentLength > 0)
                    {
                        var fExtension = Path.GetExtension(file.FileName);
                        var pathString = Server.MapPath(_ImageUploadPath);
                        var fileExists = System.IO.Directory.Exists(pathString);
                        var fullName = fName + fExtension;

                        if (!fileExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        file.SaveAs(pathString + "\\" + fullName);

                        var recipeId = model.RecipeId;

                        var image = new Image
                        {
                            Name = fName.ToString(),
                            FullName = fullName,
                            Extension = fExtension,
                            Uploaded = DateTime.Now,
                            UserId = User.Identity.GetUserId()
                        };

                        Db.Images.Add(image);
                        Db.SaveChanges();

                        var imageMap = new RecipeImageMap
                        {
                            RecipeId = recipeId,
                            ImageId = image.ImageId
                        };

                        Db.ImagesMaps.Add(imageMap);
                        Db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                isSavedSuccessfully = false;
            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = message });
            }
        }

        public ActionResult RemoveFile(Guid id, Guid recipeId)
        {
            var image = Db.Images.Find(id);

            if (image == null)
                return HttpNotFound();

            var imageMap = Db.ImagesMaps.FirstOrDefault(im => im.ImageId.Equals(id));

            if (imageMap == null)
                return HttpNotFound();

            var pathString = _ImageUploadPath + "\\" + image.FullName;
            var path = Server.MapPath(pathString);

            System.IO.File.Delete(path);

            Db.Images.Remove(image);
            Db.ImagesMaps.Remove(imageMap);
            Db.SaveChanges();

            return RedirectToAction("AddIngredient", new { recipeId = recipeId });
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