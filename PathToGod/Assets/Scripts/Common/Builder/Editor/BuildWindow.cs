using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using ProjectPratice;
public class BuildWindow : EditorWindow
{
    [MenuItem("Tools/BuildWindow")]
    public static void OpenBuilderWindow()
    {
        BuildWindow appWindow = (BuildWindow)BuildWindow.GetWindow(typeof(BuildWindow));
        appWindow.Init();
        appWindow.Show();
        appWindow.titleContent = new GUIContent("打包管理窗口");
    }

    public void Init()
    {
    }
    public string appVersion;
    public string resVersion;
    public string AppGUID;
    private void OnGUI()
    {
        GUILayout.Space(15.0f);
        EditorGUILayout.LabelField("当前版App本号：");
        appVersion = EditorGUILayout.TextArea(appVersion);
        EditorGUILayout.LabelField("当前资源版本号：");
        resVersion = EditorGUILayout.TextArea(resVersion);
        EditorGUILayout.LabelField("app唯一标识");
        AppGUID = EditorGUILayout.TextArea(AppGUID);
        if (GUILayout.Button("生成APPGUID", GUILayout.Height(20)))
        {
            AppGUID = Util.GetGUID();
        }
        GUILayout.Space(30.0f);
        if (GUILayout.Button("打包Bundle资源",GUILayout.Height(35)))
        {
            AssetBundleTools.BuildBundle(BuildTarget.Android, appVersion,resVersion);
        }
        if (GUILayout.Button("打包Android工程", GUILayout.Height(35)))
        {
            BuildProject.BuildAndroid(appVersion,resVersion,AppGUID);
        }
        if (GUILayout.Button("打印参数", GUILayout.Height(35)))
        {
            UnityEngine.Debug.Log("appVersion--"+ appVersion);
            UnityEngine.Debug.Log("resVersion--" + resVersion);
        }
        GUILayout.Space(15.0f);
        if (GUILayout.Button("生成wrap文件", GUILayout.Height(35)))
        {
            CSObjectWrapEditor.Generator.GenAll();
        }
        if (GUILayout.Button("清除wrap文件", GUILayout.Height(35)))
        {
            CSObjectWrapEditor.Generator.ClearAll();
        }
    }

    [MenuItem("Tools/AssetBundle/LoadInAssetbundle",true)]
    public static bool LoadAssetBundleValidate()
    {
        UnityEngine.Debug.Log("LoadAssetBundleValidate");
        Menu.SetChecked("Tools/AssetBundle/LoadInAssetbundle", ProjectPratice.BuildPath.EDITOR_LOAD_ASSETBUNDLE);
        return true;
    }
    [MenuItem("Tools/AssetBundle/LoadInAssetbundle")]
    public static void LoadAssetBundle()
    {
        UnityEngine.Debug.Log("LoadAssetBundle");
        ProjectPratice.BuildPath.EDITOR_LOAD_ASSETBUNDLE = !ProjectPratice.BuildPath.EDITOR_LOAD_ASSETBUNDLE;
    }
}
