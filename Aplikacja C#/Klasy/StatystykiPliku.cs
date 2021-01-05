using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
//using Aplikacja_Csharp2.Pliki_źródłowe.Metody;
using Aplikacja_Csharp2.Pliki_źródłowe.Pola;
using Java.Text;
using Java.Util;
using Newtonsoft.Json;
//using static Aplikacja_Csharp2.Pliki_źródłowe.Metody.GetMethod;


namespace Aplikacja_Csharp2.Pliki_źródłowe.Klasy
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class StatystykiPliku : AppCompatActivity


    {
        private EditText usernamereg;
        private EditText passwordreg;
        private Intent main;
        private Button regb;
        private Button cancelb;

        private TextView idpliku;
        private TextView nazwapliku;
        private TextView autor;
        private TextView rozmiarbw;
        private TextView rozmiarmbw;
        private TextView pliki_j;
        private TextView pliki_c;
        private TextView czas_j;
        private TextView czas_c;
        private TextView czasraw_j;
        private TextView czasraw_c;
        private TextView czas_na_mb_j;
        private TextView czas_na_mb_c;
        private TextView czasraw_na_mb_j;
        private TextView czasraw_na_mb_c;
        private TextView ping_j;
        private TextView ping_c;

        String filename = "";
        string fileid = "";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.statystyki_pliku);
            SupportActionBar.Title = ("Statystyki pliku");
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            String ip = GetString(Resource.String.ip);
            string username = Intent.GetStringExtra("username");
            string rank = Intent.GetStringExtra("rank");
            fileid = Intent.GetStringExtra("fileid");

            Button zapiszb = FindViewById<Button>(Resource.Id.zapiszb);

            GetStatsMethod(ip + "/file/" + fileid);


            zapiszb.Click += (sender, e) =>
            {
                View view = (View)sender;
                Date now = Java.Util.Calendar.Instance.Time;
                SimpleDateFormat format = new SimpleDateFormat("dd-MM-yyyy HH.mm.ss", Locale.Germany);
                String filenametxt = filename + " " + format.Format(now) + ".txt";
                String filenamecsv = filename + " " + format.Format(now) + ".csv";
                String result = zapiszdopliku(filenametxt, "txt");
                result = result + ", " + zapiszdopliku(filenamecsv, "csv");
                Toast.MakeText(Application.Context, "Rezultat: " + result, ToastLength.Long).Show();
            };

        }

        public String zapiszdopliku(String nazwapliku, String rozszerzenie)
        {
            String result = "Wystąpił błąd";
            String folder = "Statystyki plików";
            String path = this.GetExternalFilesDir(null).AbsolutePath;
            String fullpath = path + "/" + folder + "/" + nazwapliku;

            if (!System.IO.Directory.Exists(path + folder))
            {
                System.IO.Directory.CreateDirectory(path + "/" + folder);
                System.Console.WriteLine("Stworzono folder");
            }



            bool filedone = false;
            if (!System.IO.File.Exists(fullpath))
            {
                System.IO.File.Create(fullpath).Close();
                if (System.IO.File.Exists(fullpath))
                {
                    filedone = true;
                    using (TextWriter tw = new StreamWriter(fullpath))
                    {
                        tw.WriteLine(",Java,C#,,Id pliku," + idpliku.Text);
                        tw.WriteLine("Liczba pobrań," + pliki_j.Text + "," + pliki_c.Text + ",,Nazwa pliku," + nazwapliku);
                        tw.WriteLine("Średni czas pobierania (ms)," + czas_j.Text + "," + czas_c.Text + ",,Autor pliku," + autor.Text);
                        tw.WriteLine("Średni aktualny czas pobierania (ms)," + czasraw_j.Text + "," + czasraw_c.Text + ",,Rozmiar pliku (B)," + rozmiarbw.Text);
                        tw.WriteLine("Średni czas dla 1 MB (ms)," + czas_na_mb_j.Text + "," + czas_na_mb_c.Text + ",,Rozmiar pliku (MB)," + rozmiarmbw.Text);
                        tw.WriteLine("Średni aktualny czas dla 1 MB (ms)," + czasraw_na_mb_j.Text + "," + czasraw_na_mb_c.Text);
                        tw.WriteLine("Średnia wartość opóźnienia (ms)," + ping_j.Text + "," + ping_c.Text);
                        tw.Close();
                        result = "Stworzono plik";
                    }

                }
                else
                {
                    result = "Wystąpił błąd tworzenia pliku";
                }
            }

            return result + " " + rozszerzenie;
        }

        public void GetStatsMethod(String url)
        {
            Task.Run(async () =>
            {
                HttpClient client = new HttpClient();

                ServerStats stat = new ServerStats();

                var result = await client.GetAsync(url);
                var json = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    Pola.File tempfile = Newtonsoft.Json.JsonConvert.DeserializeObject<Pola.File>(json);



                    idpliku = FindViewById<TextView>(Resource.Id.idpliku);
                    nazwapliku = FindViewById<TextView>(Resource.Id.nazwapliku);
                    autor = FindViewById<TextView>(Resource.Id.autor);
                    rozmiarbw = FindViewById<TextView>(Resource.Id.rozmiarbw);
                    rozmiarmbw = FindViewById<TextView>(Resource.Id.rozmiarmbw);
                    pliki_j = FindViewById<TextView>(Resource.Id.download_j);
                    pliki_c = FindViewById<TextView>(Resource.Id.download_c);
                    czas_j = FindViewById<TextView>(Resource.Id.czas_j);
                    czas_c = FindViewById<TextView>(Resource.Id.czas_c);
                    czasraw_j = FindViewById<TextView>(Resource.Id.czasraw_j);
                    czasraw_c = FindViewById<TextView>(Resource.Id.czasraw_c);
                    czas_na_mb_j = FindViewById<TextView>(Resource.Id.czas_na_mb_j);
                    czas_na_mb_c = FindViewById<TextView>(Resource.Id.czas_na_mb_c);
                    czasraw_na_mb_j = FindViewById<TextView>(Resource.Id.czasraw_na_mb_j);
                    czasraw_na_mb_c = FindViewById<TextView>(Resource.Id.czasraw_na_mb_c);
                    ping_j = FindViewById<TextView>(Resource.Id.ping_j);
                    ping_c = FindViewById<TextView>(Resource.Id.ping_c);

                    RunOnUiThread(() =>
                    {
                        idpliku.Text = fileid.ToString();
                        nazwapliku.Text = tempfile.filename.ToString();
                        autor.Text = tempfile.author.ToString();
                        rozmiarbw.Text = (tempfile.file_sizeB).ToString();
                        rozmiarmbw.Text = (tempfile.file_sizeMB).ToString();
                        pliki_j.Text = (tempfile.number_of_downloads_java).ToString();
                        pliki_c.Text = (tempfile.number_of_downloads_csharp).ToString();
                        czas_j.Text = Math.Round(tempfile.average_time_downloaded_java, 2).ToString();
                        czas_c.Text = Math.Round(tempfile.average_time_downloaded_csharp, 2).ToString();
                        czasraw_j.Text = Math.Round(tempfile.raw_average_time_downloaded_java, 2).ToString();
                        czasraw_c.Text = Math.Round(tempfile.raw_average_time_downloaded_csharp, 2).ToString();
                        czas_na_mb_j.Text = Math.Round(tempfile.time_per_megabyte_download_java, 2).ToString();
                        czas_na_mb_c.Text = Math.Round(tempfile.time_per_megabyte_download_csharp, 2).ToString();
                        czasraw_na_mb_j.Text = Math.Round(tempfile.raw_time_per_megabyte_download_java, 2).ToString();
                        czasraw_na_mb_c.Text = Math.Round(tempfile.raw_time_per_megabyte_download_csharp, 2).ToString();
                        ping_j.Text = Math.Round(tempfile.average_latency_java, 2).ToString();
                        ping_c.Text = Math.Round(tempfile.average_latency_csharp, 2).ToString();
                    });



                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)result.StatusCode, result.ReasonPhrase);
                }
            });

        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return true;
        }
    }
}