using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPratice
{
    //定义消息结构
    public interface IConstruction
    {
        string Name { get; }   //消息名
        object Body { get; set; } //消息参数
        string ToString();
    }
}
