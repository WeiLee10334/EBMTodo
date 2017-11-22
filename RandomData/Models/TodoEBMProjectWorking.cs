namespace RandomData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TodoEBMProjectWorking")]
    public partial class TodoEBMProjectWorking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PWID { get; set; }

        public DateTime CreateDateTime { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public decimal WokingHour { get; set; }

        [Required]
        [StringLength(128)]
        public string Id { get; set; }

        public Guid PID { get; set; }

        public int workingType { get; set; }

        [StringLength(100)]
        public string Target { get; set; }

        public DateTime RecordDateTime { get; set; }

        [StringLength(128)]
        public string LineUID { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual TodoEBMProject TodoEBMProject { get; set; }
    }
}
