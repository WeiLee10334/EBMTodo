using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace EBMTodo.Models.Todo
{
    [DataContract(Namespace = "")]
    [Table("TodoEBMProjectMember")]
    public partial class EBMProjectMember
    {
        public EBMProjectMember()
        {

            CreateDateTime = DateTime.Now;
            EBMProjectTodoList = new HashSet<EBMProjectTodoList>();
        }

        [Key, Column(Order = 0)]
        [DataMember(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "PMID")]
        public Guid PMID { set; get; }


        [DataMember(Order = 2)]
        [Display(Name = "加入時間")]
        public DateTime CreateDateTime { set; get; }

        [DataMember(Order = 3)]
        [StringLength(100)]
        [Display(Name = "職稱")]
        public string title { set; get; }

        public string Id { set; get; }
        public virtual ApplicationUser ApplicationUser { set; get; }
        public Guid PID { set; get; }
        public virtual EBMProject EBMProject { set;get;}

        public virtual ICollection<EBMProjectTodoList> EBMProjectTodoList { get; set; }

    }
}