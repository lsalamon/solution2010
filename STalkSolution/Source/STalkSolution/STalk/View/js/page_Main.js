$(document).ready(function () {
    WinForm.SetTitle("STalk");
    InitWin();
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
        //  resizable: false,
      resizeHandle:"n",
        draggable: false,
        height: $(document).height(),
        tools: [{ iconCls: 'panel-tool-min',
            handler: function () { WinForm.MinSize(); }
        }, { iconCls: 'panel-tool-close',
            handler: function () { WinForm.Close(); }
        }],
        closable: false,
        minimizable: false,
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
    $(header).mousedown(function (e) {
        if ($(e.target).hasClass('panel-title')) {
            WinForm.Move();
        } 
    })
}

function OnSocketError() {
    
}
