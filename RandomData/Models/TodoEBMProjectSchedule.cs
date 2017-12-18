namespace RandomData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TodoEBMProjectSchedule")]
    public partial class TodoEBMProjectSchedule
    {
        [Key]
        public Guid PSID { get; set; }

        public DateTime CreateDateTime { get; set; }

        public DateTime ScheduleDateTime { get; set; }

        [StringLength(100)]
        public string Target { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public decimal WokingHour { get; set; }

        public int scheduleType { get; set; }

        public DateTime FinishDateTime { get; set; }

        [StringLength(128)]
        public string LineUID { get; set; }

        [StringLength(128)]
        public string Id { get; set; }

        public Guid PID { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public bool ProgressingFlag { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual TodoEBMProject TodoEBMProject { get; set; }
    }
}
