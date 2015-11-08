namespace SiSee_v1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public User()
        {
            CommentRecord = new HashSet<CommentRecord>();
            FavoriteRecord = new HashSet<FavoriteRecord>();
            SearchRecord = new HashSet<SearchRecord>();
        }

        [Key]
        [Display(Name = "ID")]
        public int user_ID { get; set; }

        [StringLength(20)]
        [Display(Name = "FBID")]
        public string user_FBID { get; set; }

        [StringLength(100)]
        [Display(Name = "EMAIL")]
        public string user_email { get; set; }

        [StringLength(15)]
        [Display(Name = "電話")]
        public string user_tel { get; set; }

        [Display(Name = "生日")]
        public DateTime? user_birth { get; set; }

        [Display(Name = "姓名")]
        public string user_name { get; set; }

        [StringLength(255)]
        [Display(Name = "備註")]
        public string user_other { get; set; }

        public virtual ICollection<CommentRecord> CommentRecord { get; set; }

        public virtual ICollection<FavoriteRecord> FavoriteRecord { get; set; }

        public virtual ICollection<SearchRecord> SearchRecord { get; set; }
    }
}
