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
//using static Aplikacja_Csharp2.Pliki_źródłowe.Metody.GetMethod;


namespace Aplikacja_Csharp2.Pliki_źródłowe.Klasy
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class Admin : AppCompatActivity


    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_admin);
            String ip = GetString(Resource.String.ip);
            SupportActionBar.Title = ("Panel administratora");
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            string username = Intent.GetStringExtra("username");
            string rank = Intent.GetStringExtra("rank");

        Button usersb = FindViewById<Button>(Resource.Id.usersb);
         Button serwersb = FindViewById<Button>(Resource.Id.serwersb);
         Button materialyb = FindViewById<Button>(Resource.Id.materialyb);
         Button uzytkownicyb = FindViewById<Button>(Resource.Id.uzytkownicyb);
            Button dodajb = FindViewById<Button>(Resource.Id.dodajb);

            Android.Util.Log.Error("username", username);
            Android.Util.Log.Error("rank", rank);

            usersb.Click += (sender, e) => {
                View view = (View)sender;
                Intent StatystykiUzytkownika = new Intent(this, typeof(StatystykiUzytkownika));
                StatystykiUzytkownika.PutExtra("username", username);
                StatystykiUzytkownika.PutExtra("rank", rank);
                StartActivity(StatystykiUzytkownika);
            };
            
            serwersb.Click += (sender, e) => {
                View view = (View)sender;
                Intent StatystykiSerwera = new Intent(this, typeof(StatystykiSerwera));
                StatystykiSerwera.PutExtra("username", username);
                StatystykiSerwera.PutExtra("rank", rank);
                StartActivity(StatystykiSerwera);
            };
            uzytkownicyb.Click += (sender, e) => {
                View view = (View)sender;
                Intent SprawdzanieUzytkownikow = new Intent(this, typeof(SprawdzanieUzytkownikow));
                SprawdzanieUzytkownikow.PutExtra("username", username);
                SprawdzanieUzytkownikow.PutExtra("rank", rank);
                StartActivity(SprawdzanieUzytkownikow);
            };
            materialyb.Click += (sender, e) => {
                View view = (View)sender;
                Intent Materialy = new Intent(this, typeof(Materialy));
                Materialy.PutExtra("username", username);
                Materialy.PutExtra("rank", rank);
                StartActivity(Materialy);
            };

            dodajb.Click += (sender, e) => {
                View view = (View)sender;
                Intent DodajPlik = new Intent(this, typeof(DodajPlik));
                DodajPlik.PutExtra("username", username);
                DodajPlik.PutExtra("rank", rank);
                StartActivity(DodajPlik);
            };



        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return true;
        }




    }
}