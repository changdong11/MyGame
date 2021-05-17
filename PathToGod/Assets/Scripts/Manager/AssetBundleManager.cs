using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using System.IO;
namespace ProjectPratice
{
    public class AssetBundleManager : MonoManager
    {
        //异步加载 状态
        public enum TaskState
        {
            Sucess,
            Fail,
        }
        //异步加载返回 存储返回数据
        public class TaskReturn
        {
            public TaskState state;
            public object TaskReturnObj;
            public TaskReturn(TaskState state , object TaskReturnObj)
            {
                this.state = state;
                this.TaskReturnObj = TaskReturnObj;
            }
        }


        protected override List<string> MessageList
        {
            get
            {
                return new List<string>()
                {
                    MessageName.LoadCommonAB,
                    MessageName.LoadABConfig,
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
                case MessageName.LoadCommonAB:
                    DynamicList dynamicList = notification.Body as DynamicList;
                    Action action = null;
                    ProjectUnity.Debug.LogColor("接收到 加载常用ab 的消息 ", Color.red);
                    if (dynamicList != null)
                    {
                        action = dynamicList[0] as Action;
                    }
                    LoadABCommon(action);
                    break;
                case MessageName.LoadABConfig:
                    dynamicList = notification.Body as DynamicList;
                    action = null;
                    ProjectUnity.Debug.LogColor("接收到 加载config 的消息 ", Color.red);
                    ProjectUnity.Debug.LogColor(notification.Body.ToString(), Color.red);
                    if (dynamicList != null)
                    {
                        action = dynamicList[0] as Action;
                    }
                    LoadABConfig(action);
                    break;
                default:
                    break;

            }
        }
        protected override void Init()
        {
            base.Init();
            loadAssetbundleConfig = new Dictionary<string, AssetBundleConfig>();
            loadLuaBundle = new Dictionary<string, AssetBundle>();
        }
        private string[] commonAbNames = new string[] { "base_prefab_common" };
        protected async void LoadABCommon(Action callBack = null)
        {
            ProjectUnity.Debug.LogColor("开始加载common res", Color.red);
            foreach (var item in commonAbNames)
            {
                var commonPath = "res/" + item.Replace(".","/");
                var resBundlepath = BuildPath.Search_Path(commonPath);
                
                var luaTaskReturn = await LoadAssetbundleAsync(resBundlepath);
                if (luaTaskReturn != null)
                {
                    switch (luaTaskReturn.state)
                    {
                        case TaskState.Sucess:
                            AssetBundle bundle = luaTaskReturn.TaskReturnObj as AssetBundle;
                            if (bundle != null)
                                loadLuaBundle.Add(item, bundle);
                            print(bundle.name);
                                UnityEngine.Object obj = bundle.LoadAsset<UnityEngine.Object>("Canvas");
                                GameObject.Instantiate(obj);
                            break;
                        case TaskState.Fail:
                            ProjectUnity.Debug.LogPE($"Assetbundle 加载错误：{luaTaskReturn.TaskReturnObj}");
                            break;
                        default:
                            break;
                    }
                }
                ProjectUnity.Debug.LogColor("加载common res  完成", Color.red);
            }
            callBack?.Invoke();

        }

        protected async void LoadABConfig(Action callBack = null)
        {
            await LoadABConfig("config");
            ProjectUnity.Debug.LogColor("加载config  完成", Color.red);
            callBack ?.Invoke();
            ProjectUnity.Debug.LogColor("加载config  回调 执行 完成", Color.red);

        }
        private Dictionary<string, AssetBundleConfig> loadAssetbundleConfig;
        private Dictionary<string, AssetBundle> loadLuaBundle;
        protected async Task LoadABConfig(string configName)
        {
            string configPath = "config/" + configName + ".json";
            var path = BuildPath.Search_Path(configPath);
            //ProjectUnity.Debug.LogColor("加载 config 路径---" + path,Color.red);
            //加载ab包配置文件
            var taskReturn = await LoadAssetFileAsync(path);
            AssetBundleConfig config = null;
            if (taskReturn != null)
            {
                switch (taskReturn.state)
                {
                    case TaskState.Sucess:
                        string content = taskReturn.TaskReturnObj as string;
                        if (!string.IsNullOrEmpty(content))
                            config = JsonUtility.FromJson<AssetBundleConfig>(content);
                            loadAssetbundleConfig.Add(configName, config);
                        break;
                    case TaskState.Fail:
                        ProjectUnity.Debug.LogPE($"Config 加载错误 ：{taskReturn.TaskReturnObj}");
                        break;
                    default:
                        break;
                }
            }
            //通过配置文件加载lua bundle
            foreach (var item in config.lua.Item)
            {
                var luaPath = "lua/" + item.Name;
                var luaBundlepath = BuildPath.Search_Path(luaPath);
                //ProjectUnity.Debug.LogColor("加载 lua bundle 路径---" + luaPath, Color.red);
                var luaTaskReturn = await LoadAssetbundleAsync(luaBundlepath);
                if (luaTaskReturn!= null)
                {
                    switch (luaTaskReturn.state)
                    {
                        case TaskState.Sucess:
                            AssetBundle bundle = luaTaskReturn.TaskReturnObj as AssetBundle;
                            if (bundle != null)
                                loadLuaBundle.Add(item.Name, bundle);
                            break;
                        case TaskState.Fail:
                            ProjectUnity.Debug.LogPE($"Assetbundle 加载错误：{luaTaskReturn.TaskReturnObj}");
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public byte[] LoadLua(string fileNamePath)
        {
            ProjectUnity.Debug.LogColor(fileNamePath, Color.green);
            //编辑器下 不从bundle加载
            if (RunPlatform.UNITY_EDITOR()&& !BuildPath.EDITOR_LOAD_ASSETBUNDLE)
            {
                var fliePath = fileNamePath.Replace(".", "/");
                return LoadLuaEditor(fliePath);
            }
            //从bundle 中加载
            var lastIndex = fileNamePath.LastIndexOf(".");
            var luaBundleName = fileNamePath.Substring(0, fileNamePath.Length -(fileNamePath.Length - lastIndex));
            var luaScriptsName = fileNamePath.Substring(lastIndex+1);
            luaBundleName = luaBundleName.Replace(".", "_");
            luaBundleName = luaBundleName.ToLower();
            ProjectUnity.Debug.LogColor("luaBundleName :"+ luaBundleName, Color.red);
            ProjectUnity.Debug.LogColor("luaScriptsName :" + luaScriptsName, Color.red);
            return LoadLuaBundle(luaBundleName, luaScriptsName);
        }
        private byte[] LoadLuaBundle(string bundleName,string scriptsName)
        {
            if (string.IsNullOrEmpty(bundleName) || string.IsNullOrEmpty(scriptsName))
                return null;
            AssetBundle bundle = null;
            byte[] buffer = null;
            loadLuaBundle.TryGetValue(bundleName, out bundle);
            ProjectUnity.Debug.LogColor(bundle.ToString(), Color.green);
            TextAsset text = bundle.LoadAsset<TextAsset>(scriptsName);
            if (text != null)
            {
                buffer = text.bytes;
                Resources.UnloadAsset(text);
            }
            Util.SimpleEncryp(buffer, BuildPath.Lua_Password);
            return buffer;
        }
        private byte[] LoadLuaEditor(string path)
        {
            var luapath = Application.dataPath + "/" + BuildPath.Lua_Scripts_Floder + "/" + path + ".lua";
            if (File.Exists(luapath))
            {
                ProjectUnity.Debug.LogColor("lua 脚本加载路径----" + luapath, Color.red);    
                return File.ReadAllBytes(luapath);
            }
            return null;
        }
        /// <summary>
        /// 异步加载 bundle
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected async Task<TaskReturn> LoadAssetbundleAsync(string path)
        {
            var web = UnityWebRequestAssetBundle.GetAssetBundle(path);
            await web.SendWebRequest();
            TaskReturn taskReturn = null;
            if (web.isDone)
            {
                taskReturn = new TaskReturn(TaskState.Sucess, DownloadHandlerAssetBundle.GetContent(web));
            }
            else if(!string.IsNullOrEmpty(web.error))
            {
                taskReturn = new TaskReturn(TaskState.Fail, web.error);
            }
            return taskReturn;
        }
        /// <summary>
        /// 异步加载文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected async Task<TaskReturn> LoadAssetFileAsync(string fileName)
        {
            var path = Util.Combine(fileName);
            UnityWebRequest web = UnityWebRequest.Get(path);
            await web.SendWebRequest();
            TaskReturn taskReturn = null;
            if (string.IsNullOrEmpty(web.error))
            {
                taskReturn = new TaskReturn(TaskState.Sucess,web.downloadHandler.text);
                return taskReturn;
            }
            else
            {
                taskReturn = new TaskReturn(TaskState.Fail, web.downloadHandler.text);
                return taskReturn;
            }
        }
    }
}
