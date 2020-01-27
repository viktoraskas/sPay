using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Nfc;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace sPay.BroadcastReceivers
{
    [BroadcastReceiver(Enabled = true)]

    [IntentFilter(new[] { "android.net.conn.CONNECTIVITY_CHANGE" })]
    public class Receiver : BroadcastReceiver
    {
        public event EventHandler ConnectivityChanged;

        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action == "android.net.conn.CONNECTIVITY_CHANGE")
            {
                ConnectivityChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}