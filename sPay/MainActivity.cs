using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using System;

namespace sPay
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            AppCompatDelegate.CompatVectorFromResourcesEnabled = true;
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.main);
            ImageButton imageButton1 = FindViewById<ImageButton>(Resource.Id.imageButton1);
            ImageButton imageButton2 = FindViewById<ImageButton>(Resource.Id.imageButton2);
            ImageButton imageButton3 = FindViewById<ImageButton>(Resource.Id.imageButton3);
            //imageButton1.SetBackgroundResource(Resource.Drawable.credit_card_outline);
            //imageButton2.SetBackgroundResource(Resource.Drawable.shopping_outline);
            //imageButton3.SetBackgroundResource(Resource.Drawable.history);
            imageButton1.Click += ImageButton1_Click;
            //imageView = FindViewById<ImageView>(Resource.Id.imageView1);
            //avd = AnimatedVectorDrawableCompat.Create(this, Resource.Drawable.avd_feed);
            //avd.RegisterAnimationCallback(new AnimatedCallback(this, imageView, avd));
            StartUpdateService();
            //var serviceConnection = new TimestampServiceConnection(this);
            //BindService(serviceToStart, serviceConnection, Bind.AutoCreate);
        }

        private void ImageButton1_Click(object sender, System.EventArgs e)
        {
            //Toast.MakeText(this, "Helo", ToastLength.Short).Show();
            Intent intent = new Intent(this, typeof(CheckCredit));
            StartActivity(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            StopUpdateService();
        }
        public void StartUpdateService()
        {
            Intent myIntent = new Intent(this, typeof(UpdateService));
            this.StartService(myIntent);
        }
        public void StopUpdateService()
        {
            //Intent myIntent = new Intent(this, typeof(UpdateService));
            //this.StopService(myIntent);
        }

    }
}