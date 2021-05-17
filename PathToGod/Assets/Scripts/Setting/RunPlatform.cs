using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPratice
{
    //判断运行环境
    class RunPlatform
    {
        public static bool UNITY_EDITOR()
        {
#if UNITY_EDITOR
            return true;
#else
            return false;
#endif
        }
    }
}
