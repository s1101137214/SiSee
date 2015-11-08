namespace SiSee_v1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomSpot")]
    public partial class CustomSpot
    {
        [Key]
        public int customspot_ID { get; set; }

        [Display(Name = "更新日期")]
        public DateTime customspot_updated { get; set; }

        public int user_ID { get; set; }

        public int spot_ID { get; set; }
    }
}
