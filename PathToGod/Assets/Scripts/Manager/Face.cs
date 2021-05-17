using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace ProjectPratice
{
    public class Face: MonoBehaviour,IManager, IDisposable,IRealize
    {

        Dictionary<string, IManager> managers;
        private GameObject gameManager;
        public T createGameObjAddComment<T>(string str)  where T: Component
        {
            if (gameManager == null)
            {
                gameManager = new GameObject("gameManager");
            }
            GameObject obj = new GameObject(str);
            obj.transform.SetParent(gameManager.transform);
            T c = obj.AddComponent<T>();
            return c;
        }
        protected T AddManager<T> (string managerName) where T:MonoManager
        {
            IManager c = null;
            managers.TryGetValue(managerName,out c);
            if (c!= null)
            {
                return (T)c;
            }
            c = createGameObjAddComment<T>(managerName);
            managers.Add(managerName,c);
            return (T)c;
        }
        public T GetManager<T>(string managerName) where T : MonoManager
        {
            IManager c = null;
            managers.TryGetValue(managerName, out c);
            if (c != null)
            {
                return (T)c;
            }
            return default;
        }

        protected void RemoveManager(string managerName)
        {
            if ( !managers.ContainsKey(managerName))
            {
                return;
            }
            IManager c = null;
            managers.TryGetValue(managerName, out c);
            if (c != null)
            {
                Type a = c.GetType();
                if (a.IsSubclassOf(typeof(MonoBehaviour) ))
                {
                    GameObject.DestroyImmediate(((MonoBehaviour)c).gameObject);
                }
            }
            managers.Remove(managerName);
        }

        protected void SendNotification(string notificationName, object body = null)
        {
            if (string.IsNullOrEmpty(notificationName))
                return;
            MessageCenter.instance.SendNotification(notificationName, body);
        }

        public virtual void OnMessage(IConstruction notification)
        {

        }
        protected virtual List<string> MessageList { get { return new List<string>(); } }

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
        protected virtual void Init()
        {
            managers = new Dictionary<string, IManager>();
            RemoveMessage(this, MessageList);
            RegisterMessage(this, MessageList);
        }
        void Awake()
        {
            Init();
        }
        public void Dispose()
        {
            managers.Clear();
            managers = null;
            RemoveMessage(this, MessageList);
        }
        public void OnDestroy()
        {
           
        }


    }
}

       