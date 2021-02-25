using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppLaunch : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        //启动app
        EventCenter.AddListener(EventType.ShowMainPanel, ShowMainPanel);
        EventCenter.AddListener(EventType.Launch,StartApp);
        
    }
    void StartApp()
    {
        print("启动app");
        EventCenter.Broadcast(EventType.ShowMainPanel);
    }
    void ShowMainPanel()
    {
        print("展示主页面");
        EventCenter.Broadcast(EventType.BindMainEvent);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
