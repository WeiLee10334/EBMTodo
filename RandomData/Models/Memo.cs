namespace RandomData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Memo")]
    public partial class Memo
    {
        [Key]
        public int MID { get; set; }

        public DateTime CreateDateTime { get; set; }

        [StringLength(100)]
        public string Tag { get; set; }

        [StringLength(500)]
        public string Content { get; set; }

        public int memoType { get; set; }

        [StringLength(128)]
        public string LineUID { get; set; }

        public bool ProgressingFlag { get; set; }

        [Column("memo")]
        [StringLength(100)]
        public string memo1 { get; set; }
    }
}
