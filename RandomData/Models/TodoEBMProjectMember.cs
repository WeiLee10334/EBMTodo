namespace RandomData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TodoEBMProjectMember")]
    public partial class TodoEBMProjectMember
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TodoEBMProjectMember()
        {
            TodoEBMProjectOnline = new HashSet<TodoEBMProjectOnline>();
            TodoEBMProjectTodoList = new HashSet<TodoEBMProjectTodoList>();
        }

        [Key]
        public Guid PMID { get; set; }

        public DateTime CreateDateTime { get; set; }

        [StringLength(100)]
        public string title { get; set; }

        [StringLength(128)]
        public string Id { get; set; }

        public Guid PID { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual TodoEBMProject TodoEBMProject { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TodoEBMProjectOnline> TodoEBMProjectOnline { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TodoEBMProjectTodoList> TodoEBMProjectTodoList { get; set; }
    }
}
