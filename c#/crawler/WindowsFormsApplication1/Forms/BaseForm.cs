using CX_Task_Center.Code.Message;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Forms
{
    public abstract class BaseForm : Form
    {
        /// <summary>
        /// 日志输出类
        /// </summary>
        public ILog Log { set; get; }

        /// <summary>
        /// 办事易微信报警类
        /// </summary>
        public BsyWarningHelper BsyWarningHelper { set; get; }
    }
}
