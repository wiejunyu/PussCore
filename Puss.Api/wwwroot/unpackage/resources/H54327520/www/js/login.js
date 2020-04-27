function IsLogin() {
	if(GetToken() == null)
	{
		window.location.href = "login.html"
	}
}

function Login(id)
{
    var post_data = $("#from").serializeJSON();//表单序列化
    $.ajax({
        type: "POST",
        url: "http://118.89.182.215:85/api/User/Login",
        contentType: 'application/json;charset=utf-8',
        data: JSON.stringify(post_data),
        headers: {
            Accept: "text/plain"
        },
        success: function (data) {
            if (data.status == 200) {
				sessionStorage.setItem('token', null);
				sessionStorage.setItem('token', data.message);
                alert("登陆成功！");
				window.location.href = "index.html"
            }
            else {
                alert(data.message);
            }
        }
    });
}

var Guid = "";
function CodeKey()
{
    Guid = new GUID().newGUID();
    $("#CodeKey").val(Guid);
    $.ajax({
        type: "POST",
        url: "http://118.89.182.215:85/api/User/ShowValidateCodeBase64?CodeKey=" + Guid,
        success: function (data) {
            if (data.status == 200) {
                $("#imgCode").attr("src", data.message);
            }
            else {
                alert(data.message);
            }
        }
    });
}

function GetToken() {
    return sessionStorage.getItem('token');
}

function OutLogin()
{
	$.ajax({
		type: "POST",
		url: "http://118.89.182.215:85/api/User/LoginOut",
		contentType: 'application/json;charset=utf-8',
		headers: {
			Authorization: 'Bearer ' + GetToken(),
			Accept: "text/plain"
		},
		beforeSend: function (xhr) {
			xhr.setRequestHeader("Authorization", 'Bearer ' + GetToken());
		},
		success: function (data) {
			sessionStorage.removeItem('token');
			window.location.href = "login.html"
		}
	});
}