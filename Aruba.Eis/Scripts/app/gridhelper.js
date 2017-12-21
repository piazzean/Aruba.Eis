$(function() {
        $('.edit').hide();
        $('.edit-case').on('click', function() {
            var tr = $(this).parents('tr:first');
            tr.find('.edit, .read').toggle();
        });
        $('.update-case').on('click', function (e) {
            e.preventDefault();
            var tr = $(this).parents('tr:first');
            id = $(this).prop('id');
            var minoccurs = tr.find('#MinOccurs').val();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/ActivityResource/Update",
                data: JSON.stringify({ "Id":id,"MinOccurs": minoccurs}),
                dataType: "json",
                success: function(data) {
                    tr.find('.edit, .read').toggle();
                    $('.edit').hide();
                    tr.find('#minoccurs').text(data.MinOccurs);
                },
                error: function (err) {
                    alert("error");
                }
            });
        });
        $('.cancel-case').on('click', function (e) {
            e.preventDefault();
            var tr = $(this).parents('tr:first');
            var id = $(this).prop('id');
            tr.find('.edit, .read').toggle();
            $('.edit').hide();
        });
        /*
        $('.delete-case').on('click', function(e) {
            e.preventDefault();
            var tr = $(this).parents('tr:first');
            id = $(this).prop('id');
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: "/ActivityRole/Delete/" + id,
                dataType: "json",
                success: function (data) {
                    alert('Delete success');
                    window.location.href = "/Default2/Index";
                },
                error: function() {
                    alert('Error occured during delete.');
                }
            });
        });
        */
    });