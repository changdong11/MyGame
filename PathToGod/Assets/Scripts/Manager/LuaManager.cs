using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLua;
namespace ProjectPratice
{
    public class LuaManager : MonoManager
    {
        protected override List<string> MessageList
        {
            get
            {
                return new List<string>()
                {
                    MessageName.DoLuaString,
                    MessageName.StartLuaEnv,
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
                case MessageName.StartLuaEnv:
                    StartApp();
                    break;
                case MessageName.DoLuaString:
                    break;
                default:
                    break;

            }
        }
        LuaEnv LuaEnv;
        protected override void Init()
        {
            base.Init();
            InitLuaEnv();
        }
        protected void InitLuaEnv()
        {
            if (LuaEnv != null)
            {
                UnityEngine.Debug.LogError("lua 虚拟机 已经实例化");
                return;
            }
            LuaEnv = new LuaEnv();
            if (LuaEnv !=null)
            {
                LuaEnv.AddLoader(LuaLoader);
            }
            else
            {
                ProjectUnity.Debug.LogPE("初始化 lua虚拟机 失败");
            }
            
        }
        private byte[] LuaLoader(ref string luafilePath)
        {
            return GameFace.instance.AssetBundleManager.LoadLua(luafilePath);
        }

        private void StartApp()
        {

            if (LuaEnv != null)
            {
                ProjectUnity.Debug.LogColor("执行 lua代码", UnityEngine.Color.red);
                //开始执行lua代码
                //require lua 脚本
                SafeDoString("require ('Base.GameMain.GameStart')");
                //"require('Base.Main.AppStart')";
                //执行 lua 入口函数
                SafeDoString("Start()");

            }
        }
    
        private void SafeDoString(string content)
        {
            if (LuaEnv == null)
            {
                ProjectUnity.Debug.LogPE("执行lua代码时虚拟机未实例化");
                return;
            }
            try
            {
                LuaEnv.DoString(content);
            }
            catch (Exception e)
            {

                string message = string.Format("xlua exception : {0} \n {1}", e.Message, e.StackTrace);
                ProjectUnity.Debug.LogError(message);
            }
        }

    }
}
