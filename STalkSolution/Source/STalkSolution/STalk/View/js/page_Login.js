$(document).ready(function () {
    WinForm.SetTitle("STalk 登录");
    WinForm.SetMinSize(340, 245);
    WinForm.SetMaxSize(340, 245);
    InitWin();
    //    $('#Title').html("STalk 登录");

    //    //可移动区域不能与其他按钮或者click事件重叠否则click事件无效
    //    $('#Title').mousedown(function () {
    //        WinForm.Move();
    //    });

    //    $('#MinButton').click(function () {
    //        WinForm.MinSize();
    //    });

    //    $('#MaxButton').click(function () {

    //    });

    //    $('#CloseButton').click(function () {
    //        WinForm.Close();
    //    });

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
        $('#btnLogin').attr("disabled", true);
        WinForm.ExternalCall("Login", $('#txtUserName').val(), $('#txtUserPwd').val());
    });

    WinForm.NormalSize();

    // alert(window.webkitNotifications);
});

function InitWin() {
    $('#win').window({
        title: 'STalk',
        width: $(document).width(),
        modal: false,
        collapsible: false,
        shadow: false,
        maximizable: false,
        closed: false,
        top: 0,
        left: 0,
        resizable: false,
        draggable: false,
        height: $(document).height(),
        onBeforeClose: function () {
            //这里放winform控制
            alert('s');
            return false;
        },
        onResize: function (width, height) {
            WinForm.SetSize(width, height);
            $('#win').panel('move', {
                left: 0,
                top: 0
            });
        }
    });

    //设置拖动
    var header = $('#win').panel("header");
    $(header).mousedown(function (e) { alert($(e.target).hasClass('panel-header')); return false; })
}

function OnSocketError() {
    $('#btnLogin').attr("disabled", false);
}

function OnAuthError() {
    $('#btnLogin').attr("disabled", false);
}