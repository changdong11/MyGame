using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.Application ;
using UnityEngine;
namespace ProjectPratice
{
    public class LogManager:MonoManager
    {
        protected override List<string> MessageList
        {
            get
            {
                return new List<string>()
                {
                    MessageName.OpenUploadingLog,
                };
            }
        }
        public override void OnMessage(IConstruction notification)
        {
            base.OnMessage(notification);
            if (notification == null)
            {
                return;
            }
            string name = notification.Name;
            if (name == null)
            {
                return;
            }
            switch (name)
            {
                case MessageName.OpenUploadingLog:
                    UpLoadingLog();
                    break;
                default:
                    break;

            }
        }
        protected override void Init()
        {
            base.Init();
            isOpen = false;
            Debug.Log("log manager  init");
            ProjectUnity.Debug.logMessageReceivedThreaded += LogCallback;
        }
        bool isOpen;
        protected void UpLoadingLog()
        {
            isOpen = true;
        }
        private void LogCallback(string condition,string stackTrace,LogType logType)
        {
            
          
        }
    }
}

namespace ProjectUnity
{
    public static class Debug
    {
        public static event LogCallback logMessageReceivedThreaded;
        public static void Log(object message)
        {
            UnityEngine.Debug.Log(message);
        }
        public static void LogPE(object message)
        {
            string trackStr = new System.Diagnostics.StackTrace().ToString();
            //UnityEngine.Debug.Log("正式打印信息 ----" +message.ToString()+" \n正式打印堆栈----"+ trackStr);
            logMessageReceivedThreaded?.Invoke(message.ToString(), trackStr.ToString(), UnityEngine.LogType.Log);
            UnityEngine.Debug.Log(message);
        }

        public static void LogError(object message)
        {
            UnityEngine.Debug.LogError(message);
        }

        public static void LogColor(object message, Color color)
        {
            string text = ColorUtility.ToHtmlStringRGBA(color);
            UnityEngine.Debug.LogFormat("<color=#{0}>{1}</color>", text, message);
        }
    }

}
