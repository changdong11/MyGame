using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace ProjectPratice
{
    public class BuildPath
    {
        public static string Lua_Password = "changdong_";//lua加密密码

        public static string Lua_Scripts_Floder = "Lua"; //Lua脚本存放目录
        public static string Res_Floder = "Res";//资源存放目录
        public static string Lua_temp_Floder = "LuaTempDir";//Lua临时目录
        public static string Project_Data_Floder = Application.dataPath;//项目路径
        public static string FormatToUnityPath(string path)
        {
            if (path == null)
            {
                return null;
            }
            return path.Replace("\\", "/");
        }
        /// <summary>
        /// 编辑器下从bundle加载 
        /// </summary>
#if UNITY_EDITOR
        public static bool EDITOR_LOAD_ASSETBUNDLE
        {
            get
            {
                return UnityEditor.EditorPrefs.GetBool("EDITOR_LOAD_ASSETBUNDLE", false);
            }
            set
            {
                UnityEditor.EditorPrefs.SetBool("EDITOR_LOAD_ASSETBUNDLE", value);
            }
        }
#else
        public static bool EDITOR_LOAD_ASSETBUNDLE = false;
#endif

        public static string Search_Path(string filename)
        {
            // 默认从 streamingAssetsPath 下加载热更包
            //如果persistentDataPath 下有热更包  则从 persistentDataPath 下加载

            //编辑器下加载
            string path = Application.streamingAssetsPath + "/Data" + "/"+filename;
            return path;
        }
    }
}
