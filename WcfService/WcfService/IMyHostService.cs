using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IMyHostService”。
    [ServiceContract]
    public interface IMyHostService
    {
        [OperationContract]
        //这个地方是为了让服务器知道wpf的路径，参数为string
        void GetFilePath(string rootpath);

        [OperationContract]
        //注册接口，无需传入参数，返回一个唯一的五位数字User号(str)，注册失败则返回"-1"
        string Registered();

        [OperationContract]
        //刷卡，传入自己门禁卡的User号，返回bool结果
        bool Login(int User);
    }
}
