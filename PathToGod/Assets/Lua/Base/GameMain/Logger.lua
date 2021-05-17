--日志输出 截断lua自带print
local Logger = {}

--log 添加堆栈信息
local function PrintAddTraceback(params)
	local tableParams = {}
	if	params then
		for i=1,#params do
			table.insert(tableParams,params[i])
		end
	end 
	local str =  table.concat(tableParams,"   ")
	str = debug.traceback(str,1)
	return str
end
function print(...)
	local params = {...}
	CSProDebug.Log(PrintAddTraceback(params))
end


return Logger