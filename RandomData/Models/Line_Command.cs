namespace RandomData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Line_Command
    {
        [Key]
        public Guid LCID { get; set; }

        [StringLength(128)]
        public string UserID { get; set; }

        [StringLength(128)]
        public string UserName { get; set; }

        [StringLength(128)]
        public string GroupID { get; set; }

        [StringLength(128)]
        public string RoomId { get; set; }

        public DateTime CreateDateTime { get; set; }

        [StringLength(5)]
        public string Command { get; set; }

        [StringLength(2000)]
        public string Message { get; set; }

        public int CommandType { get; set; }

        public int CommandStatus { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IssueNo { get; set; }

        [StringLength(128)]
        public string ReplyToken { get; set; }

        [StringLength(50)]
        public string LineType { get; set; }

        [StringLength(100)]
        public string assignUserName { get; set; }
    }
}
