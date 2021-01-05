using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Aplikacja_Csharp2.Pliki_źródłowe.Pola
{
    public class User
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("username")]
        public string username { get; set; }
        [JsonProperty("password")]
        public string password { get; set; }
        [JsonProperty("rank")]
        public string rank { get; set; }

        public int number_of_files_uploaded { get; set; }
        public int number_of_files_downloaded { get; set; }
        public String app_language { get; set; }
        public double total_time_of_uploading { get; set; }
        public double total_time_of_downloading { get; set; }
        public long bytes_uploaded { get; set; }
        public long bytes_downloaded { get; set; }
        public double megabytes_uploaded { get; set; }
        public double megabytes_downloaded { get; set; }
        public double average_time_of_uploading { get; set; }
        public double average_time_of_downloading { get; set; }
        public double raw_average_time_of_uploading { get; set; }
        public double raw_average_time_of_downloading { get; set; }
        public double time_per_megabyte_upload { get; set; }
        public double raw_time_per_megabyte_upload { get; set; }
        public double time_per_megabyte_download { get; set; }
        public double raw_time_per_megabyte_download { get; set; }
        public double total_latency { get; set; }
        public double average_latency { get; set; }
    }
}