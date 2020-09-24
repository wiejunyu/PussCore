using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Data.Enum
{
    /// <summary>
    /// 导演和演员
    /// </summary>
    public enum MovieMember
    {
        /// <summary>
        /// 导演
        /// </summary>
        [Description("导演")]
        Director,
        /// <summary>
        ///演员
        /// </summary>
        [Description("演员")]
        Actors,
    }
}
