using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tastebook.Models.EFModels
{
    public class RecipeImageMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid RecipeId { get; set; }
        public Guid ImageId { get; set; }
    }

    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ImageId { get; set; }
        public string UserId { get; set; }
        public DateTime? Uploaded { get; set; }        
    }
}