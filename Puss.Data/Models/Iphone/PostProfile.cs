using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Data.Models
{
    /// <summary>
    /// IphoneProfile
    /// </summary>
    public class IphoneProfile
    {
        /// <summary>
        /// profile_name
        /// </summary>
        public string profile_name { get; set; }
        /// <summary>
        /// url
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// allow_pairing
        /// </summary>
        public string allow_pairing { get; set; }
        /// <summary>
        /// is_supervised
        /// </summary>
        public string is_supervised { get; set; }
        /// <summary>
        /// is_multi_user
        /// </summary>
        public string is_multi_user { get; set; }
        /// <summary>
        /// is_mandatory
        /// </summary>
        public string is_mandatory { get; set; }
        /// <summary>
        /// await_device_configured
        /// </summary>
        public string await_device_configured { get; set; }
        /// <summary>
        /// is_mdm_removable
        /// </summary>
        public string is_mdm_removable { get; set; }
        /// <summary>
        /// auto_advance_setup
        /// </summary>
        public string auto_advance_setup { get; set; }
        /// <summary>
        /// org_magic
        /// </summary>
        public string org_magic { get; set; }
        /// <summary>
        /// devices
        /// </summary>
        public string[] devices { get; set; }
    }

    /// <summary>
    /// IphoneDevices
    /// </summary>
    public class IphoneDevices
    {
        /// <summary>
        /// profile_uuid
        /// </summary>
        public string profile_uuid { get; set; }
        /// <summary>
        /// devices
        /// </summary>
        public string[] devices { get; set; }
    }
}
