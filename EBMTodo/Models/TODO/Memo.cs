using EBMTodo.Models.Base.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace EBMTodo.Models.Todo
{
    [DataContract(Namespace = "")]
    [Table("Memo")]
    public partial class Memo
    {
        public Memo()
        {

            CreateDateTime = DateTime.Now;
        }

        [Key, Column(Order = 0)]
        [DataMember(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MID { set; get; }


        [DataMember(Order = 2)]
        [Display(Name = "紀錄時間")]
        public DateTime CreateDateTime { set; get; }

        [DataMember(Order = 3)]
        [StringLength(100)]
        [Display(Name = "標籤")]
        public string Tag { set; get; }

        [DataMember(Order = 3)]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "內容")]
        public string Content { set; get; }
        
        public bool ProgressingFlag { set; get; }

        public MemoType memoType { set; get; }

        [DataMember(Order = 7)]
        [StringLength(128)]
        [Display(Name = "LINE UID")]
        public string LineUID { set; get; }

        [StringLength(100)]
        [Display(Name = "註記")]
        public string memo { set; get; }


    }
}