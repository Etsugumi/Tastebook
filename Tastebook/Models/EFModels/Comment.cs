using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tastebook.Models.EFModels
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CommentId { get; set; }

        public DateTime? Created { get; set; }
        public string Text { get; set; }
        public string AuthorName { get; set; }

        public Comment()
        {
            Created = DateTime.Now;
        }
    }
}