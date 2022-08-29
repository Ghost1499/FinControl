var dataTableId = "main_table"
var columnsArr = []
var columnVal = ""
console.log("0. Creation started")
$(document).ready(function () {
	$("#" + dataTableId + " > thead > tr > th").each(function () {
		columnVal = this.innerText;
		//columnVal = columnVal[0].toLowerCase() + columnVal.slice(1);
		columnsArr.push({ data: columnVal })
		console.log('1. Finding columnsArr')
	})
	//columnsArr = [{ data: "col1" }, { data: "col2" }, {data:"col3"}]
	mainTable = $('#main_table').DataTable(
		{
			stateSave: true,

			serverSide: true,
			processing: true,
			
			ajax: {
				url: "/Home/TableDataSource",
				//url: "ajaxTestData.txt",
				type: "POST",
				contentType: "application/json",
				dataType: "json",
				data: function (d) {
					return JSON.stringify(d);
				},
				dataSrc: "data"
			},
			columns: columnsArr,
			columnDefs: [
				{
					searchable: false,
					orderable: false,
					targets: 0,
				},
			],
			order: [[1, 'asc']],

		}
	)
	mainTable.on('order.dt search.dt', function () {
		let i = 1;

		mainTable.cells(null, 0, { search: 'applied', order: 'applied' }).every(function (cell) {
			this.data(i++);
		});
	}).draw();
	console.log("2. DataTable created")
	console.log("3. Test string")
	});
