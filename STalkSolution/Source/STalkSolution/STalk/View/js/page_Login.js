$(document).ready(function () {
    WinForm.SetTitle("STalk 登录");
    WinForm.SetMinSize(340, 245);
    WinForm.SetMaxSize(340, 245);

    $('#Title').html("STalk 登录");

    //可移动区域不能与其他按钮或者click事件重叠否则click事件无效
    $('#Title').mousedown(function () {
        WinForm.Move();
    });

    $('#MinButton').click(function () {
        WinForm.MinSize();
    });

    $('#MaxButton').click(function () {

    });

    $('#CloseButton').click(function () {
        WinForm.Close();
    });

    $('#btnLogin').click(function () {
        if ($('#txtUserName').val() == "") {
            WinForm.MessageBox({Title:"错误",Message:"请输入账号!"});
            return;
        }

        if ($('#txtUserPwd').val() == "") {
            //
            WinForm.MessageBox({ Title: "错误", Message: "请输入账号!" });
            return;
        }

        WinForm.ExternalCall("Login", $('#txtUserName').val(), $('#txtUserPwd').val());
    });

    WinForm.NormalSize();
});