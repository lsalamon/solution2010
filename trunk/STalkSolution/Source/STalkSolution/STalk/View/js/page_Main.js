﻿var STalk_Friend_List = null;

$(document).ready(function () {
    WinForm.SetTitle("STalk");
    WinForm.SetSize(220, 480);
    InitWin();
    InitLayout();

    //初始好友树
    InitSTalkFreindList();

    WinForm.Show();
});


function InitSTalkFreindList() {

}


function InitLayout() {
    $('#frmMain').layout();
    $('#frmTop').layout();
    $('#frmCenter').layout();
    $('#STalkPanel').layout();
}

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
        draggable: false,
        height: $(document).height(),
        tools: [{ iconCls: 'panel-tool-min',
            handler: function () { WinForm.MinSize(); }
        }, { iconCls: 'panel-tool-close',
            handler: function () 
            { 
            WinForm.Close(); }
        }],
        closable: false,
        minimizable: false,
        onResize: function (width, height) {
            WinForm.SetSize(width, height);
            $('#win').panel('move', {
                left: 0,
                top: 0
            });
            InitLayout();
        }
    });

    //设置拖动
    var header = $('#win').panel("header");
    $(header).mousedown(function (e) {
        if ($(e.target).hasClass('panel-title')) {
            WinForm.Move();
        }
    })
    //设置拉动的handle maxsize minsize
    var winState = $('#win').data('window');
    var dragState = $(winState.window).data('resizable');
    dragState.options.handles = "e,s,se";
    dragState.options.minWidth = 200;
    dragState.options.minHeight = 300;
}

function OnSocketError() {

}

function switchTab(name) {
    var panel = $('>div', $("#frmTabsPanel"));
    $(panel).each(function (idx) {
        $(this).hide();
    });
    $('#' + name).show();
}
