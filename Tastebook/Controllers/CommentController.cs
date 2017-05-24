using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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
                comment.AuthorId = User.Identity.GetUserId();
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

        public ActionResult DeleteComment(Guid commentId, string returnUrl)
        {
            var comment = Db.Comments.Find(commentId);
            var map = Db.CommentMaps.FirstOrDefault(m => m.CommentId.Equals(commentId));

            Db.Comments.Remove(comment);
            Db.CommentMaps.Remove(map);
            Db.SaveChanges();

            return Redirect(returnUrl);
        }
    }
}