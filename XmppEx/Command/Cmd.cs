using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxundermoonLib.XmppEx.Command
{
   public class Cmd
    {
       public const String GetOnlineUsers = "GetOnlineUsers"; //向服务获取全部在线用户
       public const String GetOnlineUsersResponse = "GetOnlineUsersResponse";  //接受全部在线任务
       //public const String GetUserTask = "GetUserTask";
       //public const String GetUserTaskResponse = "GetUserTaskResponse";
       //public const String GetUpLine = "GetUpLine";
       //public const String GetUpLineResponse = "GetUpLineResponse";
       //public const String GetDownLine = "GetDownLine";
       //public const String GetDownLineResponse = "GetDownLineResponse";
       public const String UserLogin = "UserLogin";  //有新用户登录
       public const String OnLineAtOtherPlace = "OnLineAtOtherPlace"; //用户被顶下线
       public const String UserOffLine = "UserOffLine"; //用户离线
       public const String ErrorMessage = "ErrorMessage";  //错误相关消息

       public const String SendTask = "SendTask";   // 下发任务
       public const String SendTaskResponse = "SendTaskResponse";  //收到下发任务
       public const String DataTable = "DataTable";  //数据库/表相关操作
       public const String Response = "Response";  //数据库/表相关操作

    }
}
