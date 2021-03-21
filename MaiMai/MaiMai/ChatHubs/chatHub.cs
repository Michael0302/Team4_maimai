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
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace SignalRMvc.chatHubs
{

    //public class User
    //{
    //    /// <summary>
    //    /// 連線ID
    //    /// </summary>
    //    [Key]
    //    public string ConnectionID { get; set; }

    //    /// <summary>
    //    /// 使用者名稱稱
    //    /// </summary>
    //    public string Name { get; set; }

    //    public User(string name, string connectionId)
    //    {
    //        this.Name = name;
    //        this.ConnectionID = connectionId;
    //    }
    //}


    public class chatHub : Hub
    {
        //public static List<User> users = new List<User>();

        
        maimaiEntities db = new maimaiEntities();

        public void Group(int UserID)
        {
            var user = db.Member.Find(UserID);
                user.connectionID = Context.ConnectionId;
                db.SaveChanges();
            

            //判斷會員等級 管理員&優質會員>加入 VIP群組
            if (user.userLevel != 3)
            {
                var reload_user = db.Member.Find(UserID);
                Groups.Add(reload_user.connectionID, "VIP");
            }
        }
        public override Task OnConnected()
        {
            //var user = users.Where(u => u.ConnectionID == Context.ConnectionId).SingleOrDefault();
            //判断用户是否存在，否则添加集合
            //if (user == null)
            //{
            //    user = new User("",  Context.ConnectionId);
            //    users.Add(user);
            //}
            return base.OnConnected();
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

        public void OneToOneChat(int sender, int reciver, string message)
        {
            var user = db.Member.Find(reciver);
            if (user != null)
            {
                Clients.Client(user.connectionID).reciverMessage(message);
                Clients.Caller.senderMessage(message);

                Chat chat = new Chat()
                {
                    SenderID = sender,
                    ReciverID = reciver,
                    ChatText = message,
                    ChatTime = DateTime.Now
                };

                db.Chat.Add(chat);
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



    }
}