using System.Security.AccessControl;

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using System;
namespace ProjectPratice
{
    /// <summary>
    /// 加载app配置文件
    /// 发送消息 加载常用bundle
    /// 发送消息 加载lua
    /// </summary>
    public class AppManager :MonoManager
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
                //case MessageName.OpenUploadingLog:
                   
                //    break;
                default:
                    break;

            }
        }
        public static string BuildModelPath = Application.streamingAssetsPath + "/Build.json";
        protected override void Init()
        {
            base.Init();
            print("appManager init");
            GlobalSysSetting();
            LoadAssetFileAsync(BuildModelPath);
        }
        /// <summary>
        /// 全局参数设置
        /// </summary>
        protected void GlobalSysSetting()
        {
            //设置app 的屏幕设置
        }

        /// <summary>
        /// 异步加载配置文件
        /// </summary>
        /// <param name="path"></param>
         async void LoadAssetFileAsync(string path)
        {
            string protocol = "file://";
            if (Application.platform == RuntimePlatform.Android)
            {
                protocol = string.Empty;
            }
            path = $"{protocol}{path}";
            print("加载路径---" + path);
            var unityWebRequest = UnityWebRequest.Get(path);
            await unityWebRequest.SendWebRequest();
            if (string.IsNullOrEmpty(unityWebRequest.error))
            {
                if (!string.IsNullOrEmpty(unityWebRequest.downloadHandler.text))
                {
                    BuildModel model = LitJson.JsonMapper.ToObject<BuildModel>(unityWebRequest.downloadHandler.text);
                    AppSetting.Version = model.Version;
                    AppSetting.AppGUID = model.AppGuid;
                    AppSetting.App_runPlatform = model.RunPlatform;
                    var currentGUID = PlayerPrefs.GetString(PlayerPrefsKeys.GUID_Key);
                    if (string.IsNullOrEmpty(currentGUID))
                    {
                        ProjectUnity.Debug.LogPE("APP 第一次安装");

                        AppSetting.FirstInstall = true;
                    }
                    else if (currentGUID != AppSetting.AppGUID)
                    {
                        ProjectUnity.Debug.LogPE("APP 覆盖安装");
                        AppSetting.FirstInstall = false;
                    }
                    PlayerPrefs.SetString(PlayerPrefsKeys.GUID_Key, AppSetting.AppGUID);
                }
            }
            else
            {
                ProjectUnity.Debug.LogPE("配置文件读取错误");
            }
            //编辑器下添加budle加载 和 本地加载两种方式
            //真机下从budle加载
            //Debug.Log("发送 加载常用ab 消息");
            //SendNotification(MessageName.LoadCommonAB,new Action(LoadCommonCallback));

            
            DynamicList list = new DynamicList();
            list[0] = new Action(LoadCommonCallback);
            print("发送 加载 common 消息");
            SendNotification(MessageName.LoadCommonAB, list);
            
        }
        protected void LoadCommonCallback()
        {
            Debug.Log("发送 加载ab config 消息");
            DynamicList list = new DynamicList();
            list[0] = new Action(LoadConfigCallback);
            SendNotification(MessageName.LoadABConfig, list);
        }
        protected void LoadConfigCallback()
        {
            
            UnityEngine.Object obj = GameFace.instance.AssetBundleManager.Load<UnityEngine.Object>("Base/Prefab/Common/Canvas02.prefab");
            GameObject.Instantiate<UnityEngine.Object>(obj);
            GameFace.instance.AssetBundleManager.LoadAsync<UnityEngine.Object>(
                "Base/Prefab/Common/Canvas03.prefab",
                obj02 =>
                {
                    print(obj02.name + "异步加载完成---");
                    GameObject.Instantiate<UnityEngine.Object>(obj02);
                }
                );
            ProjectUnity.Debug.LogColor("发送 启动lua虚拟机 执行lua 代码 消息", Color.red);
            //启动lua虚拟机  执行lua代码
            SendNotification(MessageName.StartLuaEnv);
        }
        protected void Call(UnityEngine.Object obj)
        {

        }
        public override void Dispose()
        {
            base.Dispose();
        }

        public override void OnDestroy()
        {
            base.Dispose();
        }
    }
}
