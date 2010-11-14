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
    _CmdSetMaxSize: "FormSetMaxSize",
    _CmdSetMinSize: "FormSetMinSize",
    _CmdShow: "FormShow",
    _CmdMove: "FormMove",
    _CmdMessageBox: "MessageBox",
    MessageBox: function (option) {
        var _option = {
            CallBack: "",
            Title: "",
            Message: "",
            Icon: "16", // None = 0,Error = 16,Hand = 16,Stop = 16,Question = 32,Exclamation = 48,Warning = 48,Information = 64,Asterisk = 64,
            Button: "0" //OK = 0,OKCancel = 1,AbortRetryIgnore = 2,YesNoCancel = 3,YesNo = 4, RetryCancel = 5,
        };
        $.extend(_option,option);
        this.ExternalCall(this._CmdMessageBox, _option);
        //callback 的result
        /*
        None = 0,
        OK = 1,
        Cancel = 2,
        Abort = 3,
        Retry = 4,
        Ignore = 5,
        Yes = 6,
        No = 7,
        */
    },
    Close: function () { this.ExternalCall(this._CmdClose); },
    Move: function () { this.ExternalCall(this._CmdMove); },
    Show: function () { this.ExternalCall(this._CmdShow); },
    //设置大小
    SetSize: function (width, height) { this.ExternalCall(this._CmdSetSize, width, height); },
    //设置标题
    SetTitle: function (title) {
        this.ExternalCall(this._CmdSetTitle, title);
    },
    NormalSize: function () { this.ExternalCall(this._CmdNormalSize); },
    MaxSize: function () { this.ExternalCall(this._CmdMaxSize); },
    MinSize: function () { this.ExternalCall(this._CmdMinSize); },
    //设置最小Size
    SetMinSize: function (width, height) { this.ExternalCall(this._CmdSetMinSize, width, height); },
    //设置最大Size
    SetMaxSize: function (width, height) { this.ExternalCall(this._CmdSetMaxSize, width, height); },
    //调用窗口方法
    ExternalCall: function (action) {
        var param = [];
        if (arguments.length > 1) {
            for (var i = 1; i < arguments.length; i++)
                param.push(arguments[i]);
        }

        try {
            window.externalCall(null, action, $.toJSON(param)); //
        }
        catch (e) { alert(e) }
    }
});