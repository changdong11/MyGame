-- 全局模块
--require("Base.Debugger.LuaPanda").start("127.0.0.1",8818);
require "Base.Global.GameRequire"
-- 定义为全局模块，整个lua程序的入口类
GameMain = {}
--主入口函数。从这里开始lua逻辑
function Start()
    print("GameMain start...")
    GameManager:Init()
    print("start 执行完毕--")

end
return GameMain