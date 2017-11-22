namespace RandomData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RBUnit")]
    public partial class RBUnit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RBUnit()
        {
            RBUnitUserManage = new HashSet<RBUnitUserManage>();
        }

        [Key]
        [StringLength(20)]
        public string UID { get; set; }

        [Required]
        [StringLength(100)]
        public string UnitName { get; set; }

        public DateTime CDAT { get; set; }

        [StringLength(20)]
        public string ZipCode { get; set; }

        [StringLength(200)]
        public string Address1 { get; set; }

        [StringLength(200)]
        public string Address2 { get; set; }

        public string PhoneNumber { get; set; }

        public string Fax { get; set; }

        public int UnitLevel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RBUnitUserManage> RBUnitUserManage { get; set; }
    }
}
