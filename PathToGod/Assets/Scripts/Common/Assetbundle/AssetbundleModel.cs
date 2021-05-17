using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class AssetBundleItem
{
    public string MD5;
    public string Name;
    public string Path;
    public string resVersion;
    public string appVersion;
    public long Size;
    public List<string> Dependencies;
}

[Serializable]
//写入配置文件用
public class LuaAssetBundleItem
{
    public List<AssetBundleItem> Item;
    public LuaAssetBundleItem()
    {
        Item = new List<AssetBundleItem>();
    }
    public string[] GetAllDependencies(string assetBundleName)
    {
        string [] des = null;
        if (Item != null)
        {
            var abitem = Item.Find(ab => ab.Path.Equals(assetBundleName));
            if (abitem != null && abitem.Dependencies != null)
            {
                des = abitem.Dependencies.ToArray();
            }
        }
        return des;
    }


}
[Serializable]
//写入配置文件用
public class ResAssetBundleItem
{
    public List<AssetBundleItem> Item;
    public ResAssetBundleItem()
    {
        Item = new List<AssetBundleItem>();
    }
    public string[] GetAllDependencies(string assetBundleName)
    {
        string[] des = null;
        if (Item != null)
        {
            var abitem = Item.Find(ab => ab.Path.Equals(assetBundleName));
            if (abitem != null && abitem.Dependencies != null)
            {
                des = abitem.Dependencies.ToArray();
            }
        }
        return des;
    }
}

[Serializable]
public class AssetBundleConfig
{
    public LuaAssetBundleItem lua;
    public ResAssetBundleItem res;
    /// <summary>
    /// 取得依赖
    /// </summary>
    /// <param name="assetbundleName">bundle名</param>
    /// <param name="type">1 表示lua 2表示资源</param>
    /// <returns></returns>
    public string[] GetAllDependencise(string assetbundleName,int type)
    {
        string[] des = null;
        if (type == 1)
        {
            if (lua != null)
            {
                des = lua.GetAllDependencies(assetbundleName);
            }
        }
        else if (type == 2)
        {
            des = res.GetAllDependencies(assetbundleName);
        }
        return des;
    }
}