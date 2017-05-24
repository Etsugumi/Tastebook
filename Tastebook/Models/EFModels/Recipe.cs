using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tastebook.Models.EFModels
{
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RecipeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        [Range(0, 2)]
        public int Difficulty { get; set; }

        [Required]
        [RegularExpression("[0-9]*")]
        public int DishSize { get; set; }

        [Required]
        public RecipeType RecipeType { get; set; }

        public bool? isCompleted { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Edited { get; set; }
        public string AuthorId { get; set; }

        public Recipe()
        {
            Created = DateTime.Now;
            Edited = DateTime.Now;
        }
    }

    public class RecipeIngredientMap
    {
        public int Id { get; set; }
        public Guid RecipeId { get; set; }
        public Guid IngredientId { get; set; }
    }

    public class RecipeCommentMap
    {
        public int Id { get; set; }
        public Guid RecipeId { get; set; }
        public Guid CommentId { get; set; }
    }
}