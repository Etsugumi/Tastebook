using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tastebook.Models;
using Tastebook.Models.EFModels;

namespace Tastebook.Controllers
{
    [Authorize]
    public class CommentController : BaseController
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(RecipeDetailsViewModel model)
        {
            var comment = model.CommentDummy;            

            if (comment != null)
            {
                comment.AuthorName = User.Identity.Name;
                comment.Created = DateTime.Now;

                Db.Comments.Add(comment);
                Db.SaveChanges();

                CreateCommentRecipeMap(model.Recipe.RecipeId, comment.CommentId);
            }

            return RedirectToAction("ShowRecipe","Recipe", new { id = model.Recipe.RecipeId });
        }

        public void CreateCommentRecipeMap(Guid recipeId, Guid commentId)
        {
            var map = new RecipeCommentMap
            {
                RecipeId = recipeId,
                CommentId = commentId
            };

            Db.CommentMaps.Add(map);
            Db.SaveChanges();
        }
    }
}