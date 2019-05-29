$(function () {
    //Initialize Select2 Elements
    $('.select').select2();

    $('#example2').DataTable({

        "paging": false,
        'info': false,
        'searching': false,
        'ordering': false,
        dom: "Bfrtip",
        buttons: [
          "excel",
          "pdf",
          "print",
          "copy",
          "csv"
        ],
        "initComplete": function (settings, json) {
            $('.buttons-excel').html("<span class='glyphicon glyphicon-th-list'></span> Excel Export");
            $('.buttons-pdf ').html("<span class='glyphicon glyphicon-save'></span> PDF Export");
            $('.buttons-print').html("<span class='glyphicon glyphicon-print'></span> Print");
            $('.buttons-copy').html("<span class='glyphicon glyphicon-copy'></span> Copy");
            $('.buttons-csv').html("<span class='glyphicon glyphicon glyphicon-log-in'></span> CSV");

        }
    });

    $('.demo4_top,.demo4_bottom').bootpag({
        total: @totalpage,
        page: @pageIndex,
        maxVisible: 5,
        leaps: true,
        firstLastUse: true,
        first: '<span aria-hidden="true">&larr;</span>',
        last: '<span aria-hidden="true">&rarr;</span>',
        wrapClass: 'pagination',
        activeClass: 'active',
        disabledClass: 'disabled',
        nextClass: 'next',
        prevClass: 'prev',
        lastClass: 'last',
        firstClass: 'first'
    }).on("page", onpageevent(event, num)).find('.pagination');

});


