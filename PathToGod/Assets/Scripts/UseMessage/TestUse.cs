using ProjectPratice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUse : MonoManager
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
                //Call(json);
                break;
            default:
                break;

        }
    }
    protected override void Init()
    {
        base.Init();
        GameObject obj = Resources.Load("Platforms/Canvas") as GameObject;
        GameObject.Instantiate(obj);
    }
    TextMesh text;
    // Start is called before the first frame update
    void Start()
    {
        print("进入测试场景 ---");
        NativeParameter par = new NativeParameter();
        par.MethodName = "CallAndroid";
        par.param = "";
        Dictionary<string, string> test = new Dictionary<string, string>();
        test.Add("par01", "aaaaaaa");
        test.Add("par02", "bbbbbbb");
        par.param = LitJson.JsonMapper.ToJson(test);
        MessageCenter.instance.SendNotification(MessageName.InvokeNative, par);

    }
    void Call(string jsonPar)
    {
        NativeParameter par = LitJson.JsonMapper.ToObject<NativeParameter>(jsonPar);
        if (par.MethodName == "TestScripts")
        {
            LitJson.JsonData jsonData = LitJson.JsonMapper.ToObject(par.param);
            
            text.text = (string)jsonData["par01"];
         
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
