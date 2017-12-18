namespace RandomData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TodoEBMProject")]
    public partial class TodoEBMProject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TodoEBMProject()
        {
            TodoEBMProjectMember = new HashSet<TodoEBMProjectMember>();
            TodoEBMProjectSchedule = new HashSet<TodoEBMProjectSchedule>();
            TodoEBMProjectWorking = new HashSet<TodoEBMProjectWorking>();
        }

        [Key]
        public Guid PID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProjectName { get; set; }

        public DateTime CreateDateTime { get; set; }

        [StringLength(100)]
        public string ProjectNo { get; set; }

        public bool IsHode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TodoEBMProjectMember> TodoEBMProjectMember { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TodoEBMProjectSchedule> TodoEBMProjectSchedule { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TodoEBMProjectWorking> TodoEBMProjectWorking { get; set; }
    }
}
