using ApplicationAuth.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAuth.Models.RequestModels.Post
{
    public class AdminDeleteCommentRequestModel
    {
        [Required(ErrorMessage = "Post id is required")]
        public int PostId { get; set; }
        [Required(ErrorMessage = "Comment id is required")]
        public int CommentId { get; set; }
    }
}
