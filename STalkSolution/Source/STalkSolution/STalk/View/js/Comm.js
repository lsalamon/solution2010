/*
通用类
*/
var WinForm = WinForm || {};
WinForm.Close = "FormClose"; //关闭
WinForm.MinSize = "FormMinSize"; //最小化
WinForm.MaxSize = "FormMaxSize"; //最大化
WinForm.NormalSize = "FormNormalSize";// 普通
WinForm.Move = "FormMove"; //窗口移动
WinForm.Title = "FormSetTitle";
//设置标题
WinForm.SetTitle = function (title) { ExternalCall(WinForm.Title,null, title); };

//调用winform的方法接口,action是命令,后面可以带参数 第二个参数是回调函数，如果不需要回调请设置null
function ExternalCall(action) {
    var param = new Object();
    if (arguments.length > 1) {
        param.CallBack = arguments[1];
        param.Param = [];
        for (var i = 2; i < arguments.length; i++)
            param.Param.push(arguments[i]);
    }

    try {
        window.externalCall(null, action, $.toJSON(param));
    }
    catch (e) { }
}