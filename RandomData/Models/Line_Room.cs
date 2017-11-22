namespace RandomData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Line_Room
    {
        [Key]
        public string RoomID { get; set; }

        public DateTime CreateDateTime { get; set; }

        [StringLength(200)]
        public string Name { get; set; }
    }
}
