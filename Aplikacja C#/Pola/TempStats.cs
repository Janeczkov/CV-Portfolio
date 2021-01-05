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
    class TempStats
    {
        public float temp_upload_time { get; set; }
        public float temp_download_time { get; set; }
        public string temp_platform { get; set; }
        public float temp_avg_latency { get; set; }
        public string temp_filename { get; set; }
    }
}