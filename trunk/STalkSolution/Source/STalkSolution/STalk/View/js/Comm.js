﻿/*
通用类
*/
var WinForm = $.extend({
    _CmdSetTitle: "FormSetTitle", //
    _CmdClose: "FormClose",
    _CmdMinSize: "FormMinSize",
    _CmdMaxSize: "FormMaxSize",
    _CmdNormalSize: "FormNormalSize",
    _CmdSetSize: "FormSetSize",
    _CmdSetMaxSize: "FormSetMaxSize",
    _CmdSetMinSize: "FormSetMinSize",
    _CmdShow: "FormShow",
    Show: function () { this.ExternalCall(this._CmdShow); },
    //设置大小
    SetSize: function (width, height) { this.ExternalCall(this._CmdSetSize, width, height); },
    //设置标题
    SetTitle: function (title) {
        this.ExternalCall(this._CmdSetTitle, title);
    },
    //设置最小Size
    SetMinSize: function (width, height) { this.ExternalCall(this._CmdSetMinSize, width, height); },
    //设置最大Size
    SetMaxSize: function (width, height) { this.ExternalCall(this._CmdSetMaxSize, width, height); },
    //调用窗口方法
    ExternalCall: function (action) {
        var param = new Object();
        param.Param = [];
        if (arguments.length > 1) {
            for (var i = 1; i < arguments.length; i++)
                param.Param.push(arguments[i]);
        }

        try {
            window.externalCall(null, action, $.toJSON(param)); //
        }
        catch (e) { alert('Error Call') }
    }
});