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
        //WinForm.MaxSize();
    });

    $('#CloseButton').click(function () {
        WinForm.MessageBox({ CallBack: "OnCloseCallBack",
            Title: "提示",
            Message: "是否退出?"}
        );
    });

    //窗口先minsize 然后加载完毕后再设置normalsize 防止页面
    WinForm.NormalSize();

    // alert(window);
});

function OnCloseCallBack(argv) {
    alert(argv[0]);
}