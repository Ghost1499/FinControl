function createIndexColumn(dataTable) {
    dataTable.on('order.dt search.dt', function () {
        let i = 1;

        mainTable.cells(null, 0, { search: 'applied', order: 'applied' }).every(function (cell) {
            //console.log(this)
            this.data(i++);
        });
    }).draw();
}
function columnSearchHandler() {
    var columnApi = this;
    $("#input" + this.index()).focusout(function() {
        if (columnApi.search() !== this.value) {
            columnApi.search(this.value);
        }
    });
    $("#input" + this.index()).keyup(function (e) {
        if (e.key === "Enter" || e.keyCode === 13) {
            if (columnApi.search() !== this.value) {
                columnApi.search(this.value).draw();
            }
        }
    });
}
var dataTableId = "main_table"
var startOrderColumnNumber = 0;
var columnsArr = [];
var columnVal = "";
console.log("0. Creation started")
$(document).ready(function () {
    $("#" + dataTableId + " > thead > tr.column-names-row > th").each(function () {
        columnVal = this.innerText;
        //columnVal = columnVal[0].toLowerCase() + columnVal.slice(1);
        columnsArr.push({ data: columnVal });
        console.log('1. Finding columnsArr')
    })
    //columnsArr = [{ data: "col1" }, { data: "col2" }, {data:"col3"}]
    mainTable = $('#main_table').DataTable(
        {
            // features
            //stateSave: true,
            serverSide: true,
            processing: true,

            // data
            ajax: {
                url: "/Home/TableDataSource",
                type: "POST",
                contentType: "application/json",
                dataType: "json",
                data: function (d) {
                    return JSON.stringify(d);
                },
                //dataSrc: "data"
            },

            // callbacks
            initComplete: function () {
                this.api()
                    .columns()
                    .every(columnSearchHandler)
            },

            // options
            search: {
                return: true,
            },
            order: [[startOrderColumnNumber, 'asc']],

            // columns
            columns: columnsArr,
            //columnDefs: [
            //	{
            //		searchable: false,
            //		orderable: false,
            //		targets: 0,
            //	},
            //],

        }
    )
    //createIndexColumn(mainTable)
    console.log("2. DataTable created")
});
