using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Data.Models
{
    #region Key栏目
    /// <summary>
    /// 创建Key栏目模型
    /// </summary>
    public class CreateKeySection
    {
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 栏目备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 编辑Key栏目模型
    /// </summary>
    public class EditKeySection
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 栏目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 栏目备注
        /// </summary>
        public string Remarks { get; set; }
    }
    #endregion
    #region Key
    /// <summary>
    /// 创建Key模型
    /// </summary>
    public class CreateKey
    {
        /// <summary>
        /// 栏目ID
        /// </summary>
        public string SectionID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户文本
        /// </summary>
        public string UserText { get; set; }

        /// <summary>
        /// 密码文本
        /// </summary>
        public string PasswordText { get; set; }

        /// <summary>
        /// 网址文本
        /// </summary>
        public string UrlText { get; set; }

        /// <summary>
        /// 手机本文
        /// </summary>
        public string IphoneText { get; set; }

        /// <summary>
        /// 邮箱文本
        /// </summary>
        public string MailText { get; set; }

        /// <summary>
        /// 其他
        /// </summary>
        public string OtherText { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public string IsStart { get; set; }
    }

    /// <summary>
    /// 编辑Key模型
    /// </summary>
    public class EditKey
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 栏目ID
        /// </summary>
        public string SectionID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户文本
        /// </summary>
        public string UserText { get; set; }

        /// <summary>
        /// 密码文本
        /// </summary>
        public string PasswordText { get; set; }

        /// <summary>
        /// 网址文本
        /// </summary>
        public string UrlText { get; set; }

        /// <summary>
        /// 手机本文
        /// </summary>
        public string IphoneText { get; set; }

        /// <summary>
        /// 邮箱文本
        /// </summary>
        public string MailText { get; set; }

        /// <summary>
        /// 其他
        /// </summary>
        public string OtherText { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public string IsStart { get; set; }
    }
    #endregion
}
