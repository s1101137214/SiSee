namespace SiSee_v1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Spot")]
    public partial class Spot
    {
        public Spot()
        {
            CommentRecord = new HashSet<CommentRecord>();
            FavoriteRecord = new HashSet<FavoriteRecord>();
            SearchRecord = new HashSet<SearchRecord>();
        }

        [Key]
        [Display(Name = "ID")]
        public int spot_ID { get; set; }

        public int area_ID { get; set; }

        [Display(Name = "名稱")]
        public string spot_name { get; set; }

        [Display(Name = "聯絡電話")]
        public string spot_tel { get; set; }

        [Display(Name = "介紹")]
        public string spot_context { get; set; }

        [Display(Name = "開放時間")]
        public string spot_optimeS { get; set; }

        [Display(Name = "地址")]
        public string spot_add { get; set; }

        [Display(Name = "費用")]
        public string spot_fee { get; set; }

        [StringLength(5)]
        [Display(Name = "評分")]
        public string spot_score { get; set; }

        [Display(Name = "備註")]
        public string spot_other { get; set; }

        [Display(Name = "區域")]
        public string spot_loaction { get; set; }

        public virtual Area Area { get; set; }

        public virtual ICollection<CommentRecord> CommentRecord { get; set; }

        public virtual ICollection<FavoriteRecord> FavoriteRecord { get; set; }

        public virtual ICollection<SearchRecord> SearchRecord { get; set; }
    }
}
