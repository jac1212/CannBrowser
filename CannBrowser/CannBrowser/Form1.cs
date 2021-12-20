using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CannBrowser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            webBrowser1.Navigate("https://www.baidu.com/");
            url.Text = "https://www.baidu.com/";
        }
        /// <summary>
        /// 快捷键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void url_KeyUp(object sender, KeyEventArgs e)
        {
            //回车
            if (e.KeyCode == Keys.Enter)
            {
                string networking = "https://";
                if (url.Text.IndexOf("https://") > -1 || url.Text.IndexOf("http://") > -1)
                {
                    networking = "";
                }
                webBrowser1.Navigate(networking + url.Text);
            }
            //最大最小窗口
            if (e.KeyCode == Keys.F11)
            {
                if (WindowState == FormWindowState.Maximized)
                    WindowState = FormWindowState.Normal;
                else
                    WindowState = FormWindowState.Maximized;
            }
            //刷新
            if (e.KeyCode == Keys.F5)
            {
                webBrowser1.Refresh();
            }
        }
        private void back_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();//后退
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();//刷新
        }

        private void forward_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();//前进
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            url.Text = ((WebBrowser)sender).Document.Window.Url.OriginalString;
            ((WebBrowser)sender).Document.Window.Error += new HtmlElementErrorEventHandler(Window_Error);
            //将所有的链接的目标，指向本窗体
            foreach (HtmlElement archor in this.webBrowser1.Document.Links)
            {
                archor.SetAttribute("target", "_self");
            }

            //将所有的FORM的提交目标，指向本窗体
            foreach (HtmlElement form in this.webBrowser1.Document.Forms)
            {
                form.SetAttribute("target", "_self");
            }
        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }
        private void Window_Error(object sender, HtmlElementErrorEventArgs e)
        {
            e.Handled = true;
        }
    }
}
