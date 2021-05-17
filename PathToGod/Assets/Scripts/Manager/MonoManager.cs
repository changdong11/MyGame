using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;

namespace ProjectPratice
{
    public class MonoManager : MonoBehaviour, IManager, IRealize, IDisposable
    {




        #region 消息的注册 发送 接收
        protected virtual void Init()
        {
            RemoveMessage(this, MessageList);
            RegisterMessage(this, MessageList);
            print("mono manager init 注册消息");
        }
        public virtual void Dispose()
        {
            RemoveMessage(this, MessageList);
        }
        public virtual void OnDestroy()
        {
            RemoveMessage(this, MessageList);
        }
        public virtual void OnMessage(IConstruction notification)
        {

        }
        protected virtual List<string> MessageList { get { return new List<string>(); } }
        public void Awake()
        {
            Init();
        }

        protected void RegisterMessage(IRealize realize, List<string> message)
        {

            MessageCenter.instance.RegisterNotification(realize, message.ToArray());
        }

        protected void RemoveMessage(IRealize realize, List<string> message)
        {
            if (realize == null || message == null || message.Count == 0)
                return;
            MessageCenter.instance.RegisterNotification(realize, message.ToArray());
        }
        protected void SendNotification(string notificationName, object body = null)
        {
            if (string.IsNullOrEmpty(notificationName))
                return;
            MessageCenter.instance.SendNotification(notificationName, body);
        }
        #endregion
    }

}
