using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace ProjectPratice
{
    public class MessageCenter : ICenter
    {
        public void Dispose()
        {
            if (notificationList != null)
                notificationList.Clear();
        }
        private static MessageCenter Instance;
        public static MessageCenter instance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new MessageCenter();
                    return Instance;
                }
                return Instance;
            }
        }
        protected IDictionary<IRealize, List<string>> notificationList;
        public MessageCenter()
        {
            notificationList = new Dictionary<IRealize, List<string>>();
        }
        public virtual void ExecuteNotification(IConstruction construction)
        {
            List<IRealize> realizes = new List<IRealize>();
            foreach (var item in notificationList)
            {
                if (item.Value.Contains(construction.Name))
                {
                    realizes.Add(item.Key);
                }
            }
            if (realizes != null && realizes.Count > 0)
            {
                foreach (var item in realizes)
                {
                    item.OnMessage(construction);
                }
                realizes = null;
            }
        }
        public virtual void RegisterNotification(IRealize realize,string[] notificationNames)
        {
            if (notificationList.ContainsKey(realize))
            {
                List<string> list = null;
                if (notificationList.TryGetValue(realize,out list))
                {
                    foreach (var item in notificationNames)
                    {
                        if (list.Contains(item))
                            continue;
                        list.Add(item);
                    }
                }
            }
            else
            {
                notificationList.Add(realize, new List<string>(notificationNames));
            }
            
    }

        public virtual void RemoveNotification(IRealize realize,string [] notificationNames)
        {
            if (notificationList.ContainsKey(realize))
            {
                List<string> list = null;
                if (notificationList.TryGetValue(realize,out list))
                {
                    foreach (var item in notificationNames)
                    {
                        if (!list.Contains(item))
                            continue;
                        list.Remove(item);
                    }
                }
            }
        }

        public void SendNotification(string notificationName,object body = null)
        {
            IConstruction construction = new Construction(notificationName, body);
            ExecuteNotification(construction);
        }
    }
}

