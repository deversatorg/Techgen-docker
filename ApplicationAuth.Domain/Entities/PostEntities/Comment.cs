using ApplicationAuth.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAuth.Domain.Entities.PostEntities
{
    public class Comment : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int PostId { get; set; }

        public int UserId { get; set; }

        [DefaultValue("")]
        public string Text { get; set; }

        [DataType("DateTime")]
        public DateTime CreatedAt { get; set; }

        public int? ParentCommentId { get; set; }

        #region Navigation properties

        public ICollection<Comment> Answers { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Comments")]
        public ApplicationUser User { get; set; }

        [ForeignKey("PostId")]
        [InverseProperty("Comments")]
        public Post Post { get; set; }

        #endregion
    }
}
