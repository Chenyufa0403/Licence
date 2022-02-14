using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Test.ViewModel
{
    public class LicenceAddViewModel
    {
        [DisplayName("证照名称")]
        [Required(ErrorMessage = "证照名称不能为空")]
        public string Name { get; set; }
        [DisplayName("签发日期：")]
        [Required(ErrorMessage = "签发日期不能为空")]
        public DateTime IssueDate { get; set; }
        [DisplayName("有效期至：")]

        public DateTime? VaildDate { get; set; }
        [DisplayName("是否年审：")]
        public bool? IsReview { get; set; }
        [DisplayName("下次年审日期：")]

        public DateTime? ReviewDate { get; set; }
        [DisplayName("状态：")]
        [Required(ErrorMessage = "状态不能为空")]
        public string State { get; set; }
        [DisplayName("出借人：")]

        public int LendUserID { get; set; }
    }
}