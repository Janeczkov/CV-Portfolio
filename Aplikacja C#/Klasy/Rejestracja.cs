using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
//using Aplikacja_Csharp2.Pliki_źródłowe.Metody;
using Aplikacja_Csharp2.Pliki_źródłowe.Pola;
using Newtonsoft.Json;
//using static Aplikacja_Csharp2.Pliki_źródłowe.Metody.GetMethod;


namespace Aplikacja_Csharp2.Pliki_źródłowe.Klasy
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class Rejestracja : AppCompatActivity


    {
        private EditText usernamereg;
        private EditText passwordreg;
        private Intent main;
        private Button regb;
        private Button cancelb;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_rejestracja);
            SupportActionBar.Title = ("Rejestracja");
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            String ip = GetString(Resource.String.ip);

            regb = FindViewById<Button>(Resource.Id.registerb);
            cancelb = FindViewById<Button>(Resource.Id.regcancelb);

            regb.Click += async (sender, e) =>
            {
                View view = (View)sender;
                usernamereg = FindViewById<EditText>(Resource.Id.usernamereg);
                passwordreg = FindViewById<EditText>(Resource.Id.passwordreg);
                String userregcontent = usernamereg.Text;
                String passregcontent = passwordreg.Text;
                if ((string.IsNullOrEmpty(userregcontent)) || (string.IsNullOrEmpty(passregcontent)))
                {
                    Toast.MakeText(Application.Context, "Poprawnie wypełnij pola",
                            ToastLength.Long).Show();
                }
                else if (userregcontent.Length > 12)
                {
                    Toast.MakeText(Application.Context, "Za długa nazwa użytkownika",
                            ToastLength.Long).Show();
                }
                else
                {

                    HttpClient client = new HttpClient()
                    {
                        Timeout = TimeSpan.FromMilliseconds(3000)
                    };
                    string url = ip + "/accounts/";
                    try
                    {
                        var result = await client.GetAsync(url);
                        var json = await result.Content.ReadAsStringAsync();



                        if (result.IsSuccessStatusCode)
                        {
                            List<User> users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(json);
                            bool alreadyregistered = false;
                            foreach (var tempuser in users)
                            {
                                if (tempuser.username.Equals(userregcontent))
                                {
                                    alreadyregistered = true;
                                    Toast.MakeText(Application.Context, "Konto o podanej nazwie już istnieje",
                                ToastLength.Long).Show();
                                    break;
                                }
                            }
                            if (alreadyregistered == false)
                            {
                                User user = new User();
                                user.username = userregcontent;
                                user.password = passregcontent;
                                user.rank = "1";
                                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                                var jsonpost = JsonConvert.SerializeObject(user);
                                var content = new StringContent(jsonpost, Encoding.UTF8, "application/json");
                                var response = await client.PostAsync(url, content);

                                if (result.IsSuccessStatusCode)
                                {
                                    Toast.MakeText(Application.Context, "Zostałes poprawnie zarejestrowany! Możesz się zalogować.",
                                ToastLength.Long).Show();
                                    StartActivity(typeof(Logowanie));
                                }
                                else
                                {
                                    Toast.MakeText(Application.Context, "Wystąpił błąd",
                                ToastLength.Long).Show();
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("{0} ({1})", (int)result.StatusCode, result.ReasonPhrase);
                        }
                    }
                    catch (System.OperationCanceledException ex)
                    {
                        Console.WriteLine(ex.Message);

                        Toast.MakeText(Application.Context, "Przekroczono czas odpowiedzi serwera.",
                                ToastLength.Long).Show();

                    }

                }


            };
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return true;
        }



    }
}