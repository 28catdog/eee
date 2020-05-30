using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp.MyHost;

namespace WpfApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //服务端的命名空间是MyHost，我已添加，如需引用，直接输入MyHostServiceClient
        MyHostServiceClient MyHost = new MyHostServiceClient();
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                Init();
            }
            catch
            {
                MessageBox.Show("程序初始化错误，请联系系统管理员！");
            }
        }

        //初始化，得到文件主目录，为了服务端那边找到Id等目录方便
        private void Init()
        {
            string rootpath = Directory.GetCurrentDirectory();
            //rootpath = "F:\\asus\\桌面\\密码算法识别\\小组大作业\\Lamport门禁系统设计\\WcfService\\WpfApp\\bin\\Debug";
            string[] tmp = rootpath.Split('\\');
            rootpath = "";
            for (int i = 0; i < tmp.Length - 4; i++)
            {
                rootpath += tmp[i] + "\\";
            }
            MyHost.GetFilePath(rootpath);
        }

        private void Registered(object sender, RoutedEventArgs e)
        {
            try
            {
                this.text.Text = MyHost.Registered();
                this.label.Content = "注册成功！";
            }
            catch
            {
                this.text.Text = "?????";
                this.label.Content = "注册失败！";
            }
        }


    }
}
