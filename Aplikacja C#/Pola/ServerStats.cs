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

namespace Aplikacja_Csharp2.Pliki_źródłowe.Pola
{
    class ServerStats
    {
        public int id { get; set; }

        public String platform { get; set; }
        public int total_files_uploaded { get; set; }
        public int total_files_downloaded { get; set; }
        public long total_B_size_of_files_uploaded { get; set; }
        public double total_MB_size_of_files_uploaded { get; set; }
        public long total_B_size_of_files_downloaded { get; set; }
        public double total_MB_size_of_files_downloaded { get; set; }
        public double total_time_uploaded { get; set; }
        public double total_time_downloaded { get; set; }
        public double average_time_uploaded { get; set; }
        public double average_time_downloaded { get; set; }
        public double raw_average_time_uploaded { get; set; }
        public double raw_average_time_downloaded { get; set; }
        public double time_per_megabyte_upload { get; set; }
        public double time_per_megabyte_download { get; set; }
        public double raw_time_per_megabyte_upload { get; set; }
        public double raw_time_per_megabyte_download { get; set; }
        public double total_latency_upload { get; set; }
        public double average_latency_upload { get; set; }
        public double total_latency_download { get; set; }
        public double average_latency_download { get; set; }
    }
}