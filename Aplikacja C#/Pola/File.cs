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
    class File
    {
        public int id { get; set; }
        public String filename { get; set; }
        public String author { get; set; }
        public String file_type { get; set; }
        public bool accepted { get; set; }
        public long file_sizeB { get; set; }
        public float file_sizeMB { get; set; }
        public int number_of_downloads_java { get; set; }
        public int number_of_downloads_csharp { get; set; }
        public double total_time_downloaded_java { get; set; }
        public double total_time_downloaded_csharp { get; set; }
        public double average_time_downloaded_java { get; set; }
        public double average_time_downloaded_csharp { get; set; }
        public double raw_average_time_downloaded_java { get; set; }
        public double raw_average_time_downloaded_csharp { get; set; }
        public double time_per_megabyte_download_java { get; set; }
        public double time_per_megabyte_download_csharp { get; set; }
        public double raw_time_per_megabyte_download_java { get; set; }
        public double raw_time_per_megabyte_download_csharp { get; set; }
        public double total_latency_java { get; set; }
        public double total_latency_csharp { get; set; }
        public double average_latency_java { get; set; }
        public double average_latency_csharp { get; set; }
        public float temp_upload_time { get; set; }
        public float temp_download_time { get; set; }
        public string temp_platform { get; set; }
        public float temp_avg_latency { get; set; }
        public string temp_filename { get; set; }
        public string temp_username { get; set; }
    }
}