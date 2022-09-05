using System.ComponentModel.DataAnnotations;

namespace Tutoronic.Models
{
    public partial class Comment_reply
    {
        [Key]
        public int Comment_reply_id { get; set; }

        public int C_V_C_fid { get; set; }

        public int teacher_fid { get; set; }

        [Required]
        [StringLength(10)]
        public string reply { get; set; }

        public virtual Course_video_comment Course_video_comment { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
