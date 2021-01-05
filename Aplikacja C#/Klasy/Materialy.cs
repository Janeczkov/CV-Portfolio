using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
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
using Java.Net;
using Newtonsoft.Json;
//using static Aplikacja_Csharp2.Pliki_źródłowe.Metody.GetMethod;


namespace Aplikacja_Csharp2.Pliki_źródłowe.Klasy
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class Materialy : AppCompatActivity


    {
        string username, rank;
        ProgressDialog bar;
        int numerpobierania, ilepobrac;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_materialy);
            SupportActionBar.Title = ("Lista materiałów");
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            String ip = GetString(Resource.String.ip);

            username = Intent.GetStringExtra("username");
            rank = Intent.GetStringExtra("rank");
            GetMethod(ip + "/file/all/");






        }




        public void GetMethod(String url)
        {
            Task.Run(async () =>
            {
                LinearLayout linearLayout = FindViewById<LinearLayout>(Resource.Id.linearLayout);
                HttpClient client = new HttpClient();
                String ip = GetString(Resource.String.ip);
                String contentfolder = "Pliki pobrane";
                String path = this.GetExternalFilesDir(null).AbsolutePath;
                Android.Util.Log.Error("path", path);

                File file = new File();

                var result = await client.GetAsync(url);
                var json = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    List<File> files = Newtonsoft.Json.JsonConvert.DeserializeObject<List<File>>(json);

                    int i = 0;
                    int b = 0;
                    var layouty = new List<LinearLayout>();
                    for (var j = 0; j < files.Count; j++)
                    {
                        int k = j;
                        System.Console.WriteLine("to jest j: " + j);
                        System.Console.WriteLine("wtf " + files.Count);




                        LinearLayout linear = (LinearLayout)LayoutInflater.Inflate(Resource.Layout.materialy, linearLayout, false);

                        System.Diagnostics.Debug.WriteLine("jacie " + j);
                        System.Diagnostics.Debug.WriteLine("jaciek " + k);

                        ((TextView)linear.FindViewById(Resource.Id.idt)).Text = "Plik numer " + (j + 1) + " o nazwie " + files[k].filename;

                        layouty.Add(linear);

                        Button downloadb = linear.FindViewById<Button>(Resource.Id.downloadb);
                        Button deleteb = linear.FindViewById<Button>(Resource.Id.deletefb);
                        Button filestatsb = linear.FindViewById<Button>(Resource.Id.filestatsb);

                        if (rank.Equals("1"))
                        {
                            deleteb.Visibility = ViewStates.Invisible;
                        }


                        downloadb.Click += (sender, e) =>
                        {
                            System.Console.WriteLine("kliknalem");
                            View view = (View)sender;
                            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                            builder.SetTitle("Pobieranie pliku");
                            builder.SetMessage("Czy na pewno chcesz pobrac?");
                            builder.SetPositiveButton("Tak", OkAction);
                            builder.SetNegativeButton("Nie", CancelAction);
                            var myCustomDialog = builder.Create();

                            System.Diagnostics.Debug.WriteLine("w downloadzie " + k);

                            myCustomDialog.Show();

                            async void OkAction(object sender, DialogClickEventArgs e)
                            {
                                builder.Dispose();

                                System.Console.WriteLine("ok");
                                Button myButton = sender as Button;
                                System.Console.WriteLine("tu");
                                TextView liczbapobrant = linear.FindViewById<TextView>(Resource.Id.liczbapobrant);
                                int liczbapobran = int.Parse(liczbapobrant.Text);
                                for (int i = 1; i <= liczbapobran; i++)
                                {
                                    System.Console.WriteLine(liczbapobran.ToString());
                                }
                                if (!System.IO.Directory.Exists(path + contentfolder))
                                {
                                    System.IO.Directory.CreateDirectory(path + "/" + contentfolder);
                                    System.Console.WriteLine("Stworzono folder");
                                }
                                bar = new ProgressDialog(this);
                                bar.SetProgressStyle(ProgressDialogStyle.Horizontal);
                                bar.Max = (liczbapobran);
                                bar.Progress = 0;
                                bar.Show();
                                ilepobrac = liczbapobran;
                                numerpobierania = 0;
                                String text = null;

                                String filename = files[k].filename;
                                


                                for (int i = 1; i <= liczbapobran; i++)
                                {

                                    using (WebClient wc = new WebClient())
                                    {

                                        var watch = System.Diagnostics.Stopwatch.StartNew();
                                        wc.DownloadFile(new Uri(ip + "/file/download/" + files[k].id), path + "/" + contentfolder + "/" + files[k].filename);
                                        float czasdownload = 0;
                                        long roznicaping = 0;
                                        float avgping = 0;
                                        watch.Stop();
                                        czasdownload = watch.ElapsedTicks;
                                        bar.Progress = bar.Progress + (bar.Max / ilepobrac);
                                        numerpobierania++;


                                        if (numerpobierania == ilepobrac)
                                        {
                                            bar.Dismiss();
                                            text = "Plik pobrano " + ilepobrac + " razy i zapisano w folderze " + contentfolder;
                                            Toast.MakeText(Application.Context, text,
                                ToastLength.Long).Show();
                                        }
                                        URL url = new URL(ip + "/ping/");
                                        float m;
                                        for (m = 1; m <= 10; m++)
                                        {
                                            watch = System.Diagnostics.Stopwatch.StartNew();
                                            HttpURLConnection urlConnection = (HttpURLConnection)url.OpenConnection();
                                            urlConnection.ConnectTimeout = 1000;
                                            urlConnection.ReadTimeout = 1000;
                                            watch.Stop();
                                            var po = watch.ElapsedTicks;
                                            roznicaping += po;
                                            urlConnection.Disconnect();
                                            System.Diagnostics.Debug.WriteLine(roznicaping / 10000);
                                        }
                                        avgping = roznicaping / m / 10000;
                                        czasdownload = czasdownload / 10000;
                                        System.Diagnostics.Debug.WriteLine("Tyle czasu pobieram: " + czasdownload);

                                        string urlstats = ip + "/file/updatestats/";
                                        Pola.File filestat = new Pola.File();
                                        filestat.temp_avg_latency = avgping;
                                        filestat.temp_download_time = czasdownload;
                                        filestat.temp_platform = GetString(Resource.String.jezyk);
                                        filestat.id = files[k].id;
                                        filestat.temp_filename = files[k].filename;
                                        filestat.temp_username = username;
                                        client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));

                                        var jsonpost = JsonConvert.SerializeObject(filestat);
                                        var content = new StringContent(jsonpost, Encoding.UTF8, "application/json");
                                        string coto = await content.ReadAsStringAsync();

                                        System.Diagnostics.Debug.WriteLine("coto: " + coto);


                                        var responsepost = await client.PutAsync(urlstats + filestat.id, content);
                                        System.Diagnostics.Debug.WriteLine("Statystyka numer " + numerpobierania);

                                    }
                                }

                            }

                            void CancelAction(object sender, DialogClickEventArgs e)
                            {
                                builder.Dispose();
                            }
                        };

                        deleteb.Click += (sender, e) =>
                        {
                            View view = (View)sender;
                            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                            builder.SetTitle("Usunięcie materiału");
                            builder.SetMessage("Czy na pewno usunąć ten materiał?");
                            builder.SetPositiveButton("Tak", OkAction);
                            builder.SetNegativeButton("Nie", CancelAction);
                            var myCustomDialog = builder.Create();

                            myCustomDialog.Show();

                            async void OkAction(object sender, DialogClickEventArgs e)
                            {
                                HttpClient client = new HttpClient();

                                System.Console.WriteLine("takie ip: " + ip + "/file/delete/" + files[k].id);
                                var result = await client.DeleteAsync(ip + "/file/delete/" + files[k].id);
                                var json = await result.Content.ReadAsStringAsync();

                                if (result.IsSuccessStatusCode)
                                {
                                    Toast.MakeText(Application.Context, "Usunięto plik",
                                ToastLength.Long).Show();
                                    Finish();
                                    StartActivity(Intent);
                                }
                                else
                                {
                                    Toast.MakeText(Application.Context, "Wystąpił błąd",
                                ToastLength.Long).Show();
                                }

                                builder.Dispose();

                            }
                            void CancelAction(object sender, DialogClickEventArgs e)
                            {
                                builder.Dispose();
                            }

                        };
                        filestatsb.Click += (sender, e) =>
                        {
                            View view = (View)sender;
                            Intent StatystykiPliku = new Intent(this, typeof(StatystykiPliku));
                            StatystykiPliku.PutExtra("username", username);
                            StatystykiPliku.PutExtra("rank", rank);
                            StatystykiPliku.PutExtra("fileid", files[k].id.ToString());
                            StartActivity(StatystykiPliku);

                        };




                        //newtask.Wait();
                        //});
                    }
                    rysuj(layouty, linearLayout);

                }

                //}).Wait();
            });
        }

        public void rysuj(List<LinearLayout> layouty, LinearLayout linearLayout)
        {
            for (int k = 0; k < layouty.Count; k++)
            {
                int h = k;
                RunOnUiThread(() =>
                {
                    LinearLayout linearLayout = FindViewById<LinearLayout>(Resource.Id.linearLayout);
                    System.Diagnostics.Debug.WriteLine("siam numer " + k);
                    linearLayout.AddView(layouty[h]);
                    //Thread.Sleep(300);
                });
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return true;
        }
    }
}