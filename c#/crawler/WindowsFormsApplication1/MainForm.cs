using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.Forms;

namespace WindowsFormsApplication1
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 日志输出类
        /// </summary>
        public ILog Log { set; get; }

        /// <summary>
        /// 跨平台的斜杠
        /// </summary>
        protected static char s;

        static MainForm()
        {
            s = Path.DirectorySeparatorChar;
        }

        public MainForm()
        {
            InitializeComponent();
        }
    }
}
