﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FinControlCore6.Models.AuxiliaryModels
{
    /// <summary>
    /// A full result, as understood by jQuery DataTables.
    /// </summary>
    /// <typeparam name="T">The data type of each row.</typeparam>
    public class DataTableResult<T>
    {
        /// <summary>
        /// The draw counter that this object is a response to - from the draw parameter sent as part of the data request.
        /// Note that it is strongly recommended for security reasons that you cast this parameter to an integer, rather than simply echoing back to the client what it sent in the draw parameter, in order to prevent Cross Site Scripting (XSS) attacks.
        /// </summary>
        [JsonProperty("draw")]
        public int? Draw { get; set; } = null;

        /// <summary>
        /// Total records, before filtering (i.e. the total number of records in the database)
        /// </summary>
        [JsonProperty("recordsTotal")]
        public int RecordsTotal { get; set; }

        /// <summary>
        /// Total records, after filtering (i.e. the total number of records after filtering has been applied - not just the number of records being returned for this page of data).
        /// </summary>
        [JsonProperty("recordsFiltered")]
        public int RecordsFiltered { get; set; }

        /// <summary>
        /// The data to be displayed in the table.
        /// This is an array of data source objects, one for each row, which will be used by DataTables.
        /// Note that this parameter's name can be changed using the ajax option's dataSrc property.
        /// </summary>
        [JsonProperty("data")]
        public IEnumerable<T>? Data { get; set; }

        /// <summary>
        /// Optional: If an error occurs during the running of the server-side processing script, you can inform the user of this error by passing back the error message to be displayed using this parameter.
        /// Do not include if there is no error.
        /// </summary>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string? Error { get; set; } = null;

        [JsonProperty("partialView", NullValueHandling = NullValueHandling.Ignore)]
        public string? PartialView { get; set; } = null;
    }
}
