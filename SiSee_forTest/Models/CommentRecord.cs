namespace SiSee_v1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommentRecord")]
    public partial class CommentRecord
    {
        [Key]
        public int commentrecord_ID { get; set; }

        public int spot_ID { get; set; }

        public int user_ID { get; set; }

        [Display(Name = "內容")]
        public string comment_context { get; set; }

        [StringLength(10)]
        [Display(Name = "評分")]
        public string comment_grade { get; set; }

        [Display(Name = "日期")]
        public DateTime? comment_date { get; set; }

        public virtual Spot Spot { get; set; }

        public virtual User User { get; set; }
    }
}
