namespace SiSee_v1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FavoriteRecord")]
    public partial class FavoriteRecord
    {
        [Key]
        [Display(Name = "½s¸¹")]
        public int favoriterecord_ID { get; set; }
        [Display(Name = "´ºÂI")]
        public int spot_ID { get; set; }

        public int user_ID { get; set; }

        public virtual Spot Spot { get; set; }

        public virtual User User { get; set; }
    }
}
