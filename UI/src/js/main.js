
    $(document).ready(function ()
    {
        $.getJSON("http://localhost:4342/api/message/",
            function (Data) {

                $.each(Data, function (key, val)
                {
                    var str = val.id + ': ' + val.message;
                    $('<li/>', { text: str })
                        .appendTo($('#messages'));
                });
            });
    });
    function show() {
        var Id = $('#itId').val();
        $.getJSON("http://localhost:4342/api/message/" + Id,
            function (Data) {
                var str = Data.id + ': ' + Data.message;
                $('#messages').text(str);
            })
            .fail(
                function (jqXHR, textStatus, err) {
                    $('#messages').text('Error: ' + err);
                });
    }
