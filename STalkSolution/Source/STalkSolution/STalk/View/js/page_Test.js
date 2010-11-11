

//统一调用函数，每个页面必须有
function ProcessCmd(json) {
    /*
    json格式
    json.Cmd = 对应执行的函数
    json.Param = 参数
    */
    try {
        alert(json.Cmd);
    }
    catch (e) { }
}

function fuck() {
    var len = arguments.length;
    alert(len);
}

$(document).ready(function () {
    //    $('#title').mousedown(function () {
    //        ExternalCall('FormMove');
    //    });
    //    $('#btnReSize').mousedown(function () {
    //        ExternalCall('FormReSize');
    //    });
    //    WinForm.SetTitle("Iamdcboy!!!!");
    // WinForm.SetTitle("Iamdcboy!!!!");
    alert(WinForm.ExternalCall);
});