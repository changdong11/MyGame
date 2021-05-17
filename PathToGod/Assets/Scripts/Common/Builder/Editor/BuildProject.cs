using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using System.IO;
using UnityEngine;
using CSObjectWrapEditor;
/// <summary>
/// 打包android（xcode）工程工具类
/// </summary>
class BuildProject
{
    static string buildModelPath = Application.streamingAssetsPath + "/Build.json";
    public static string OutProjectTempPath(BuildTarget target)
    {
        return Application.dataPath.Replace("Assets", "") + $"Project/{ target.ToString()}";
    }
    public static void BuildAndroid(string appVersion,string resVersion,string AppGUID)
    {
        //保存配置
        BuildModel model = new BuildModel();
        model.Version = appVersion;
        model.AppGuid = AppGUID;
        model.RunPlatform = "Android";
        File.WriteAllText(buildModelPath, JsonUtility.ToJson(model));
        AssetDatabase.Refresh();
        //打包bundle
        AssetBundleTools.BuildBundle(BuildTarget.Android, appVersion, resVersion);
        AssetDatabase.Refresh();
        string outPath = OutProjectTempPath(BuildTarget.Android);
        BuildAndroidProject(true, outPath);
        AssetDatabase.Refresh();
    }
    static void BuildAndroidProject(bool debug = false,string outPath = null)
    {
        Generator.GenAll();
        
        AssetDatabase.Refresh();
        BuildTarget target = BuildTarget.Android;
        BuildTargetGroup group = BuildTargetGroup.Android;
        PlayerSettings.SetScriptingBackend(group, ScriptingImplementation.Mono2x); //安卓平台使用mono2x
        BuildOptions options = BuildOptions.CompressWithLz4HC | BuildOptions.AcceptExternalModificationsToPlayer;
        PlayerSettings.usePlayerLog = true;
        string macro = BuildPlayer.MACRO_BASE;
        string[] scenes = BuildPlayer.BuildScenes;
        PlayerSettings.SetScriptingDefineSymbolsForGroup(group, macro);
        AssetDatabase.Refresh();
        bool isSuccess = StructureAndroidProject(outPath, scenes,options);
        AssetDatabase.Refresh();
        //Generator.ClearAll();
        AssetDatabase.Refresh();
        PlayerSettings.SetApiCompatibilityLevel(group, ApiCompatibilityLevel.NET_4_6);
        if (isSuccess)
        {
            EditorUtility.DisplayDialog(target.ToString(), "success", "关闭");
        }
    }
    static bool StructureAndroidProject(string outPath,string[] scenes, BuildOptions options)
    {
        try
        {
            if (Directory.Exists(outPath))
            {
                Directory.Delete(outPath, true);
            }
            Directory.CreateDirectory(outPath);
            UnityEditor.Build.Reporting.BuildReport result = BuildPipeline.BuildPlayer(scenes, outPath, BuildTarget.Android, options);
            if (result.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
            {
                string resultStr = result.summary.result.ToString();
                EditorUtility.DisplayDialog(BuildTarget.Android.ToString() , "失败" + resultStr,"确定");
                Debug.LogError("打包函数结果报错");
                return false;
            }
        }
        catch (Exception e)
        {
            EditorUtility.DisplayDialog(BuildTarget.Android.ToString(), "失败：" + e, "ok");
            Debug.LogError("打包编译报错");
            return false;
        }
        return true;
    }
}
