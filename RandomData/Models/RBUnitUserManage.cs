namespace RandomData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RBUnitUserManage")]
    public partial class RBUnitUserManage
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string UID { get; set; }

        [Key]
        [Column(Order = 1)]
        public string ApplicationUserId { get; set; }

        public virtual RBUnit RBUnit { get; set; }
    }
}
