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

namespace sPay
{
    public class TimestampServiceConnection : Java.Lang.Object, IServiceConnection
    {
        static readonly string TAG = typeof(TimestampServiceConnection).FullName;
        MainActivity mainActivity;
        Messenger messenger;
        public bool IsConnected { get; private set; }
        public Messenger Messenger { get; private set; }

        public TimestampServiceConnection(MainActivity activity)
        {
            IsConnected = false;
            mainActivity = activity;
        }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {

            IsConnected = service != null;
            Messenger = new Messenger(service);

            if (IsConnected)
            {
                // things to do when the connection is successful. perhaps notify the client? enable UI features?
                RunUpdateLoop();
            }
            else
            {
                // things to do when the connection isn't successful.
            }
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            IsConnected = false;
            Messenger = null;

            // Things to do when the service disconnects. perhaps notify the client? disable UI features?

        }

        private async void RunUpdateLoop()
        {

            while (true)
            {
                await Task.Delay(10000);
                Toast.MakeText(null, "Hello from service!!!", ToastLength.Long).Show();
                //Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(null);
                //alert.SetTitle("Confirm delete");
                //alert.SetMessage("Lorem ipsum dolor sit amet, consectetuer adipiscing elit.");
                //alert.SetPositiveButton("OK", (senderAlert, args) =>
                //{
                //    Toast.MakeText(this, "Ok button Tapped!", ToastLength.Short).Show();
                //});

                //Dialog dialog = alert.Create();
                //dialog.Window.SetType(Android.Views.WindowManagerTypes.SystemAlert);
                //dialog.Show();
            }
        }
    }
}