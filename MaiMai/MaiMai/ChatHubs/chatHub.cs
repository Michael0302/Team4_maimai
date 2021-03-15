//using Microsoft.AspNet.SignalR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace SignalRMvc.chatHubs
//{
//    public class chatHub : Hub
//    {
//        public void Sendmessage(string name, string message)
//        {
//            // Clients.All.hello();
//            Clients.All.receiveMessage(name, message);
//            //用戶調用客戶端的函數
//        }
//    }
//}


using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MaiMai.Models;
using Microsoft.AspNet.SignalR;
using System.Web.Mvc;
using System.Collections.Generic;

namespace SignalRMvc.chatHubs
{
    public class chatHub : Hub
    {
        private static readonly char[] Constant =
        {
 '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
 'w', 'x', 'y', 'z',
 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
 'W', 'X', 'Y', 'Z'
 };

        /// <summary>
        /// 供客戶端呼叫的伺服器端程式碼
        /// </summary>
        /// <param name="message"></param>
        maimaiEntities db = new maimaiEntities();

        public void Group(int UserID)
        {
            var user = db.Member.Find(UserID);
            
            //判斷是否有connectionID，如沒有，塞一個進去
            if(user.connectionID == null)
            {
                user.connectionID = Context.ConnectionId;
                db.SaveChanges();
            }

            //判斷會員等級 一般會員>加入All群組，管理員&優質會員>加入All & VIP群組
            if (user.userLevel != 3)
            {
                var reload_user = db.Member.Find(UserID);
                Groups.Add(reload_user.connectionID, "VIP");
            }
        }

        public void Send(int sender,  string message)
        {
            // 呼叫所有客戶端的sendMessage方法
            Clients.All.addMessage(message);

            Notification noti = new Notification() {
                SenderID = sender,
                ReciverLevel = "All",
                NotifyText = message,
                CreateTime = DateTime.Now
            };

            db.Notification.Add(noti);
            db.SaveChanges();
        }

        public void SendToOne(int sender, int reciver, string message)
        {
            var user = db.Member.Find(reciver);
            if(user != null)
            {                
                Clients.Client(user.connectionID).addMessage(message);

                Notification noti = new Notification()
                {
                    SenderID = sender,
                    ReciverLevel = reciver.ToString(),
                    NotifyText = message,
                    CreateTime = DateTime.Now
                };

                db.Notification.Add(noti);
                db.SaveChanges();
            }
        }

        public void SendToVIP(int sender,  string message)
        {
            Clients.Group("VIP").addMessage(message);

            Notification noti = new Notification()
            {
                SenderID = sender,
                ReciverLevel = "VIP",
                NotifyText = message,
                CreateTime = DateTime.Now
            };

            db.Notification.Add(noti);
            db.SaveChanges();
        }


        public override Task OnConnected()
        {
            Trace.WriteLine("客戶端連線成功");
            return base.OnConnected();
        }

        /// <summary>
        /// 產生隨機使用者名稱函式
        /// </summary>
        /// <param name="length">使用者名稱長度</param>
        /// <returns></returns>
        public static string GenerateRandomName(int length)
        {
            var newRandom = new System.Text.StringBuilder(62);
            var rd = new Random();
            for (var i = 0; i < length; i++)
            {
                newRandom.Append(Constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }
    }
}