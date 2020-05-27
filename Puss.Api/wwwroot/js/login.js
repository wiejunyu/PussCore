function Login()
{
    $("#btnSubmit").attr("disabled", true);
    $("#loading").show();
    $.ajax({
        type: "POST",
        url: GetAjaxUrl() + "/api/User/Login",
        contentType: 'application/json;charset=utf-8',
        data: JSON.stringify($("#from").serializeJSON()),
        headers: {
            Accept: "text/plain"
        },
        success: function (data) {
            if (data.status == 200) {
                window.localStorage.setItem('token', null);
                window.localStorage.setItem('token', data.message);
				window.location.href = "index.html"
            }
            else {
                layui.use('layer', function () {
                    var layer = layui.layer;
                    layer.open({ content: data.message, icon: 2, yes: null });
                });
            }
            $("#loading").hide();
            $("#btnSubmit").attr("disabled", false);
        }
    });
}

var Guid = "";
function CodeKey()
{
    Guid = new GUID().newGUID();
    $("#CodeKey").val(Guid);
    $("#iCode").show();
    $("#imgCode").hide();
    $.ajax({
        type: "POST",
        url: GetAjaxUrl() + "/api/User/ShowValidateCodeBase64?CodeKey=" + Guid,
        success: function (data) {
            if (data.status == 200) {
                $("#imgCode").attr("src", data.message);
            }
            else {
                layui.use('layer', function () {
                    var layer = layui.layer;
                    layer.open({ content: data.message, icon: 2, yes: null });
                });
            }
            $("#iCode").hide();
            $("#imgCode").show();
        }
    });
}

function OutLogin()
{
    $("#loading").show();
	$.ajax({
		type: "POST",
        url: GetAjaxUrl() + "/api/User/LoginOut",
		contentType: 'application/json;charset=utf-8',
		headers: {
			Authorization: 'Bearer ' + GetToken(),
			Accept: "text/plain"
		},
		beforeSend: function (xhr) {
			xhr.setRequestHeader("Authorization", 'Bearer ' + GetToken());
		},
        success: function (data) {
            $("#loading").hide();
            window.localStorage.clear();
			window.location.href = "login.html"
		}
    });
}

function IsLogin() {
    if (GetToken() == null) {
        window.location.href = "login.html"
    }
    else {
        $.ajax({
            type: "POST",
            url: GetAjaxUrl() + "/api/User/IsToken?sToken=" + GetToken(),
            contentType: 'application/json;charset=utf-8',
            headers: {
                Accept: "text/plain"
            },
            success: function (data) {
                if (data.status != 200) {
                    window.location.href = "login.html"
                }
            }
        });
    }
}

function GetToken() {
    return window.localStorage.getItem('token', null);
}