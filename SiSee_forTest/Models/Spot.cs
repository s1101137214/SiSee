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

        [Display(Name = "�W��")]
        public string spot_name { get; set; }

        [Display(Name = "�p���q��")]
        public string spot_tel { get; set; }

        [Display(Name = "����")]
        public string spot_context { get; set; }

        [Display(Name = "�}��ɶ�")]
        public string spot_optimeS { get; set; }

        [Display(Name = "�a�}")]
        public string spot_add { get; set; }

        [Display(Name = "�O��")]
        public string spot_fee { get; set; }

        [StringLength(5)]
        [Display(Name = "����")]
        public string spot_score { get; set; }

        [Display(Name = "�Ƶ�")]
        public string spot_other { get; set; }

        [Display(Name = "�ϰ�")]
        public string spot_loaction { get; set; }

        public virtual Area Area { get; set; }

        public virtual ICollection<CommentRecord> CommentRecord { get; set; }

        public virtual ICollection<FavoriteRecord> FavoriteRecord { get; set; }

        public virtual ICollection<SearchRecord> SearchRecord { get; set; }
    }
}
