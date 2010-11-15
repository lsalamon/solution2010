$(document).ready(function () {
    WinForm.SetTitle("STalk");
   // WinForm.SetMinSize(340, 245);
   // WinForm.SetMaxSize(340, 245);
    WinForm.SetSize(340, 245);
    InitWin();
    InitEvent();
    //窗口初始化后必须调用这个显示
    WinForm.Show();
});

function InitWin() {
    $('#win').window({
        title: 'STalk',
        width: 340,
        modal: false,
        collapsible: false,
        shadow: false,
        maximizable: false,
        closed: false,
        top: 0,
        left: 0,
        resizable: false,
        draggable: false,
        height: 245,
        tools: [{ iconCls: 'panel-tool-min',
            handler: function () { WinForm.MinSize(); }
        }, { iconCls: 'panel-tool-close',
            handler: function () { WinForm.Close(); }
        }],
        closable: false,
        minimizable: false,
    });

    //设置拖动
    var header = $('#win').panel("header");
    $(header).mousedown(function (e) {
        if ($(e.target).hasClass('panel-title')) {
            WinForm.Move();
        } 
    })
}

function InitEvent() {
    $('#btnLogin').click(function () {
        if ($('#txtUserName').val() == "") {
            WinForm.MessageBox({ Title: "错误", Message: "请输入账号!" });
            $('#txtUserName').focus();
            return;
        }

        if ($('#txtUserPwd').val() == "") {
            WinForm.MessageBox({ Title: "错误", Message: "请输入密码!" });
            $('#txtUserPwd').focus();
            return;
        }
        $('#txtUserName').attr("disabled", true);
        $('#txtUserPwd').attr("disabled", true);
        $('#btnLogin').attr("disabled", true);
        //设置输入框disabled
        WinForm.ExternalCall("Login", $('#txtUserName').val(), $('#txtUserPwd').val());
    });


}

function OnSocketError() {
    //设置输入框disabled=false
    $('#txtUserName').attr("disabled", false);
    $('#txtUserPwd').attr("disabled", false);
    $('#btnLogin').attr("disabled", false);
}

function OnAuthError() {
    //设置输入框disabled=false
    $('#txtUserName').attr("disabled", false);
    $('#txtUserPwd').attr("disabled", false);
    $('#btnLogin').attr("disabled", false);
}