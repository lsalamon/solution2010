﻿$(document).ready(function () {
    WinForm.SetTitle("STalk 登录");
    WinForm.SetMinSize(340, 245);
    WinForm.SetMaxSize(340, 245);
    //WinForm.SetSize(340, 245);

    $('#Title').html("STalk 登录");
    //可移动区域不能与其他按钮或者click事件重叠否则click事件无效
    $('#Title').mousedown(function () {
        WinForm.Move();
    });

    $('#MinButton').click(function () {
        WinForm.MinSize();
    });

    $('#MaxButton').click(function () {
        //WinForm.MaxSize();
    });

    $('#CloseButton').click(function () {
        WinForm.Close();
    });

    WinForm.NormalSize();
});