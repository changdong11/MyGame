using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    //启动各模块
    // Start is called before the first frame update
    void Awake()
    {
        print("AppManager-----");
        EventCenter.Broadcast(EventType.Launch);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
