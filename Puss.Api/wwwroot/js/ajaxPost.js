function ajaxPostEntity(data)
{
    $.ajax({
        type: "POST",
        url: "http://localhost:60227/api/ExpressDelivery/GetPrice",
        data: data,
        contentType: "application/x-www-form-urlencoded",
        headers: {
            Authorization: 'Bearer ' + token
        },
        processData: false,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", 'Bearer ' + token);
        },
        success: function (data) {
            console.log(data.message)
        }
    });
}