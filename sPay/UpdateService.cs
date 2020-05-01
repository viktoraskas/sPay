using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RestSharp;

namespace sPay
{
    [Service]
    class UpdateService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Toast.MakeText(this, "Update service started", ToastLength.Long).Show();
            RunUpdateLoop();
            return StartCommandResult.Sticky;
        }
        /*
         * When our service is to be destroyed, show a Toast message before the destruction.
         */
        public override void OnDestroy()
        {
            base.OnDestroy();
            Toast.MakeText(this, "Oh Update Service is Destroyed.", ToastLength.Long).Show();
        }

        private async void RunUpdateLoop()
        {

            while (true)
            {
                await Task.Delay(5000);
                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                alert.SetTitle("Confirm delete");
                alert.SetMessage("Lorem ipsum dolor sit amet, consectetuer adipiscing elit.");
                alert.SetPositiveButton("OK", (senderAlert, args) => {
                    Toast.MakeText(this, "Ok button Tapped!", ToastLength.Short).Show();
                });

                Dialog dialog = alert.Create();
                dialog.Window.SetType(Android.Views.WindowManagerTypes.SystemAlert);
                dialog.Show();
            }
        }
    }
}