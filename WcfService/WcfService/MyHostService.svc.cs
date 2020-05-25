using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“MyHostService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 MyHostService.svc 或 MyHostService.svc.cs，然后开始调试。
    public class MyHostService : IMyHostService
    {
        string RootPath = "";
        public void GetFilePath(string rootpath)
        {
            RootPath = rootpath;
            //格式为"...\\Lamport门禁系统设计\\"
        }

        public string Registered()
        {

            //确保唯一性
            //调试用例
            //RootPath = " F:\\asus\\桌面\\密码算法识别\\小组大作业\\Lamport门禁系统设计\\";
            string IdPath = RootPath + "WcfService\\Id";
            string[] files = Directory.GetFiles(IdPath, "*.txt");
            bool flag=true;//唯一性标识
            //产生五个随机数
            Random rm = new Random();
            string res = "";
            do
            {
                res = "";
                flag = true;
                for (int i = 0; i < 5; i++)
                {
                    res += rm.Next(0, 10);
                }
                foreach (var item in files)
                {
                    if (item == res+".txt")
                    {
                        flag = false;
                        break;
                    }
                }
            } while (!flag);
            //User号就是res

            //生成用户Id

            return res;
        }

        public bool Login(int User)
        {
            return true;
        }
    }
}
