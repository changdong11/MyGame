using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectPratice;
public class GameFace : Face
{
    protected override List<string> MessageList
    {
        get
        {
            return new List<string>()
                {
                    MessageName.StartManager,
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
            case MessageName.StartManager:
                StartManager();
                break;
            default:
                break;

        }
    }

    private static GameFace Instance;
    public static GameFace instance
    {
        get
        {
            if (Instance == null)
            {
                Instance = GameObject.FindObjectOfType<GameFace>();
                if (Instance == null)
                {
                    GameObject ower = GameObject.Find("GameFace");
                    if (ower == null)
                    {
                        ower = new GameObject("GameFace");
                        GameObject.DontDestroyOnLoad(ower);
                    }
                    if (ower!= null)
                    {
                        ower.AddComponent<GameFace>();
                    }
                    Instance = GameObject.FindObjectOfType<GameFace>();
                }
            }
            return Instance;
        }
    }


    // Start is called before the first frame update
    protected override void Init()
    {
        base.Init();
        print("app face  init");
    }
    public void StartUp()
    {
        SendNotification(MessageName.StartManager);
    }


    private void StartManager()
    {
        AddManager<LogManager>(ManagerName.LogManager);
        AddManager<AssetBundleManager>(ManagerName.AssetBundleManager);
        AddManager<LuaManager>(ManagerName.LuaManager);
        AddManager<AppManager>(ManagerName.AppManager);
        
        
        
    }
    #region LogManager
    private LogManager logManager;
    public LogManager LogManager
    {
        get
        {
            if (logManager == null)
            {
                logManager = GetManager<LogManager>(ManagerName.LogManager);
            }
            return logManager;
        }
    }
    #endregion
    #region AppManager
    private AppManager appManager;
    public AppManager AppManager
    {
        get
        {
            if (appManager == null)
            {
                appManager = GetManager<AppManager>(ManagerName.AppManager);
            }
            return appManager;
        }
    }
    #endregion
    #region AssetBundleManager
    private AssetBundleManager assetBundleManager;
    public AssetBundleManager AssetBundleManager
    {
        get
        {
            if (assetBundleManager == null)
            {
                assetBundleManager = GetManager<AssetBundleManager>(ManagerName.AssetBundleManager);
            }
            return assetBundleManager;
        }
    }
    #endregion

    #region luaManager
    private LuaManager luaManager;
    public LuaManager LuaManager
    {
        get
        {
            if (luaManager == null)
            {
                luaManager = GetManager<LuaManager>(ManagerName.LuaManager);
            }
            return luaManager;
        }
    }
    #endregion
}
