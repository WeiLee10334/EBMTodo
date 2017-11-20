using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Linq;
using System.Threading.Tasks;
using EBMTodo.Models.Base.Enum;

namespace EBMTodo.Models.Base
{
    [DataContract(Namespace = "")]
    [Table("RBUnit")]
    public partial class RBUnit
    {
        public RBUnit()
        {
            RBUnitUserManage = new HashSet<RBUnitUserManage>();
            CDAT = DateTime.Now;
        }

        [Key, Column(Order = 0)]
        [Required]
        [DataMember(Order = 1)]
        [StringLength(20)]
        [Display(Name = "UnitID")]
        public string UID { set; get; }

        [Required]
        [DataMember(Order = 2)]
        [StringLength(100)]
        [Display(Name = "UnitName")]
        public string UnitName { set; get; }

        [DataMember(Order = 3)]
        [Display(Name = "CreateDateTime")]
        public DateTime CDAT { set; get; }

        [DataMember(Order = 4)]
        [StringLength(20)]
        //[Required(ErrorMessage = "Zip is Required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
        [Display(Name = "ZipCode")]
        public string ZipCode { set; get; }

        [DataMember(Order = 5)]
        [StringLength(200)]
        [Display(Name = "Address1")]
        public string Address1 { set; get; }

        [DataMember(Order = 6)]
        [StringLength(200)]
        [Display(Name = "Address2")]
        public string Address2 { set; get; }

        [DataMember(Order = 7)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "PhoneNumber")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumber { set; get; }

        [DataMember(Order = 8)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Fax")]
        public string Fax { set; get; }

        [Required]
        [DataMember(Order = 9)]
        [Display(Name = "UnitLevel")]
        public UnitLevel UnitLevel { set; get; }

        public virtual ICollection<RBUnitUserManage> RBUnitUserManage { get; set; }

    }
}