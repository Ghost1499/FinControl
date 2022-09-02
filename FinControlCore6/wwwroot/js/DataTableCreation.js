function createIndexColumn(dataTable) {
	dataTable.on('order.dt search.dt', function () {
		let i = 1;

		mainTable.cells(null, 0, { search: 'applied', order: 'applied' }).every(function (cell) {
			//console.log(this)
			this.data(i++);
		});
	}).draw();
}

var dataTableId = "main_table"
var startOrderColumnNumber = 0;
var columnsArr = []
var columnVal = ""
console.log("0. Creation started")
$(document).ready(function () {
	$("#" + dataTableId + " > thead > tr > th").each(function () {
		columnVal = this.innerText;
		//columnVal = columnVal[0].toLowerCase() + columnVal.slice(1);
		columnsArr.push({ data: columnVal });
		console.log('1. Finding columnsArr')
	})
	//columnsArr = [{ data: "col1" }, { data: "col2" }, {data:"col3"}]
	mainTable = $('#main_table').DataTable(
		{
			// features
			stateSave: true,
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

			//
			search: {
				return: true,
			},
			columns: columnsArr,
			//columnDefs: [
			//	{
			//		searchable: false,
			//		orderable: false,
			//		targets: 0,
			//	},
			//],
			order: [[startOrderColumnNumber, 'asc']],

		}
	)
	//createIndexColumn(mainTable)
	console.log("2. DataTable created")
	});
