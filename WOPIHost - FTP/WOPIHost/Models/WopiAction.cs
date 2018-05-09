using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WOPIHost.Models
{
    public class WopiAction
    {
        public string app { get; set; }
        public string favIconUrl { get; set; }
        public bool checkLicense { get; set; }
        public string name { get; set; }
        public string ext { get; set; }
        public string progid { get; set; }
        public string requires { get; set; }
        public bool? isDefault { get; set; }
        public string urlsrc { get; set; }
    }
}