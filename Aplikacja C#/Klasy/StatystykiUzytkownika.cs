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
    public class StatystykiUzytkownika : AppCompatActivity


    {
        private EditText usernamereg;
        private EditText passwordreg;
        private Intent main;
        private Button regb;
        private Button cancelb;

        public TextView uzytkownikw;
        public TextView rangaw;
        public TextView jezykw;
        public TextView sredni_czas_pingw;
        public TextView pliki_up;
        public TextView pliki_down;
        public TextView bajty_up;
        public TextView bajty_down;
        public TextView megabajty_up;
        public TextView megabajty_down;
        public TextView sredni_czas_up;
        public TextView sredni_czas_down;
        public TextView sredni_czas_aktualny_up;
        public TextView sredni_czas_aktualny_down;
        public TextView czas_na_mb_up;
        public TextView czas_na_mb_down;
        public TextView aktualny_czas_na_mb_up;
        public TextView aktualny_czas_na_mb_down;

        String app_language = "";
        string username, rank;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.statystyki_uzytkownika);
            SupportActionBar.Title = ("Statystyki użytkownika");
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            String ip = GetString(Resource.String.ip);
             username = Intent.GetStringExtra("username");
             rank = Intent.GetStringExtra("rank");

            Button zapiszb = FindViewById<Button>(Resource.Id.zapiszb);

            GetStatsMethod(ip + "/accounts/username/" + username);


            zapiszb.Click += (sender, e) =>
            {
                View view = (View)sender;
                Date now = Java.Util.Calendar.Instance.Time;
                SimpleDateFormat format = new SimpleDateFormat("dd-MM-yyyy HH.mm.ss", Locale.Germany);
                String filenametxt = username + " " + format.Format(now) + ".txt";
                String filenamecsv = username + " " + format.Format(now) + ".csv";
                String result = zapiszdopliku(filenametxt, "txt");
                result = result + ", " + zapiszdopliku(filenamecsv, "csv");
                Toast.MakeText(Application.Context, "Rezultat: " + result, ToastLength.Long).Show();
            };

        }

        public String zapiszdopliku(String nazwapliku, String rozszerzenie)
        {
            String result = "Wystąpił błąd";
            String folder = "Statystyki użytkownika";
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
                        tw.WriteLine("," + "Pobieranie" + "," + "Wysyłanie" + ",,Nazwa użytkownika," + username);
                        tw.WriteLine("Liczba plików," + pliki_up.Text + "," + pliki_down.Text + ",,Język aplikacji," + app_language);
                        tw.WriteLine("Ilość danych (B)," + bajty_up.Text + "," + bajty_down.Text + ",,Średnia wartość opóźnienia," + sredni_czas_pingw.Text);
                        tw.WriteLine("Ilość danych (MB)," + megabajty_up.Text + "," + megabajty_down.Text);
                        tw.WriteLine("Średni czas (ms)," + sredni_czas_up.Text + "," + sredni_czas_down.Text);
                        tw.WriteLine("Średni czas aktualny (ms)," + sredni_czas_aktualny_up.Text + "," + sredni_czas_aktualny_down.Text);
                        tw.WriteLine("Średni czas dla 1 MB (ms)," + czas_na_mb_up.Text + "," + czas_na_mb_down.Text);
                        tw.WriteLine("Aktualny średni czas dla 1 MB (ms)," + aktualny_czas_na_mb_up.Text + "," + aktualny_czas_na_mb_down.Text);
                        tw.Close();
                        result = "stworzono plik";
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

                User user = new User();

                var result = await client.GetAsync(url);
                var json = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(json);

                    rangaw = FindViewById<TextView>(Resource.Id.rangaw);
                    uzytkownikw = FindViewById<TextView>(Resource.Id.uzytkownikw);
                    jezykw = FindViewById<TextView>(Resource.Id.jezykw);
                    sredni_czas_pingw = FindViewById<TextView>(Resource.Id.sredni_czas_pingw);
                    pliki_up = FindViewById<TextView>(Resource.Id.pliki_up);
                    pliki_down = FindViewById<TextView>(Resource.Id.pliki_down);
                    bajty_up = FindViewById<TextView>(Resource.Id.bajty_up);
                    bajty_down = FindViewById<TextView>(Resource.Id.bajty_down);
                    megabajty_up = FindViewById<TextView>(Resource.Id.megabajty_up);
                    megabajty_down = FindViewById<TextView>(Resource.Id.megabajty_down);
                    sredni_czas_up = FindViewById<TextView>(Resource.Id.sredni_czas_up);
                    sredni_czas_down = FindViewById<TextView>(Resource.Id.sredni_czas_down);
                    sredni_czas_aktualny_up = FindViewById<TextView>(Resource.Id.sredni_czas_aktualny_up);
                    sredni_czas_aktualny_down = FindViewById<TextView>(Resource.Id.sredni_czas_aktualny_down);
                    czas_na_mb_up = FindViewById<TextView>(Resource.Id.czas_na_mb_up);
                    czas_na_mb_down = FindViewById<TextView>(Resource.Id.czas_na_mb_down);
                    aktualny_czas_na_mb_up = FindViewById<TextView>(Resource.Id.aktualny_czas_na_mb_up);
                    aktualny_czas_na_mb_down = FindViewById<TextView>(Resource.Id.aktualny_czas_na_mb_down);

                    RunOnUiThread(() =>
                    {
                        if (user.rank.Equals("1"))
                        {
                            rangaw.Text = ("użytkownik");
                        }
                        else if (user.rank.Equals("3"))
                        {
                            rangaw.Text = ("administrator");
                        }
                        System.Diagnostics.Debug.WriteLine("omgomg np ilosc plikow: " + user.number_of_files_uploaded);
                        uzytkownikw.Text = username;
                        jezykw.Text = user.app_language;
                        sredni_czas_pingw.Text = Math.Round(user.average_latency, 2).ToString();
                        pliki_up.Text = (user.number_of_files_uploaded).ToString();
                        pliki_down.Text = (user.number_of_files_downloaded).ToString();
                        bajty_up.Text = (user.bytes_uploaded).ToString();
                        bajty_down.Text = (user.bytes_downloaded).ToString();
                        megabajty_up.Text = Math.Round(user.megabytes_uploaded, 2).ToString();
                        megabajty_down.Text = Math.Round(user.megabytes_downloaded, 2).ToString();
                        sredni_czas_up.Text = Math.Round(user.average_time_of_uploading, 2).ToString();
                        sredni_czas_down.Text = Math.Round(user.average_time_of_downloading, 2).ToString();
                        sredni_czas_aktualny_up.Text = Math.Round(user.raw_average_time_of_uploading, 2).ToString();
                        sredni_czas_aktualny_down.Text = Math.Round(user.raw_average_time_of_downloading, 2).ToString();
                        czas_na_mb_up.Text = Math.Round(user.time_per_megabyte_upload, 2).ToString();
                        czas_na_mb_down.Text = Math.Round(user.raw_time_per_megabyte_download, 2).ToString();
                        aktualny_czas_na_mb_up.Text = Math.Round(user.raw_time_per_megabyte_upload, 2).ToString();
                        aktualny_czas_na_mb_down.Text = Math.Round(user.raw_time_per_megabyte_download, 2).ToString();
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