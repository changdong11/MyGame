using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
namespace ProjectPratice
{
    //json 格式
    [Serializable]
    public class NativeParameter
    {
        public string MethodName;
        public string param;
    }
    public class NativeManager: MonoManager
    {
        protected override List<string> MessageList
        {
            get
            {
                return new List<string>()
                {
                    MessageName.InvokeNative, //调用原生方法
                   // MessageName.CheckNativeMethod,//原生方法回调
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
                case MessageName.InvokeNative:
                    NativeParameter parameter = notification.Body as NativeParameter;

#if UNITY_EDITOR
                        EditorInvoke(parameter);
#elif UNITY_ANDROID
                    AndroidInvoke(parameter);
#elif UNITY_IOS
                    IosInvoke(parameter);
#endif



                    break;
                case MessageName.CheckNativeMethod:
                    

                    break;
                default:
                    break;

            }
        }
        protected override void Init()
        {
            base.Init();
            jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        }

        public void EditorInvoke(NativeParameter parameter)
        {
            string json = LitJson.JsonMapper.ToJson(parameter);
            print(parameter.ToString());
            ProjectUnity.Debug.LogPE("###unity模块调用原生方法:" + json);
        }
        private AndroidJavaClass jc;
        private AndroidJavaObject jo;
        public void AndroidInvoke(NativeParameter parameter)
        {
            string json = null;
            try
            {
                json = LitJson.JsonMapper.ToJson(parameter);
                jo.Call("NativeSupportService", json);
                ProjectUnity.Debug.LogPE("###unity模块调用原生方法:" + json);
            }
            catch (Exception)
            {
                ProjectUnity.Debug.LogPE("###unity模块调用原生方法失败:" + parameter.MethodName.ToString());
                throw;
            }
            
            
        }
        public void IosInvoke(NativeParameter parameter)
        {

        }

        /// <summary>
        /// 接收原生方法调用
        /// </summary>
        /// <param name="jsonParameter"></param>
        public void AndroidCallUnity(string jsonParameter)
        {
            ProjectUnity.Debug.LogPE("###原生回传给unity的json串:"+jsonParameter);
            if (string.IsNullOrEmpty(jsonParameter))
            {
                ProjectUnity.Debug.LogPE("###原生回传给unity的json串为空。" );
                return;
            }
            SendNotification(MessageName.CheckNativeMethod,jsonParameter);
        }

    }
}

