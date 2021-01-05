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
//using static Aplikacja_Csharp2.Pliki_źródłowe.Metody.GetMethod;


namespace Aplikacja_Csharp2.Pliki_źródłowe.Klasy
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class SprawdzanieUzytkownikow : AppCompatActivity


    {
        string username, rank;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_materialy);
            SupportActionBar.Title = ("Lista użytkowników");
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            String ip = GetString(Resource.String.ip);

            username = Intent.GetStringExtra("username");
            rank = Intent.GetStringExtra("rank");
            GetMethod(ip + "/accounts/");






        }




        public void GetMethod(String url)
        {
            Task.Run(async () =>
            {
                LinearLayout linearLayout = FindViewById<LinearLayout>(Resource.Id.linearLayout);
                HttpClient client = new HttpClient();
                String ip = GetString(Resource.String.ip);
                String contentfolder = "Materiały";
                String path = this.GetExternalFilesDir(null).AbsolutePath;
                Android.Util.Log.Error("path", path);

                User user = new User();

                var result = await client.GetAsync(url);
                var json = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    List<User> users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(json);

                    int i = 0;
                    int b = 0;
                    var layouty = new List<LinearLayout>();
                    for (var j = 0; j < users.Count; j++)
                    {
                        //files.Count
                        System.Console.WriteLine("to jest j: " + j);
                        System.Console.WriteLine("wtf " + users.Count);




                        LinearLayout linear = (LinearLayout)LayoutInflater.Inflate(Resource.Layout.wypelnij_uzytkownikow, linearLayout, false);

                        System.Diagnostics.Debug.WriteLine("jacie " + j);

                        ((TextView)linear.FindViewById(Resource.Id.usernamet)).Text = ("Użytkownik:\r\n" + users[j].username);
                        if (rank.Equals(1))
                        {
                            ((TextView)linear.FindViewById(Resource.Id.rangt)).Text = ("Ranga: uzytkownik");
                        }
                        else
                        {
                            ((TextView)linear.FindViewById(Resource.Id.rangt)).Text = ("Ranga: administrator");
                        }
                        ((TextView)linear.FindViewById(Resource.Id.platformt)).Text = ("Platforma: " + users[j].app_language);



                        layouty.Add(linear);

                        Button statsb = linear.FindViewById<Button>(Resource.Id.statsb);
                        Button deleteb = linear.FindViewById<Button>(Resource.Id.deleteb);


                        int k = j;
                        statsb.Click += (sender, e) =>
                        {
                            View view = (View)sender;
                            Intent StatystykiUzytkownika = new Intent(this, typeof(StatystykiUzytkownika));
                            StatystykiUzytkownika.PutExtra("username", users[k].username);
                            StatystykiUzytkownika.PutExtra("rank", users[k].rank);
                            StartActivity(StatystykiUzytkownika);
                        };

                        deleteb.Click += (sender, e) =>
                        {
                            View view = (View)sender;
                            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                            builder.SetTitle("Usunięcie użytkownika");
                            builder.SetMessage("Czy na pewno usunąć tego użytkownika?");
                            builder.SetPositiveButton("Tak", OkAction);
                            builder.SetNegativeButton("Nie", CancelAction);
                            var myCustomDialog = builder.Create();

                            myCustomDialog.Show();

                            async void OkAction(object sender, DialogClickEventArgs e)
                            {
                                
                                HttpClient client = new HttpClient();

                                var result = await client.DeleteAsync(ip + "/accounts/" + users[k].id);
                                var json = await result.Content.ReadAsStringAsync();

                                if (result.IsSuccessStatusCode)
                                {
                                    Toast.MakeText(Application.Context, "Usunięto użytkwonika",
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




                    }
                    rysuj(layouty, linearLayout);

                }

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
                    System.Diagnostics.Debug.WriteLine("siam numer " + h);
                    linearLayout.AddView(layouty[h]);
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