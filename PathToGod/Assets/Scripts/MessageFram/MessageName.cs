using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPratice
{
    public class MessageName 
    {
        public const string StartManager = "StartManager"; //开始启动manager
        public const string OpenUploadingLog = "OpenUploadingLog"; //打开日志上传
        public const string LoadCommonAB = "LoadCommonAB"; //加载常用ab
        public const string LoadABConfig = "LoadABConfig"; //加载config 文件

        public const string StartLuaEnv = "StartLuaEnv"; //启动lua虚拟机
        public const string LuaDispose = "LuaDispose"; //关闭lua虚拟机
        public const string DoLuaString = "DoLuaString"; //执行lua代码
    }
}

