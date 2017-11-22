namespace RandomData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TodoEBMProjectTodoList")]
    public partial class TodoEBMProjectTodoList
    {
        [Key]
        public Guid PTLID { get; set; }

        public DateTime CreateDateTime { get; set; }

        [StringLength(100)]
        public string title { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public int CompleteRate { get; set; }

        public Guid PMID { get; set; }

        public virtual TodoEBMProjectMember TodoEBMProjectMember { get; set; }
    }
}
