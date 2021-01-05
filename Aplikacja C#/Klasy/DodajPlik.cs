using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;


using Android.Net;
using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
//using Aplikacja_Csharp2.Pliki_źródłowe.Metody;
using Aplikacja_Csharp2.Pliki_źródłowe.Pola;
using Android.Provider;
using Android.Util;
using Java.IO;
using Java.Net;
using Newtonsoft.Json;
//using Java.IO;
//using static Aplikacja_Csharp2.Pliki_źródłowe.Metody.GetMethod;


namespace Aplikacja_Csharp2.Pliki_źródłowe.Klasy
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class DodajPlik : AppCompatActivity


    {
        private EditText usernamelogin;
        private EditText passwordlogin;
        private Intent main;
        private Button loginb;
        private Button cancelb;
        private static int READ_REQUEST_CODE = 42;
        Android.Net.Uri uri = null;
        String displayName;
        String size = null;
        private TextView wybrany;
        ProgressDialog bar;
        int numerwysylania, ilewyslac;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_dodajplik);
            String ip = GetString(Resource.String.ip);
            SupportActionBar.Title = ("Dodawanie pliku");
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            string username = Intent.GetStringExtra("username");
            string rank = Intent.GetStringExtra("rank");
            //Android.App.ActionBar actionbar =
            Button przegladajb = FindViewById<Button>(Resource.Id.przegladajb);
            Button wyslijplikb = FindViewById<Button>(Resource.Id.wyslijplikb);
            Button cancelb = FindViewById<Button>(Resource.Id.cancelb);


            przegladajb.Click += (sender, e) =>
            {
                if (ContextCompat.CheckSelfPermission(this,
                            Android.Manifest.Permission.ReadExternalStorage) != Android.Content.PM.Permission.Granted)
                {
                    ActivityCompat.RequestPermissions(this, new string[] { Android.Manifest.Permission.ReadExternalStorage }, 1);
                }
                else
                {
                    PerformFileSearch();
                }


            };
            wyslijplikb.Click += async (sender, e) =>
            {
                View view = (View)sender;

                if (uri == null)
                {
                    Toast.MakeText(Application.Context, "Proszę wybrać plik",
                        ToastLength.Long).Show();
                }
                else if (getFileName(uri).Length > 40)
                {
                    Toast.MakeText(Application.Context, "Nazwa pliku jest za długa",
                        ToastLength.Long).Show();
                }
                else
                {


                    TextView liczbawysylan = FindViewById<TextView>(Resource.Id.liczbawysylan);
                    int ilewysylan = int.Parse(liczbawysylan.Text);
                    bar = new ProgressDialog(this);
                    bar.SetProgressStyle(ProgressDialogStyle.Horizontal);
                    bar.Max = (ilewysylan);
                    bar.Progress = 0;
                    bar.Show();
                    ilewyslac = ilewysylan;
                    numerwysylania = 0;
                    String text = null;

                    String filename = getFileName(uri);
                    


                    for (int j = 1; j <= ilewyslac; j++)
                        {
                        using (var resolverStream = ContentResolver.OpenInputStream(uri))
                        using (var streamContent = new StreamContent(resolverStream))
                        using (var byteArrayContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync()))
                        using (var formDataContent = new MultipartFormDataContent())
                        using (var usernameContent = new StringContent(username))
                        using (var filenameContent = new StringContent(filename))
                        using (var client = new HttpClient())
                        {
                            formDataContent.Add(byteArrayContent, "content", filename);
                            formDataContent.Add(usernameContent, "autor");
                            var watch = System.Diagnostics.Stopwatch.StartNew();
                            var response = await client.PostAsync(ip + "/file/upload/", formDataContent);
                            var json = await response.Content.ReadAsStringAsync();

                            if (response.IsSuccessStatusCode)
                            {
                                
                                Pola.File file = Newtonsoft.Json.JsonConvert.DeserializeObject<Pola.File>(json);

                                bar.Progress = bar.Progress + (bar.Max / ilewyslac);
                                numerwysylania++;
                                if (numerwysylania == ilewyslac)
                                {
                                    bar.Dismiss();
                                    text = "Plik wysłano " + ilewyslac + " razy";
                                    Toast.MakeText(Application.Context, text,
                        ToastLength.Long).Show();
                                }
                                float czasupload = 0;
                                long roznicaping = 0;
                                float avgping = 0;
                                await response.Content.ReadAsStreamAsync();
                                watch.Stop();
                                czasupload = watch.ElapsedTicks;



                                URL url = new URL(ip + "/ping/");
                                float i;
                                for (i = 1; i <= 10; i++)
                                {
                                    watch = System.Diagnostics.Stopwatch.StartNew();
                                    HttpURLConnection urlConnection = (HttpURLConnection)url.OpenConnection();
                                    urlConnection.ConnectTimeout = 1000;
                                    urlConnection.ReadTimeout = 1000;
                                    watch.Stop();
                                    var po = watch.ElapsedTicks;
                                    roznicaping += po;
                                    urlConnection.Disconnect();
                                    urlConnection.Dispose();
                                }
                                avgping = roznicaping / i / 10000;
                                czasupload = czasupload / 10000;
                                System.Diagnostics.Debug.WriteLine("Tyle czasu uploaduje: " + czasupload);

                                string urlstats = ip + "/file/updatestats/";
                                Pola.File filestat = new Pola.File();
                                filestat.temp_avg_latency = avgping;
                                filestat.temp_upload_time = czasupload;
                                filestat.temp_platform = GetString(Resource.String.jezyk);
                                filestat.id = file.id;
                                filestat.temp_filename = file.filename;
                                filestat.temp_username = username;
                                using (var client2 = new HttpClient())
                                {
                                    client2.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                                    var jsonpost = JsonConvert.SerializeObject(filestat);
                                    var content = new StringContent(jsonpost, Encoding.UTF8, "application/json");

                                    var responsepost = await client2.PutAsync(urlstats + file.id, content);
                                }
                                client.DefaultRequestHeaders.ConnectionClose = true;


                            }
                        }
                    }
                }
            };
            cancelb.Click += (sender, e) =>
                    {
                        View view = (View)sender;
                        Finish();
                    };


        }
        public void PerformFileSearch()
        {

            Intent intent = new Intent(Intent.ActionOpenDocument);

            intent.AddCategory(Intent.CategoryOpenable);

            intent.SetType("*/*");

            StartActivityForResult(intent, READ_REQUEST_CODE);
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            if (grantResults.Length > 0
                    && grantResults[0] == Android.Content.PM.Permission.Granted)
            {
                PerformFileSearch();
            }
        }


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent resultData)
        {


            wybrany = FindViewById<TextView>(Resource.Id.wybranyt);

            base.OnActivityResult(requestCode, resultCode, resultData);

            if (requestCode == READ_REQUEST_CODE && resultCode == Result.Ok)
            {
                if (resultData != null)
                {
                    uri = resultData.Data;
                    String[] projection = { Android.Provider.MediaStore.Images.ImageColumns.Data };
                    ICursor cursor = ContentResolver.Query(uri, projection, null, null, null, null);
                    try
                    {
                        if (cursor != null && cursor.MoveToFirst())
                        {
                            displayName = cursor.GetString(cursor.GetColumnIndex(MediaStore.Files.FileColumns.Data));

                            int sizeIndex = cursor.GetColumnIndex(OpenableColumns.Size) + 1;

                            if (!cursor.IsNull(sizeIndex))
                            {
                                size = cursor.GetString(sizeIndex);
                            }
                            else
                            {
                                size = "Unknown";
                            }
                            size = cursor.GetString(sizeIndex);
                            wybrany.Text = getFileName(uri);
                        }
                    }
                    finally
                    {
                        cursor.Close();
                    }
                }
            }
        }

        public String getFileName(Android.Net.Uri uri)
        {
            String result = null;
            if (uri.Scheme.Equals("content"))
            {
                ICursor cursor = ContentResolver.Query(uri, null, null, null, null);
                try
                {
                    if (cursor != null && cursor.MoveToFirst())
                    {
                        result = cursor.GetString(cursor.GetColumnIndex(OpenableColumns.DisplayName));
                    }
                }
                finally
                {
                    cursor.Close();
                }
            }
            if (result == null)
            {
                result = uri.Path;
                int cut = result.LastIndexOf('/');
                if (cut != -1)
                {
                    result = result.Substring(cut + 1);
                }
            }
            return result;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return true;
        }

    }
}