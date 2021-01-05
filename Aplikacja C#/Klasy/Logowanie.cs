using System;
using System.Collections.Generic;
using System.Linq;
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
    public class Logowanie : AppCompatActivity


    {
        private EditText usernamelogin;
        private EditText passwordlogin;
        private Intent main;
        private Button loginb;
        private Button cancelb;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_login);
            String ip = GetString(Resource.String.ip);
            SupportActionBar.Title = ("Logowanie");
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            loginb = FindViewById<Button>(Resource.Id.loginb);
            cancelb = FindViewById<Button>(Resource.Id.logcancelb);


            loginb.Click += async (sender, e) =>
            {
                View view = (View)sender;
                usernamelogin = FindViewById<EditText>(Resource.Id.usernamelogin);
                passwordlogin = FindViewById<EditText>(Resource.Id.passwordlogin);
                String usernamecontent = usernamelogin.Text;
                String passwordcontent = passwordlogin.Text;

                var client = new HttpClient()
                {
                    Timeout = TimeSpan.FromMilliseconds(3000)
                };
                string url = ip + "/accounts/";
                User user = new User();

               try
                {
                    var result = await client.GetAsync(url);
                    var json = await result.Content.ReadAsStringAsync();



                    if (result.IsSuccessStatusCode)
                    {
                        List<User> users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(json);
                        bool logged = false;
                        foreach (var tempuser in users)
                        {
                            if (tempuser.username.Equals(usernamecontent))
                            {
                                if (tempuser.password.Equals(passwordcontent))
                                {
                                    Intent zalogowany = new Intent(this, typeof(Zalogowany));
                                    logged = true;
                                    Toast.MakeText(Application.Context, "Zostałeś zalogowany!",
                            ToastLength.Long).Show();
                                    zalogowany.PutExtra("username", tempuser.username);
                                    zalogowany.PutExtra("rank", tempuser.rank);
                                    Android.Util.Log.Error("username", tempuser.username);
                                    Android.Util.Log.Error("rank", tempuser.rank);
                                    StartActivity(zalogowany);
                                    break;
                                }

                            }
                        }
                        if (!logged)
                        {
                            Toast.MakeText(Application.Context, "Podałeś niepoprawne dane, spróbuj jeszcze raz.",
                            ToastLength.Long).Show();
                        }
                    }
                }
                catch (System.OperationCanceledException ex)
                {
                    Console.WriteLine(ex.Message);
                    //Console.WriteLine(ex.InnerException.Message);

                    Toast.MakeText(Application.Context, "Przekroczono czas odpowiedzi serwera.",
                            ToastLength.Long).Show();

                }

            };

            cancelb.Click += (sender, e) =>
            {
                View view = (View)sender;
                Finish();
            };


        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return true;
        }

    }
}