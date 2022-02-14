using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.ViewModel
{
    public class LicenceViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime? VaildDate { get; set; }

        public bool? IsReview { get; set; }

        public DateTime? ReviewDate { get; set; }

        public string State { get; set; }

        public string LendUserName { get; set; }
    }
}