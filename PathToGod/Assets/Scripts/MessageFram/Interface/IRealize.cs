using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPratice
{
    //定义消息实现
    public interface IRealize
    {
        void OnMessage(IConstruction notification);
    }
}
