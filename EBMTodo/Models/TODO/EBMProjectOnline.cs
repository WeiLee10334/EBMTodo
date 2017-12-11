using EBMTodo.Models.Base.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace EBMTodo.Models.Todo
{
    [DataContract(Namespace = "")]
    [Table("TodoEBMProjectOnline")]
    public partial class EBMProjectOnline
    {
        public EBMProjectOnline()
        {

        }

        [Key, Column(Order = 0)]
        [DataMember(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "POID")]
        public Guid POID { set; get; }


        [DataMember(Order = 2)]
        [Display(Name = "加入時間")]
        public DateTime CreateDateTime { set; get; }

        [DataMember(Order = 2)]
        [Display(Name = "提出時間")]
        public DateTime ApplyDateTime { set; get; }

        [DataMember(Order = 3)]
        [StringLength(100)]
        [Display(Name = "申請者")]
        public string ApplyName { set; get; }


        [DataMember(Order = 3)]
        [StringLength(100)]
        [Display(Name = "標題")]
        public string title { set; get; }

        [DataMember(Order = 3)]
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "內容")]
        public string Description { set; get; }


        [DataMember(Order = 4)]
        [Range(0,100)]
        [Display(Name = "優先順序")]
        public int CompleteRate { set; get; }

        [DataMember(Order = 3)]
        [Display(Name = "分類")]
        public OnlineCategories OnlineCategories { set; get; }

        [DataMember(Order = 3)]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "駐記")]
        public string Memo { set; get; }

        public Guid PMID { set; get; }
        public virtual EBMProjectMember EBMProjectMember { set; get; }

    }
}