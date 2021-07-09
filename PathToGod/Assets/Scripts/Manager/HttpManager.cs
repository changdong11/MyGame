using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestHTTP;
using ProjectUnity;
using UnityEngine;
namespace ProjectPratice
{
    public class HttpManager:MonoManager
    {
        protected override List<string> MessageList
        {
            get
            {
                return new List<string>()
                {
                    
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
                
                default:
                    break;

            }
        }
        protected override void Init()
        {
            base.Init();
            
        }
        public void SendHttpRequest(U3DAppHttpRequest request)
        {
            ProjectUnity.Debug.LogPE("###Http请求信息:\n" + request.ToString());
            if (DetectionNotNetworking())
            {
                ProjectUnity.Debug.LogPE("检测到未联网状态----");
            }
        }

        /// <summary>
        /// 检测是否联网
        /// </summary>
        /// <returns>true 表示未联网 false 表示已联网</returns>
        public bool DetectionNotNetworking()
        {
            return Application.internetReachability == NetworkReachability.NotReachable;
        }

        /// <summary>
        /// 检测是否流量上网
        /// </summary>
        /// <returns>1 表示流量上网 2 表示wifi上网 3表示未联网</returns>
        public int DetectionDataNetwork()
        {

            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                return 1;
            }
            else if(Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                return 2;
            }
            else
            {
                return 3;
            }
            
        }

        public class U3DAppHttpRequest : IDisposable
        {
            public void Dispose()
            {
                
            }
            /// <summary>
            /// url 地址
            /// </summary>
            public string url;
            /// <summary>
            /// 请求方式（get post）
            /// </summary>
            public HTTPMethods methods;

            /// <summary>
            /// 请求头 Header
            /// </summary>
            public Dictionary<string, string> header;

            /// <summary>
            /// 请求参数
            /// </summary>
            public Dictionary<string, object> reqParams;
            public U3DAppHttpRequest(string url, HTTPMethods methods, 
                Dictionary<string, string> header, 
                Dictionary<string, object> reqParams)
            {
                this.url = url;
                this.methods = methods;
                this.header = header;
                this.reqParams = reqParams;
            }
        }
    }
}
