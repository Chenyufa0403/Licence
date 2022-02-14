using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Test.ViewModel
{
    public class LicenceLoginViewModel
    {
        [DisplayName("账号：")]
        public string UserName { get; set; }
        [DisplayName("密码：")]
        public string Pwd { get; set; }

    }
}