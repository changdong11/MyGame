using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AwaitFun();
        //AwaitFun();
        test();
    }
    void test()
    {
        Debug.Log("test");
    }
    async void AwaitFun()
    {
        await Task.Run(() =>
       {
           System.Threading.Thread.Sleep(100);
           Debug.Log("AwaitFun02");
       });
        //Debug.Log("AwaitFun02");
        Debug.Log("AwaitFun");
    }



    //加上await 打印顺序  
    // test   --  AwaitFun02    ---   AwaitFun

    //不加 await 打印顺序
    //    --  AwaitFun02    ---   AwaitFun  --  test


    // Update is called once per frame
    void Update()
    {
        
    }
}
