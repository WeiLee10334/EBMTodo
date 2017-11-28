using EBMTodo.Models.Base.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace EBMTodo.Models.Todo
{
    [DataContract(Namespace = "")]
    [Table("TodoEBMProjectSchedule")]
    public partial class EBMProjectSchedule
    {
        public EBMProjectSchedule()
        {

            CreateDateTime = DateTime.Now;
        }

        [Key, Column(Order = 0)]
        [DataMember(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "PSID")]
        public Guid PSID { set; get; }


        [DataMember(Order = 2)]
        [Display(Name = "產生時間")]
        public DateTime CreateDateTime { set; get; }

        [DataMember(Order = 3)]
        [Display(Name = "行程時間")]
        public DateTime ScheduleDateTime { set; get; }

        [DataMember(Order = 4)]
        [StringLength(100)]
        [Display(Name = "行程地點")]
        public string Target { set; get; }

        [DataMember(Order = 5)]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "行程內容")]
        public string Description { set; get; }

        [DataMember(Order = 6)]
        [Display(Name = "預期時數")]
        public decimal WokingHour { set; get; }

        [DataMember(Order = 7)]
        [Display(Name = "型態")]
        public ScheduleType scheduleType { set; get; }

        [DataMember(Order = 8)]
        [Display(Name = "完成時間")]
        public DateTime FinishDateTime { set; get; }

        [DataMember(Order = 9)]
        [StringLength(128)]
        [Display(Name = "LINE UID")]
        public string LineUID { set; get; }

        [DataMember(Order = 10)]
        [StringLength(100)]
        [Display(Name = "行程Title")]
        public string Title { set; get; }

        
        public bool ProgressingFlag { set; get; }

        public string Id { set; get; }
        public virtual ApplicationUser ApplicationUser { set; get; }

        public Guid PID { set; get; }
        public virtual EBMProject EBMProject { set;get;}

    }
}