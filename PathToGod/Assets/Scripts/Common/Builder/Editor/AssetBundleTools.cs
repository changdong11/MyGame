using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ProjectPratice;
public class AssetBundleTools 
{
    public static string OutBundleTempPath(BuildTarget target)
    {
        return $"AssetBuilds/{ target.ToString()}";
    }
    public static string AssetEquativePath()
    {
        return Application.dataPath.Replace("Assets", "");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">平台</param>
    /// <param name="resVersion">资源版本号</param>
    public static void BuildBundle(BuildTarget target,string appVersion,string resVersion)
    {
        CreatBundleDir(target);
        BuildLua(target);
        BuildRes(target);
        WriteConfig(target, appVersion, resVersion);
        CopyToStreamAssets();
    }
    static string  outBundlePath;
    public static void CreatBundleDir(BuildTarget target)
    {
        outBundlePath = AssetEquativePath() + OutBundleTempPath(target);
        Console.WriteLine(outBundlePath);
        if (!Directory.Exists(outBundlePath))
        {
            Directory.CreateDirectory(outBundlePath);
        }
    }
    public static void BuildLua(BuildTarget target)
    {
        AssetDatabase.RemoveUnusedAssetBundleNames();
        AssetDatabase.Refresh();
        string luaPatn = BuildPath.FormatToUnityPath(BuildPath.Project_Data_Floder + "/"+ BuildPath.Lua_Scripts_Floder);
        string luaTempPath = BuildPath.FormatToUnityPath(BuildPath.Project_Data_Floder + "/" + BuildPath.Lua_temp_Floder);
        if (!Directory.Exists(luaPatn))
        {
            Debug.Log("lua路径为空");
            return;
        }
        if (Directory.Exists(luaTempPath))
        {
            Directory.Delete(luaTempPath, true);
        }
        Directory.CreateDirectory(luaTempPath);
        AssetDatabase.Refresh();
        //lua文件转换成二进制写入临时文件
        string[] files = Directory.GetFiles(luaPatn, "*.lua", SearchOption.AllDirectories);
        foreach (var item in files)
        {
            string fileName = BuildPath.FormatToUnityPath(item);
            string tempName = fileName.Replace(luaPatn, luaTempPath);
            string dir = Path.GetDirectoryName(tempName);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            tempName = tempName.Replace(".lua", ".bytes");
            var buffer = File.ReadAllBytes(fileName);
            Util.SimpleEncryp(buffer, BuildPath.Lua_Password);
            File.WriteAllBytes(tempName, buffer);
        }
        AssetDatabase.Refresh();
        //文件夹打标记
        string [] tempDirs = Directory.GetDirectories(luaTempPath,"*.*",SearchOption.AllDirectories);
        foreach (var item in tempDirs)
        {
            var assetPath = BuildPath.FormatToUnityPath(item.Replace(BuildPath.Project_Data_Floder, "Assets"));
            var itemPath = assetPath.Replace("Assets/"+BuildPath.Lua_temp_Floder+"/", "");
            itemPath = itemPath.Replace("/", "_");
            AssetImporter importer = AssetImporter.GetAtPath(assetPath);
            importer.assetBundleName = itemPath;
        }
        AssetDatabase.Refresh();
        BuildAssetBundleOptions options = BuildAssetBundleOptions.DeterministicAssetBundle
                                            | BuildAssetBundleOptions.UncompressedAssetBundle;
        string outPath = outBundlePath + "/lua";
        if (!Directory.Exists(outPath))
        {
            Directory.CreateDirectory(outPath);
        }
        BuildPipeline.BuildAssetBundles(outPath, options,target);
        Directory.Delete(luaTempPath, true);
        AssetDatabase.Refresh();
        AssetDatabase.RemoveUnusedAssetBundleNames();
        AssetDatabase.Refresh();
    }
    public static void BuildRes(BuildTarget target)
    {
        AssetDatabase.RemoveUnusedAssetBundleNames();
        string resPath = BuildPath.Project_Data_Floder +"/"+ BuildPath.Res_Floder;
        //文件夹打标记
        string[] tempDirs = Directory.GetDirectories(resPath, "*.*", SearchOption.AllDirectories);
        foreach (var item in tempDirs)
        {
            var assetPath = BuildPath.FormatToUnityPath(item.Replace(BuildPath.Project_Data_Floder, "Assets"));
            var label = assetPath.Replace("Assets/" + BuildPath.Res_Floder + "/", "");
            label = label.Replace("/", "_");
            AssetImporter importer = AssetImporter.GetAtPath(assetPath);
            importer.assetBundleName = label;
        }
        AssetDatabase.Refresh();
        AssetDatabase.RemoveUnusedAssetBundleNames();
        AssetDatabase.Refresh();
        BuildAssetBundleOptions options = BuildAssetBundleOptions.DeterministicAssetBundle
                                            | BuildAssetBundleOptions.UncompressedAssetBundle;
        string outPath = outBundlePath + "/res";
        if (!Directory.Exists(outPath))
        {
            Directory.CreateDirectory(outPath);
        }
        BuildPipeline.BuildAssetBundles(outPath, options, target);
        
        AssetDatabase.Refresh();
        AssetDatabase.RemoveUnusedAssetBundleNames();
        AssetDatabase.Refresh();
    }
    public static void WriteConfig(BuildTarget target,string appVersion,string resVersion)
    {
        string outConfigPath = outBundlePath + "/config";
        //配置lua
        string luaBundlePath = outBundlePath + "/lua";
        AssetBundleConfig config = new AssetBundleConfig();
        LuaAssetBundleItem luaItem = new LuaAssetBundleItem();
        List<AssetBundleItem> luaList = new List<AssetBundleItem>();
        string[] files = Directory.GetFiles(luaBundlePath);
        foreach (var item in files)
        {
            if (Path.GetExtension(item) == ".meta" || Path.GetExtension(item) == ".manifest")
            {
                File.Delete(item);
                continue;
            }
            AssetBundleItem ab_item = new AssetBundleItem();
            ab_item.resVersion = resVersion;
            ab_item.appVersion = appVersion;
            ab_item.Name = Path.GetFileNameWithoutExtension(item);
            ab_item.Path = "lua/" + Path.GetFileNameWithoutExtension(item);
            FileStream fileStream = new FileStream(item, FileMode.Open);
            ab_item.Size = fileStream.Length;
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retval = md5.ComputeHash(fileStream);
            fileStream.Close();
            StringBuilder vb = new StringBuilder();
            for (int j = 0; j < retval.Length; j++)
            {
                vb.Append(retval[j].ToString("x2"));
            }
            ab_item.MD5 = vb.ToString();
            luaList.Add(ab_item);
        }
        luaItem.Item = luaList;
        config.lua = luaItem;
        //配置res
        string resBundlePath = outBundlePath + "/res";
        ResAssetBundleItem resItem = new ResAssetBundleItem();
        List<AssetBundleItem> resList = new List<AssetBundleItem>();
        string[] res_files = Directory.GetFiles(resBundlePath);
        foreach (var item in res_files)
        {
            if (Path.GetExtension(item) == ".meta" || Path.GetExtension(item) == ".manifest")
            {
                File.Delete(item);
                continue;
            }
            AssetBundleItem ab_item = new AssetBundleItem();
            ab_item.resVersion = resVersion;
            ab_item.appVersion = appVersion;
            ab_item.Name = Path.GetFileNameWithoutExtension(item);
            ab_item.Path = "res/" + Path.GetFileNameWithoutExtension(item);
            FileStream fileStream = new FileStream(item, FileMode.Open);
            ab_item.Size = fileStream.Length;
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retval = md5.ComputeHash(fileStream);
            fileStream.Close();
            StringBuilder vb = new StringBuilder();
            for (int j = 0; j < retval.Length; j++)
            {
                vb.Append(retval[j].ToString("x2"));
            }
            ab_item.MD5 = vb.ToString();
            var itemABName = ((BuildPath.FormatToUnityPath(item)).Replace(resBundlePath + "/","")).ToLower();
            string[] dependencies = AssetDatabase.GetAssetBundleDependencies(itemABName, true);
            List<string> depen = new List<string>();
            foreach (var depenItem in dependencies)
            {
                depen.Add(depenItem);
            }
            ab_item.Dependencies = depen;
            resList.Add(ab_item);
        }
        resItem.Item = resList;
        config.res = resItem;
        if (Directory.Exists(outConfigPath))
        {
            Directory.Delete(outConfigPath,true);
        }
        Directory.CreateDirectory(outConfigPath);
        //for (int i = 0; i < config.res.Item.Count-1; i++)
        //{
        //    Debug.LogError(config.res.Item[i].Name);
        //}
        File.WriteAllText(outConfigPath + "/config.json", JsonUtility.ToJson(config));
        //File.WriteAllText(outConfigPath + "/config.json", LitJson.JsonMapper.ToJson(config));
        //删除res标记
        string resPath = BuildPath.Project_Data_Floder + "/" + BuildPath.Res_Floder;
        //文件夹打标记
        string[] tempDirs = Directory.GetDirectories(resPath, "*.*", SearchOption.AllDirectories);
        foreach (var item in tempDirs)
        {
            var assetPath = BuildPath.FormatToUnityPath(item.Replace(BuildPath.Project_Data_Floder, "Assets"));
            AssetImporter importer = AssetImporter.GetAtPath(assetPath);
            importer.assetBundleName = null;
        }
        AssetDatabase.RemoveUnusedAssetBundleNames();
        AssetDatabase.Refresh();
    }
    public static void CopyToStreamAssets()
    {
        var target = Application.streamingAssetsPath + "/Data";
        if (Directory.Exists(target))
        {
            Directory.Delete(target, true);
        }
        Directory.CreateDirectory(target);
        if (!string.IsNullOrEmpty(outBundlePath))
        {
            Util.CopyDirectory(outBundlePath, target);
        }
        AssetDatabase.Refresh();
    }
}
