using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EBMTodo.Models.Base
{
    [DataContract(Namespace = "")]
    [Table("RBUnitUserManage")]
    public class RBUnitUserManage
    {
        [DataMember(Order = 1)]
        [Required]
        [StringLength(128)]
        public string ApplicationUserId { get; set; }

        [DataMember(Order = 2)]
        [Required]
        [StringLength(20)]
        public string UID { get; set; }
        public virtual RBUnit RBUnit { get; set; }
    }
}