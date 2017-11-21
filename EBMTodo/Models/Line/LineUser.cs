using EBMTodo.Models.Base.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace EBMTodo.Models.Todo
{
    [DataContract(Namespace = "")]
    [Table("Line_User")]
    public partial class LineUser
    {
        public LineUser()
        {
            CreateDateTime = DateTime.Now;
        }

        [Key]
        [StringLength(128)]
        public string UID { get; set; }

      
        [DataMember(Order = 2)]
        [Display(Name = "CreateDateTime")]
        public DateTime CreateDateTime { set; get; }


        [Display(Name = "UserName")]
        [StringLength(200)]
        public string Name { get; set; }


    }
}