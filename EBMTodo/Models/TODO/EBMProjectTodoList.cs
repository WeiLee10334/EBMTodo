using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace EBMTodo.Models.Todo
{
    [DataContract(Namespace = "")]
    [Table("TodoEBMProjectTodoList")]
    public partial class EBMProjectTodoList
    {
        public EBMProjectTodoList()
        {
            CompleteRate = 0;
            CreateDateTime = DateTime.Now;
        }

        [Key, Column(Order = 0)]
        [DataMember(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "PTLID")]
        public Guid PTLID { set; get; }


        [DataMember(Order = 2)]
        [Display(Name = "加入時間")]
        public DateTime CreateDateTime { set; get; }

        [DataMember(Order = 3)]
        [StringLength(100)]
        [Display(Name = "標題")]
        public string title { set; get; }

        [DataMember(Order = 3)]
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "工作內容")]
        public string Description { set; get; }


        [DataMember(Order = 4)]
        [Range(0,100)]
        [Display(Name = "完成百分比")]
        public int CompleteRate { set; get; }


        public Guid PMID { set; get; }
        public virtual EBMProjectMember EBMProjectMember { set; get; }

    }
}