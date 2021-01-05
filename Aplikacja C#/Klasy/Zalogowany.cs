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
using Aplikacja_Csharp2.Pliki_źródłowe.Pola;


namespace Aplikacja_Csharp2.Pliki_źródłowe.Klasy
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class Zalogowany : AppCompatActivity


    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.zalogowany);
            String ip = GetString(Resource.String.ip);
            SupportActionBar.Title = ("Zostałeś zalogowany");
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            string username = Intent.GetStringExtra("username");
            string rank = Intent.GetStringExtra("rank");

        Button usersb = FindViewById<Button>(Resource.Id.usersb);
        Button dodajb = FindViewById<Button>(Resource.Id.dodajb);
         Button materialyb = FindViewById<Button>(Resource.Id.materialyb);
            
         Button adminb = FindViewById<Button>(Resource.Id.adminb);

        Android.Util.Log.Error("username", username);
            Android.Util.Log.Error("rank", rank);
            if (rank.Equals("3")){
                adminb.Visibility = ViewStates.Visible;
        }


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


                    adminb.Click += (sender, e) => {
                        View view = (View)sender;
                        Intent Admin = new Intent(this, typeof(Admin));
                        Admin.PutExtra("username", username);
                        Admin.PutExtra("rank", rank);
                        StartActivity(Admin);
                    };

            usersb.Click += (sender, e) => {
                View view = (View)sender;
                Intent StatystykiUzytkownika = new Intent(this, typeof(StatystykiUzytkownika));
                StatystykiUzytkownika.PutExtra("username", username);
                StatystykiUzytkownika.PutExtra("rank", rank);
                StartActivity(StatystykiUzytkownika);
            };



        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Finish();
            return true;
        }


    }
}