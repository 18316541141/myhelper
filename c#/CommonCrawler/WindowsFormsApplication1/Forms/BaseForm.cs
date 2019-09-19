
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonCrawler.Forms
{
    public abstract class BaseForm : Form
    {
        /// <summary>
        /// 日志输出类
        /// </summary>
        public ILog Log { set; get; }

		/// <summary>
        /// 跨平台的斜杠
        /// </summary>
        protected static char s;

        static BaseForm()
        {
            s = Path.DirectorySeparatorChar;
        }
    }
}
