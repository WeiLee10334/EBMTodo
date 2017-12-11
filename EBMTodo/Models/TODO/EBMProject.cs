using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace EBMTodo.Models.Todo
{
    [DataContract(Namespace = "")]
    [Table("TodoEBMProject")]
    public partial class EBMProject
    {
        public EBMProject()
        {
            CreateDateTime = DateTime.Now;
            EBMProjectMember = new HashSet<EBMProjectMember>();
            EBMProjectSchedule = new HashSet<EBMProjectSchedule>();
        }

        [Key, Column(Order = 0)]
        [DataMember(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "PID")]
        public Guid PID { set; get; }

        [Required]
        [DataMember(Order = 2)]
        [StringLength(100)]
        [Display(Name = "專案名稱")]
        public string ProjectName { set; get; }

        [DataMember(Order = 3)]
        [Display(Name = "CreateDateTime")]
        public DateTime CreateDateTime { set; get; }
        
        [DataMember(Order = 2)]
        [StringLength(100)]
        [Display(Name = "專案號")]
        public string ProjectNo { set; get; }

        [DataMember(Order = 4)]
        [Display(Name = "隱藏")]
        public bool IsHode { set; get; }

        public virtual ICollection<EBMProjectMember> EBMProjectMember { get; set; }
        public virtual ICollection<EBMProjectSchedule> EBMProjectSchedule { get; set; }

    }
}