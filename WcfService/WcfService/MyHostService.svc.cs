using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Windows;

namespace WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“MyHostService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 MyHostService.svc 或 MyHostService.svc.cs，然后开始调试。
    public class MyHostService : IMyHostService
    {
        string RootPath = Data0.RootPath;
        public static int count = 3;
        
        public void GetFilePath(string rootpath)
        {
            RootPath = rootpath;
            Data0.RootPath = rootpath;
            //格式为"...\\Lamport门禁系统设计\\"
        }

        public string Registered()
        {
            //确保唯一性
            //调试用例
            //RootPath = " F:\\asus\\桌面\\密码算法识别\\小组大作业\\Lamport门禁系统设计\\";
            string IdPath = RootPath + "WcfService\\Id";
            string UserPath = RootPath + "WcfService\\User";
            string HostPath = RootPath + "WcfService\\Host\\HostKey.txt";
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
            string user = res;

            //MD5加密得到Id号
            //key就是主机主密钥
            //我这里没有用Diffie-Hellman！！！！！！！！！！！！！！！所以该处可以升级
            string key = File.ReadAllText(HostPath);
            
           
            MD5 md5 = MD5.Create();
            //将字符串装换为字节数组
            byte[] byteold = Encoding.UTF8.GetBytes(key);
            //加密
            byte[] bytenew = md5.ComputeHash(byteold);
            StringBuilder sb = new StringBuilder();
            foreach (var item in bytenew)
            {
                sb.Append(item.ToString("x2"));
            }

            //用户id号
            string id = sb.ToString();
            string filename = IdPath + "\\" + user + ".txt";
            FileStream fs = File.Create(filename);  //创建文件
            fs.Close();
            StreamWriter sw = new StreamWriter(filename);
            sw.Write(id);
            sw.Flush();
            sw.Close();
            
            //计数，新建新文件存放对应id号的次数
            string s = user + "1.txt";
            FileStream fs1 = File.Create(s);         
            StreamWriter sw1 = new StreamWriter(s);
            sw1.Write(count);
            sw1.Flush();
            sw1.Close();
            fs1.Close();

            //生成回话密钥
            byteold = Encoding.UTF8.GetBytes(id+"0");
            //加密
            bytenew = md5.ComputeHash(byteold);
            StringBuilder sb0 = new StringBuilder();
            foreach (var item in bytenew)
            {
                sb0.Append(item.ToString("x2"));
            }

            //初始回话密钥,写入文件
            string NO1 = sb0.ToString();
            string filename0 = UserPath + "\\" + user + ".txt";
            FileStream fs0 = File.Create(filename0);  //创建文件
            fs0.Close();
            StreamWriter sw0 = new StreamWriter(filename0);
            sw0.Write(NO1);
            sw0.Flush();
            sw0.Close();


            return user;
        }

        public bool Login(int User)
        {
            //若文件存在，则说明该user号存在，登录并将对应计数文件内容减一
            string path = User.ToString() + "1.txt";
            if (File.Exists(path))
            {
                int num = Convert.ToInt32(File.ReadAllText(path));
                num -= 1;
                File.WriteAllText(path, num.ToString());
                //若次数用完则删除该文件，这样下次登陆时即返回false而达到不能再使用的目的
                if (num == 0)
                {
                    File.Delete(path);
                    MessageBox.Show("本卡已失效，请重新生成一张");
                }
                   
                return true;
            }
            else
                return false;
        }
    }
}
