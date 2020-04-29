using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Data.Models
{
    public class IphoneProfile
    {
        public string profile_name { get; set; }
        public string url { get; set; }
        public string allow_pairing { get; set; }
        public string is_supervised { get; set; }
        public string is_multi_user { get; set; }
        public string is_mandatory { get; set; }
        public string await_device_configured { get; set; }
        public string is_mdm_removable { get; set; }
        public string auto_advance_setup { get; set; }
        public string org_magic { get; set; }
        public string[] devices { get; set; }
    }

    public class IphoneDevices
    {
        public string profile_uuid { get; set; }
        public string[] devices { get; set; }
    }
}
