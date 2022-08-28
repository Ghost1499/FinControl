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
	$('#main_table').DataTable(
		{
			serverSide: true,
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
			columns: columnsArr

		}
	)
	console.log("2. DataTable created")
	});
