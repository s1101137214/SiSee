namespace SiSee_v1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Class")]
    public partial class Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int class_ID { get; set; }

        [StringLength(10)]
        public string class_Name { get; set; }
    }
}
