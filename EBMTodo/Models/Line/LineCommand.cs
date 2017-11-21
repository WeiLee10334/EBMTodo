using EBMTodo.Models.Base.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace EBMTodo.Models.Todo
{
    [DataContract(Namespace = "")]
    [Table("Line_Command")]
    public partial class LineCommand
    {
        public LineCommand()
        {
            CreateDateTime = DateTime.Now;
        }

        [Key]
        [DataMember(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "PID")]
        public Guid LCID { set; get; }

        [Index]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IssueNo { set; get; }

        [Display(Name = "UID")]
        [StringLength(128)]
        public string UserID { get; set; }

        [Display(Name = "Line Name")]
        [StringLength(128)]
        public string UserName { get; set; }

        [Display(Name = "GroupID")]
        [StringLength(128)]
        public string GroupID { get; set; }

        [Display(Name = "RoomId")]
        [StringLength(128)]
        public string RoomId { get; set; }

        [DataMember(Order = 2)]
        [Display(Name = "CreateDateTime")]
        public DateTime CreateDateTime { set; get; }

        [StringLength(5)]
        public string Command { get; set; }

        [Display(Name = "輸入訊息")]
        [StringLength(2000)]
        public string Message { get; set; }


        [Display(Name = "指定人")]
        [StringLength(100)]
        public string assignUserName { get; set; }

        [StringLength(128)]
        public string ReplyToken { get; set; }
        [StringLength(50)]
        public string LineType { get; set; }
        public CommandType CommandType { get; set; }
        public CommandStatus CommandStatus { get; set; }
    }
}