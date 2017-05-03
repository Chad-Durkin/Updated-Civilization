$(document).ready(function () {
    $('.divTableCell').click(function () {
        $.ajax({
            type: 'GET',
            data: { clickedTileId: this.id},
            url: '@Url.Action("MoveToStone")',
            success: function (result) {
                $('#result1').html(result);
            }
        });
    });
});