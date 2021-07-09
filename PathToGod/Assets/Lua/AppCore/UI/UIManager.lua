local UIManager = BaseClass("UIManager")
print("UIManager 加载---")
--构造函数
local function __init(self)
	print("进入UIManager   __init")
end

local function InitWindow(self)
	
end

local function InnerOpenWindow(self)
	
end


local function InnerCloseWindow(self)
	
end


local function OpenWindow(self)
	
end

local function CloseWindow(self)
	
end

local function DestroyWindow(self)
	
end

local function DestroyAllWindow(self)
	
end

-- 析构函数
local function __delete(self)
end

UIManager.__init = __init
UIManager.InitWindow = InitWindow
UIManager.InnerOpenWindow = InnerOpenWindow
UIManager.InnerCloseWindow = InnerCloseWindow
UIManager.OpenWindow = OpenWindow
UIManager.CloseWindow = CloseWindow
UIManager.DestroyWindow = DestroyWindow
UIManager.DestroyAllWindow = DestroyAllWindow
UIManager.__delete = __delete