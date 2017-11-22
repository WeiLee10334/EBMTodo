namespace RandomData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ApplicationGroupRoles
    {
        [Key]
        [Column(Order = 0)]
        public string ApplicationRoleId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string ApplicationGroupId { get; set; }

        public virtual ApplicationGroups ApplicationGroups { get; set; }
    }
}
