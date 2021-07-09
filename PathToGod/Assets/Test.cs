using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using ProjectPratice;
using ProjectUnity;
using UnityEngine.UI;
using Debug = ProjectUnity.Debug;

public class Test : MonoManager
{
    protected override List<string> MessageList
    {
        get
        {
            return new List<string>()
            {
                MessageName.CheckNativeMethod
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
            case MessageName.CheckNativeMethod:
                string json = notification.Body as string;
                Call(json);
                break;
            default:
                break;

        }
    }
    protected override void Init()
    {
        base.Init();
        Debug.LogPE("unity 脚本启动--- awake");
        text = GetComponentInChildren<Text>();
    }
    void Call(string jsonPar)
    {
        NativeParameter par = LitJson.JsonMapper.ToObject<NativeParameter>(jsonPar);
        if (par.MethodName == "TestScripts")
        {
            LitJson.JsonData jsonData = LitJson.JsonMapper.ToObject(par.param);
            Debug.LogPE("unity 脚本接收到原生回传--- " + (string)jsonData["par01"]);
            text.text = (string)jsonData["par01"];

        }
    }
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        

    }
   
}
