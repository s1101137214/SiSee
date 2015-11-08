namespace SiSee_v1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SearchRecord")]
    public partial class SearchRecord
    {
        [Key]
        public int searchrecord_ID { get; set; }

        public int user_ID { get; set; }

        public int spot_ID { get; set; }

        public DateTime? search_date { get; set; }

        public virtual Spot Spot { get; set; }

        public virtual User User { get; set; }
    }
}
