using EBMTodo.Models.Base.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace EBMTodo.Models.Todo
{
    [DataContract(Namespace = "")]
    [Table("Line_Group")]
    public partial class LineGroup
    {
        public LineGroup()
        {
            CreateDateTime = DateTime.Now;
        }

        [Key]
        [StringLength(128)]
        public string GroupID { get; set; }

      
        [DataMember(Order = 2)]
        [Display(Name = "CreateDateTime")]
        public DateTime CreateDateTime { set; get; }


        [Display(Name = "群組名稱")]
        [StringLength(200)]
        public string Name { get; set; }

        public CommandType CommandType { get; set; }
        public CommandStatus CommandStatus { get; set; }
    }
}