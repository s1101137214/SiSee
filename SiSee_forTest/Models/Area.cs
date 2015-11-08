namespace SiSee_v1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Area")]
    public partial class Area
    {
        public Area()
        {
            Spot = new HashSet<Spot>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int area_ID { get; set; }

        [StringLength(10)]
        [Display(Name = "¤ÀÃþ")]
        public string area_Name { get; set; }

        public virtual ICollection<Spot> Spot { get; set; }
    }
}
