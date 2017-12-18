namespace RandomData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TodoEBMProjectOnline")]
    public partial class TodoEBMProjectOnline
    {
        [Key]
        public Guid POID { get; set; }

        public DateTime CreateDateTime { get; set; }

        public DateTime ApplyDateTime { get; set; }

        [StringLength(100)]
        public string ApplyName { get; set; }

        [StringLength(100)]
        public string title { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public int CompleteRate { get; set; }

        public int OnlineCategories { get; set; }

        [StringLength(500)]
        public string Memo { get; set; }

        public Guid PMID { get; set; }

        public DateTime? HandleDateTime { get; set; }

        public DateTime? ResolveDateTime { get; set; }

        [StringLength(100)]
        public string ResponseName { get; set; }

        [StringLength(100)]
        public string HandleName { get; set; }

        public string ApplyDepartment { get; set; }

        public virtual TodoEBMProjectMember TodoEBMProjectMember { get; set; }
    }
}
