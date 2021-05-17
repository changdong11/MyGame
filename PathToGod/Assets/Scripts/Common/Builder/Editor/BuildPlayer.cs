using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class BuildPlayer
{
    public static string[] BuildScenes = new string[]
    {
        "Assets/Scenes/Message.unity"
    };
    private static string[] macros =
    {
        "THREAD_SAFE;",//Xlua中线程安全
         "BESTHTTP_DISABLE_SIGNALR;",//整个SignalR实现将被禁用。
         "BESTHTTP_DISABLE_ALTERNATE_SSL;",//禁用HTTPS、WebSocket、HTTYP2，
         "BESTHTTP_DISABLE_SOCKETIO;",//将禁用整个Socket.IO实现

    };
    public static string MACRO_BASE
    {
        get
        {
            string str = "";
            for (int i = 0; i < macros.Length; i++)
            {
                str += macros[i];
            }
            return str;
        }
    }
}
