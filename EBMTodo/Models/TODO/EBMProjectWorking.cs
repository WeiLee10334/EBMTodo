using EBMTodo.Models.Base.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace EBMTodo.Models.Todo
{
    [DataContract(Namespace = "")]
    [Table("TodoEBMProjectWorking")]
    public partial class EBMProjectWorking
    {
        public EBMProjectWorking()
        {

            CreateDateTime = DateTime.Now;
        }

        [Key, Column(Order = 0)]
        [DataMember(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "PWID")]
        public Guid PWID { set; get; }


        [DataMember(Order = 2)]
        [Display(Name = "工作時間")]
        public DateTime CreateDateTime { set; get; }

        [DataMember(Order = 3)]
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "工作內容")]
        public string Description { set; get; }

        [DataMember(Order = 4)]
        [Display(Name = "工作時數")]
        public decimal WokingHour { set; get; }

        [DataMember(Order = 5)]
        [Display(Name = "型態")]
        public WorkingType workingType { set; get; }

        

        [Required(AllowEmptyStrings =true)]
        public string Id { set; get; }
        public virtual ApplicationUser ApplicationUser { set; get; }

        public Guid PID { set; get; }
        public virtual EBMProject EBMProject { set;get;}

    }
}