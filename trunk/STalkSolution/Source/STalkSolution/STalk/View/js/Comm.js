/*
通用类
*/
var WinForm = $.extend({
    _CmdSetTitle: "FormSetTitle", //
    _CmdClose: "FormClose",
    _CmdMinSize: "FormMinSize",
    _CmdMaxSize: "FormMaxSize",
    _CmdNormalSize: "FormNormalSize",
    _CmdSetSize: "FormSetSize",
    //设置大小
    SetSize: function (width, height) { this.ExternalCall(this._CmdSetSize, width, height); },
    //设置标题
    SetTitle: function (title) { this.ExternalCall(_CmdSetTitle, title); },
    //设置最小Size
    SetMinSize: function (width, height) { },
    //设置最大Size
    SetMaxSize: function (width, height) { },
    //调用窗口方法
    ExternalCall: function (action) {
        var param = new Object();
        if (arguments.length > 1) {
            param.Param = [];
            for (var i = 1; i < arguments.length; i++)
                param.Param.push(arguments[i]);
        }

        try {
            window.externalCall(null, action, $.toJSON(param));
        }
        catch (e) { }
    }
});