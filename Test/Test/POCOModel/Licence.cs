namespace Test.POCOModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Licence")]
    public partial class Licence
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime? VaildDate { get; set; }

        public bool? IsReview { get; set; }

        public DateTime? ReviewDate { get; set; }

        [Required]
        [StringLength(20)]
        public string State { get; set; }

        public int? LendUserID { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}
