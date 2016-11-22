//**********  加载新闻表格信息  ***********
window.onload = function () {
    $.ajax({
        url: "/News/GetNewsList",
        type: 'post',
        success: function (data) {
            CreateNewsTable(eval("(" + data + ")"));
        }
    });
}

function CreateNewsTable(data) {
    $('#news_body').empty();
    var newsTd = "";
    for (i = 0; i < data.length; i++) {
        var publish_span;
        if (data[i].is_published == true) { publish_span = "<span class='label label-sm label-success'>已发布</span>" }
        else { publish_span = "<span class='label label-sm label-danger'>未发布</span>" }

        newsTr = "<tr>";
        newsTr += "<td id='newsId'><input type='checkbox' value='" + data[i].id + "' name = 'checkBox' /></td>";
        newsTr += "<td>" + (i+1).toString() + "</td>";
        newsTr += "<td name='newsCatalog'>" + data[i].catalog_name + "</td>";
        newsTr += "<td name='newsTitle'>" + data[i].news_title + "</td>";
        newsTr += "<td name='newsSummary'>" + data[i].news_summary + "</td>";
        newsTr += "<td><button type='button' class='btn btn-default btn-xs' onclick='newsContenEdit(\""+ data[i].id +"\")'><i class='fa fa-edit'></i>&nbsp;编辑</button></td>";
        newsTr += "<td name='publishState'>" + publish_span + "</td>";
        newsTr += "<td>" + data[i].publish_date + "</td>";
        newsTr += "<td>" + data[i].write_date + "</td>";
        newsTr += "<td>" + data[i].view_times + "</td>";
        newsTr += "<td><button type='button' class='btn btn-default btn-xs' onclick='viewNewsDetails(\"" + data[i].id + "\")'><i class='glyphicon glyphicon-eye-open'></i>&nbsp;查看</button></td>";
        newsTr += "</tr>";
        newsTd += newsTr;
    }

    document.getElementById("news_body").innerHTML = newsTd;
}

//新闻条目增删改逻辑处理
function modalChoose(modalState) {
    switch (modalState) {
        case "add":
            $('#newsModal').modal();
            document.getElementById("SaveNewsInfo").setAttribute("onclick", "SaveNewsInfo('add')");
            document.getElementById("newsModalLabel_h4").innerHTML = "新增";
            document.getElementById("choseCatalogName").value = treeNode.text;
            break;
        case "modify":
            var row = GetCheckedNews();
            if ((row.length == 0) || row.length > 1) {
                $('#infoModal').modal();
                document.getElementById("infoModalLabel").innerHTML = "提示";
                document.getElementById("infoLabel").innerHTML = "请选择一个需要修改的新闻条目";
                document.getElementById("newsInfoFotter").style.visibility = 'hidden';
            }
            else {
                $('#newsModal').modal();
                document.getElementById("newsModalLabel_h4").innerHTML = "修改";
                document.getElementById("choseCatalogName").value = row[0].news_catalog;
                document.getElementById("inputTitleName").value = row[0].news_title;
                document.getElementById("textSummary").value = row[0].news_summary;
                document.getElementById("SaveNewsInfo").setAttribute("onclick", "SaveNewsInfo('modify')");
            }
            break;
        case "del":
            var row = GetCheckedNews();
            if ((row.length == 0)) {
                $('#infoModal').modal();
                document.getElementById("infoModalLabel").innerHTML = "提示";
                document.getElementById("infoLabel").innerHTML = "请选择需要删除的新闻条目";
                document.getElementById("newsInfoFotter").style.visibility = 'hidden';
            }
            else {
                $('#infoModal').modal();
                document.getElementById("infoModalLabel").innerHTML = "删除";
                document.getElementById("infoLabel").innerHTML = "确认要执行删除吗？";
                document.getElementById("infoModalOk").setAttribute("onclick", "SaveNewsInfo('del')");
                document.getElementById("infoModal").style.visibility = 'visible';
            }
            break;
        case "publish":
            var row = GetCheckedNews();
            if ((row.length == 0)) {
                $('#infoModal').modal();
                document.getElementById("infoModalLabel").innerHTML = "提示";
                document.getElementById("infoLabel").innerHTML = "请选择需要发布的新闻条目";
                document.getElementById("newsInfoFotter").style.visibility = 'hidden';
            }
            else {
                $('#infoModal').modal();
                document.getElementById("infoModalLabel").innerHTML = "提示";
                document.getElementById("infoLabel").innerHTML = "确认要发布新闻吗？";
                document.getElementById("infoModalOk").setAttribute("onclick", "publishNews()");
                document.getElementById("infoModal").style.visibility = 'visible';
            }
            break;
        default:
            $('#infoModal').modal();
            document.getElementById("infoModalLabel").innerHTML = "提示";
            document.getElementById("infoLabel").innerHTML = "未知信息错误";
            document.getElementById("newsInfoFotter").style.visibility = 'hidden';
            break;
    }
}

//新闻条目增删改业务处理
function SaveNewsInfo(status) {
    switch (status) {
        case "add":
            $.ajax({
                url: "/News/Add",
                data: {
                    news_catalog: treeNode.text,
                    catalog_id: treeNode.id,
                    news_title: document.getElementById("inputTitleName").value
                },
                type: 'post',
                success: function (data) {
                    CreateNewsTable(eval("(" + data + ")"));
                    $('#newsModal').modal('hide');
                }
            });
            break;
        case "modify":
            var row = GetCheckedNews();
            $.ajax({
                url: "/News/Modify",
                data: {
                    id: row[0].news_id,
                    news_title: document.getElementById("inputTitleName").value,
                    news_summary:  document.getElementById("textSummary").value
                },
                type: 'post',
                success: function (data) {
                    CreateNewsTable(eval("(" + data + ")"));
                    $('#newsModal').modal('hide');
                }
            });
            break;
        case "del":
            //将id转为一个字符串
            var id = "";
            for (i = 0; i < row.length; i++) {
                id += row[i].news_id + ',';
            }
            $.ajax({
                url: "/News/Del?id=" + id,
                type: 'post',
                success: function (data) {
                    CreateNewsTable(eval("(" + data + ")"));
                    $('#newsModal').modal('hide');
                }
            });
            break;
        case "publish":
            break;
        default:

    }
}

//新闻发布
function publishNews() {
    $('#infoModal').modal();

    var row = GetCheckedNews();
        //将id转为一个字符串
        var id = "";
        for (i = 0; i < row.length; i++) {
            id += row[i].news_id + ',';
        }
        $.ajax({
            url: "/News/Publish?id=" + id,
            type: 'post',
            success: function (data) {
                CreateNewsTable(eval("(" + data + ")"));
                $('#infoModal').modal('hide');
            }
        });
}


//获得复选框选中的行对象数据
function GetCheckedNews() {
    var row = document.getElementsByName("newsTr");
    var objCheckBox = document.getElementsByName("checkBox");//获取所有的复选框对象
    var objNewsTitle = document.getElementsByName("newsTitle");//获取所有的标题对象
    var objNewsCatalog = document.getElementsByName("newsCatalog");//获取所有的文件夹对象
    var objNewsSummary = document.getElementsByName("newsSummary");//获取所有的新闻简介对象

    var objCheckedRows = new Array();//获取所有复选框选中的行对象

    for (var i = 0; i < objCheckBox.length; i++) {
        if (objCheckBox[i].checked) {
            //var newsItem = new Array();
            var news_id = objCheckBox[i].value;
            var news_title = objNewsTitle[i].innerHTML;
            var news_catalog = objNewsCatalog[i].innerHTML;
            var news_summary = objNewsSummary[i].innerHTML;

            var newsItem = { "news_id": news_id, "news_catalog": news_catalog, "news_title": news_title, "news_summary": news_summary };

            objCheckedRows.push(newsItem);
        }
    }
    return objCheckedRows;
}


//查看新闻条目详情
function viewNewsDetails(id) {
    $('#newsDetailModal').modal();
    $.ajax({
        url: '/News/GetNewsDetails',
        data: {
            id: id
        },
        type: 'post',
        success: function (data) {
            data = eval("(" + data + ")");
            document.getElementById("newsDetailNewsId").value = data.id,
            document.getElementById("newsDetailNewsTitle").value = data.news_title,
            document.getElementById("newsDetailCatalogId").value = data.catalog_id,
            document.getElementById("newsDetailCatalog").value = data.catalog_name,
            document.getElementById("newsDetailCreaterName").value = data.creater_name,
            document.getElementById("newsDetailCreateDate").value = data.create_time,
            document.getElementById("newsDetailUpdaterName").value = data.updater_name,
            document.getElementById("newsDetailUpdatedDate").value = data.updated_time
        }
    });
}


//富文本编辑

var editor = new wangEditor('newsContentEditor');
//普通的自定义菜单
editor.config.menus = [
    'bold', 'underline', 'italic', 'strikethrough', 'eraser', 'forecolor', 'bgcolor', '|', //'|'为菜单分割线
    'quote', 'fontfamily', 'fontsize', 'head', '|',
    'unorderlist', 'orderlist', '|', 'alignleft', 'aligncenter', 'alignright', '|',
    'link', 'unlink', 'table', 'emotion', '|',
    'img', 'video', 'location', 'insertcode', '|',
    'undo', 'redo', 'source', 'fullscreen'
];
editor.config.uploadImgUrl = '/News/UploadImgFile';
editor.create();

var news_id = "";

function newsContenEdit(newsContentId) {

    $('#newsContentModal').modal();
    document.getElementById('newsContentTitleLabel').innerHTML = "内容编辑";

    $.post("/News/GetNewsContent", { "news_id": newsContentId }, function (data, status) {

        news_id = newsContentId;

        //获取新闻内容的编码信息信息
        var newsInfo = $.parseJSON(data);
        editor.$txt.html(newsInfo.news_info);
    });
}

function saveNewsContent() {
    $.post("/News/SaveNewsContent", { "newsID": news_id, "news_content": editor.$txt.html() }, function (status) {
        alert(status);
    });
}



//jstree
var treeNode = new Object();

var treeData = "";
$.post("/Catalog/GetTreeJson", function (data, status) {
    treeData = eval("(" + data + ")");
    //treeData = [{'id':'4FCF90DB-4D03-4421-ACDA-9557CF7DD4BC','text':'新闻管理','children':[{'id':'F79AF2FE-6DF2-4565-BA28-922E5938010B','text':'1','children':[{'id':'0A0FE61B-5BAC-443D-B878-0BDC0A848F82','text':'12','leaf':true},{'id':'5D3A8558-4178-4F08-9A79-7F16264E2AE6','text':'11','leaf':true}]},{'id':'F9517234-4C8E-4D06-B73D-C0BF3C803272','text':'2','children':[{'id':'8EBCCBCB-98BA-45C8-B5A4-620E821B4303','text':'22','children':[{'id':'2AD20D86-F2C7-4246-A552-F15CF6DF20BA','text':'22','leaf':true}]},{'id':'CF31434F-44A9-4EC9-B969-8286E17D1BD2','text':'21','leaf':true}]}]}];
    //console.log(treeData);
    $('#jstreeContainer').jstree({
        'core': {
            'data': treeData
        }
    });
});

$('#jstreeContainer').on("changed.jstree", function (e, data) {
    treeNode.id = data.selected[0];
    treeNode.text = data.instance.get_selected(true)[0].text;
    $.ajax({
        url: "/News/GetNewsList?catalogId="+treeNode.id,
        type: 'post',
        success: function (data) {
            CreateNewsTable(eval("(" + data + ")"));
        }
    });
});

function newsCatalog(state) {
    switch (state) {
        case "add":
            $('#treeviewModal').modal();
            document.getElementById("catalogName").value = "";
            document.getElementById("newsOkButton").onclick = AddNewsCatalog;//"AddNewsCatalog()";
            break;
        case "modify":
            $('#treeviewModal').modal();
            document.getElementById("catalogName").value = treeNode.text;
            document.getElementById("newsOkButton").onclick = ModifyNewsCatalog;
        default: break;
    }
}

function AddNewsCatalog() {
    var catalogName = document.getElementById("catalogName").value;

    $.ajax({
        url: "/Catalog/AddCatalog",
        data: {
            name: catalogName,
            pid: treeNode.id
        },
        type: 'post',
        success: function (data) {
            $('#treeviewModal').modal('hide');
            treeData = eval("(" + data + ")");
            $('#jstreeContainer').jstree({
                'core': {
                    'data': treeData
                }
            });
        }
    });

    //$.post("/Catalog/AddCatalog", { "name": catalogName, "pid": treeNode.id }, function (data, status) {
    //    treeData = eval("(" + data + ")");
    //    $('#jstreeContainer').jstree({
    //        'core': {
    //            'data': treeData
    //        }
    //    });
    //});
}


function ModifyNewsCatalog() {
    var catalogName = document.getElementById("catalogName").value;
    var tree = $('#jstreeContainer');

    $.ajax({
        url: "/Catalog/ModifyCatalog",
        data: {
            name: catalogName,
            id: treeNode.id
        },
        type: 'post',
        success: function (data) {
            $("#myDiv").html();

            $('#treeviewModal').modal('hide');
            treeData = eval("(" + data + ")");

            tree.jstree(true).settings.core.data = treeData;
            tree.jstree(true).refresh();
        }
    });
}