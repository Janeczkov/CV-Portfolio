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
    public class StatystykiSerwera : AppCompatActivity


    {
        private EditText usernamereg;
        private EditText passwordreg;
        private Intent main;
        private Button regb;
        private Button cancelb;

        private TextView download_j;
        private TextView upload_j;
        private TextView mbpobrane_j;
        private TextView mbwyslane_j;
        private TextView rozmiarmbw;
        private TextView czaspobierania_j;
        private TextView czaswysylania_j;
        private TextView raw_czas_pobierania_j;
        private TextView raw_czas_wysylania_j;
        private TextView czas_na_mb_pobierania_j;
        private TextView czas_na_mb_wysylania_j;
        private TextView rawczas_na_mb_pobierania_j;
        private TextView rawczas_na_mb_wysylania_j;
        private TextView ping_pobierania_j;
        private TextView ping_wysylania_j;
        private TextView download_c;
        private TextView upload_c;
        private TextView mbpobrane_c;
        private TextView mbwyslane_c;
        private TextView czaspobierania_c;
        private TextView czaswysylania_c;
        private TextView raw_czas_pobierania_c;
        private TextView raw_czas_wysylania_c;
        private TextView czas_na_mb_pobierania_c;
        private TextView czas_na_mb_wysylania_c;
        private TextView rawczas_na_mb_pobierania_c;
        private TextView rawczas_na_mb_wysylania_c;
        private TextView ping_pobierania_c;
        private TextView ping_wysylania_c;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.statystyki_serwera);
            SupportActionBar.Title = ("Statystyki serwera");
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            String ip = GetString(Resource.String.ip);
            string username = Intent.GetStringExtra("username");
            string rank = Intent.GetStringExtra("rank");

            Button zapiszb = FindViewById<Button>(Resource.Id.zapiszb);

            GetStatsMethod(ip + "/server/stats/");


            zapiszb.Click += (sender, e) =>
            {
                View view = (View)sender;
                Date now = Java.Util.Calendar.Instance.Time;
                SimpleDateFormat format = new SimpleDateFormat("dd-MM-yyyy HH.mm.ss", Locale.Germany);
                String filename = "Serwer";
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
            String folder = "Statystyki serwera";
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
                        tw.WriteLine(",Java,C#");
                        tw.WriteLine("Liczba pobrań," + download_j.Text + "," + download_c.Text);
                        tw.WriteLine("Liczba udostępnień," + upload_j.Text + "," + upload_c.Text);
                        tw.WriteLine("Rozmiar plików pobranych," + mbpobrane_j.Text + "," + mbpobrane_c.Text);
                        tw.WriteLine("Rozmiar plików wysłanych," + mbwyslane_j.Text + "," + mbwyslane_c.Text);
                        tw.WriteLine("Średni czas pobierania (ms)," + czaspobierania_j.Text + "," + czaspobierania_c.Text);
                        tw.WriteLine("Średni czas wysyłania (ms)," + czaswysylania_j.Text + "," + czaswysylania_c.Text);
                        tw.WriteLine("Aktualny średni czas pobierania (ms)," + raw_czas_pobierania_j.Text + "," + raw_czas_pobierania_c.Text);
                        tw.WriteLine("Aktualny średni czas wysyłania (ms)," + raw_czas_wysylania_j.Text + "," + raw_czas_wysylania_c.Text);
                        tw.WriteLine("Średni czas pobierania dla 1 MB (ms)," + czas_na_mb_pobierania_j.Text + "," + czas_na_mb_pobierania_c.Text);
                        tw.WriteLine("Średni czas wysyłania dla 1 MB (ms)," + czas_na_mb_wysylania_j.Text + "," + czas_na_mb_wysylania_c.Text);
                        tw.WriteLine("Aktualny średni czas pobierania dla 1 MB (ms)," + rawczas_na_mb_pobierania_j.Text + "," + rawczas_na_mb_pobierania_c.Text);
                        tw.WriteLine("Aktualny średni czas wysyłania dla 1 MB (ms)," + rawczas_na_mb_wysylania_j.Text + "," + rawczas_na_mb_wysylania_c.Text);
                        tw.WriteLine("Średnia wartość opóźnienia dla pobierania (ms)," + ping_pobierania_j.Text + "," + ping_pobierania_c.Text);
                        tw.WriteLine("Średnia wartość opóźnienia dla wysyłania (ms)," + ping_wysylania_j.Text + "," + ping_wysylania_c.Text);
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
                    List<ServerStats> stats = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ServerStats>>(json);



                    download_j = FindViewById<TextView>(Resource.Id.download_j);
                    upload_j = FindViewById<TextView>(Resource.Id.upload_j);
                    mbpobrane_j = FindViewById<TextView>(Resource.Id.mbpobrane_j);
                    mbwyslane_j = FindViewById<TextView>(Resource.Id.mbwyslane_j);
                    czaspobierania_j = FindViewById<TextView>(Resource.Id.czaspobierania_j);
                    czaswysylania_j = FindViewById<TextView>(Resource.Id.czaswysylania_j);
                    raw_czas_pobierania_j = FindViewById<TextView>(Resource.Id.raw_czas_pobierania_j);
                    raw_czas_wysylania_j = FindViewById<TextView>(Resource.Id.raw_czas_wysylania_j);
                    czas_na_mb_pobierania_j = FindViewById<TextView>(Resource.Id.czas_na_mb_pobierania_j);
                    czas_na_mb_wysylania_j = FindViewById<TextView>(Resource.Id.czas_na_mb_wysylania_j);
                    rawczas_na_mb_pobierania_j = FindViewById<TextView>(Resource.Id.rawczas_na_mb_pobierania_j);
                    rawczas_na_mb_wysylania_j = FindViewById<TextView>(Resource.Id.rawczas_na_mb_wysylania_j);
                    ping_pobierania_j = FindViewById<TextView>(Resource.Id.ping_pobierania_j);
                    ping_wysylania_j = FindViewById<TextView>(Resource.Id.ping_wysylania_j);

                    download_c = FindViewById<TextView>(Resource.Id.download_c);
                    upload_c = FindViewById<TextView>(Resource.Id.upload_c);
                    mbpobrane_c = FindViewById<TextView>(Resource.Id.mbpobrane_c);
                    mbwyslane_c = FindViewById<TextView>(Resource.Id.mbwyslane_c);
                    czaspobierania_c = FindViewById<TextView>(Resource.Id.czaspobierania_c);
                    czaswysylania_c = FindViewById<TextView>(Resource.Id.czaswysylania_c);
                    raw_czas_pobierania_c = FindViewById<TextView>(Resource.Id.raw_czas_pobierania_c);
                    raw_czas_wysylania_c = FindViewById<TextView>(Resource.Id.raw_czas_wysylania_c);
                    czas_na_mb_pobierania_c = FindViewById<TextView>(Resource.Id.czas_na_mb_pobierania_c);
                    czas_na_mb_wysylania_c = FindViewById<TextView>(Resource.Id.czas_na_mb_wysylania_c);
                    rawczas_na_mb_pobierania_c = FindViewById<TextView>(Resource.Id.rawczas_na_mb_pobierania_c);
                    rawczas_na_mb_wysylania_c = FindViewById<TextView>(Resource.Id.rawczas_na_mb_wysylania_c);
                    ping_pobierania_c = FindViewById<TextView>(Resource.Id.ping_pobierania_c);
                    ping_wysylania_c = FindViewById<TextView>(Resource.Id.ping_wysylania_c);


                    foreach (var tempstat in stats)
                    {
                        RunOnUiThread(() =>
                        {

                            System.Diagnostics.Debug.WriteLine(tempstat.total_files_downloaded.ToString());
                            if (tempstat.platform.Equals("java"))
                            {
                                download_j.Text = (tempstat.total_files_downloaded).ToString();
                                upload_j.Text = (tempstat.total_files_uploaded).ToString();
                                mbpobrane_j.Text = Math.Round(tempstat.total_MB_size_of_files_downloaded, 2).ToString();
                                mbwyslane_j.Text = Math.Round(tempstat.total_MB_size_of_files_uploaded, 2).ToString();
                                czaspobierania_j.Text = Math.Round(tempstat.average_time_downloaded, 2).ToString();
                                czaswysylania_j.Text = Math.Round(tempstat.average_time_uploaded, 2).ToString();
                                raw_czas_pobierania_j.Text = Math.Round(tempstat.raw_average_time_downloaded, 2).ToString();
                                raw_czas_wysylania_j.Text = Math.Round(tempstat.raw_average_time_uploaded, 2).ToString();
                                czas_na_mb_pobierania_j.Text = Math.Round(tempstat.time_per_megabyte_download, 2).ToString();
                                czas_na_mb_wysylania_j.Text = Math.Round(tempstat.time_per_megabyte_upload, 2).ToString();
                                rawczas_na_mb_pobierania_j.Text = Math.Round(tempstat.raw_time_per_megabyte_download, 2).ToString();
                                rawczas_na_mb_wysylania_j.Text = Math.Round(tempstat.raw_time_per_megabyte_upload, 2).ToString();
                                ping_pobierania_j.Text = Math.Round(tempstat.average_latency_download, 2).ToString();
                                ping_wysylania_j.Text = Math.Round(tempstat.average_latency_upload, 2).ToString();
                            }
                            else if (tempstat.platform.Equals("csharp"))
                            {
                                download_c.Text = (tempstat.total_files_downloaded).ToString();
                                upload_c.Text = (tempstat.total_files_uploaded).ToString();
                                mbpobrane_c.Text = Math.Round(tempstat.total_MB_size_of_files_downloaded, 2).ToString();
                                mbwyslane_c.Text = Math.Round(tempstat.total_MB_size_of_files_uploaded, 2).ToString();
                                czaspobierania_c.Text = Math.Round(tempstat.average_time_downloaded, 2).ToString();
                                czaswysylania_c.Text = Math.Round(tempstat.average_time_uploaded, 2).ToString();
                                raw_czas_pobierania_c.Text = Math.Round(tempstat.raw_average_time_downloaded, 2).ToString();
                                raw_czas_wysylania_c.Text = Math.Round(tempstat.raw_average_time_uploaded, 2).ToString();
                                czas_na_mb_pobierania_c.Text = Math.Round(tempstat.time_per_megabyte_download, 2).ToString();
                                czas_na_mb_wysylania_c.Text = Math.Round(tempstat.time_per_megabyte_upload, 2).ToString();
                                rawczas_na_mb_pobierania_c.Text = Math.Round(tempstat.raw_time_per_megabyte_download, 2).ToString();
                                rawczas_na_mb_wysylania_c.Text = Math.Round(tempstat.raw_time_per_megabyte_upload, 2).ToString();
                                ping_pobierania_c.Text = Math.Round(tempstat.average_latency_download, 2).ToString();
                                ping_wysylania_c.Text = Math.Round(tempstat.average_latency_upload, 2).ToString();
                            }
                        });

                    }


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