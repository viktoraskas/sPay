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

    [IntentFilter(new[] { "android.net.conn.CONNECTIVITY_CHANGE", "android.nfc.action.TECH_DISCOVERED", "android.nfc.action.TRANSACTION_DETECTED", "android.nfc.action.ADAPTER_STATE_CHANGED" })]
    public class Receiver : BroadcastReceiver
    {
        public event EventHandler ConnectivityChanged;
        public event EventHandler NFCdiscovered;
        public event EventHandler TagDiscovered;
        public event EventHandler TrDiscovered;

        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action == "android.net.conn.CONNECTIVITY_CHANGE")
            {
                ConnectivityChanged?.Invoke(this, EventArgs.Empty);
            }
            if (intent.Action == "android.nfc.action.TECH_DISCOVERED")
            {
                NFCdiscovered?.Invoke(this, EventArgs.Empty);
            }
            if (intent.Action == "android.nfc.action.TRANSACTION_DETECTED")
            {
                TagDiscovered?.Invoke(this, EventArgs.Empty);
            }
            if (intent.Action == "android.nfc.action.ADAPTER_STATE_CHANGED")
            {
                TrDiscovered?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}