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
using sPay.BroadcastReceivers;

namespace sPay
{
    [Activity(Label = "ChekCredit")]
    public class CheckCredit : Activity
    {
        //Receiver receiver;
        //IntentFilter filter;
        NfcAdapter mNfcAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CheckCreditLay);
            ImageView imageView1 = FindViewById<ImageView>(Resource.Id.imageView1);
            ImageView imageView2 = FindViewById<ImageView>(Resource.Id.imageView2);
            imageView1.SetImageResource(Resource.Drawable.credit_card_outline);
            // Create your application here
            //receiver = new Receiver();
            //receiver.NFCdiscovered += Receiver_NFCdiscovered;
            //receiver.ConnectivityChanged += Receiver_ConnectivityChanged;
            //receiver.TagDiscovered += Receiver_TagDiscovered;
            //receiver.TrDiscovered += Receiver_TrDiscovered;
            //filter = new IntentFilter();
            //filter.AddAction("android.net.conn.CONNECTIVITY_CHANGE");
            //filter.AddAction("android.nfc.action.TECH_DISCOVERED");
            //filter.AddAction("android.nfc.action.ADAPTER_STATE_CHANGED");
            //filter.AddAction("android.nfc.action.TRANSACTION_DETECTED");
            mNfcAdapter = NfcAdapter.GetDefaultAdapter(this);
        }
        #region Broadcast events
        //private void Receiver_TrDiscovered(object sender, EventArgs e)
        //{
        //    Toast.MakeText(this, "Transaction", ToastLength.Short).Show();
        //}

        //private void Receiver_TagDiscovered(object sender, EventArgs e)
        //{
        //    Toast.MakeText(this, "TAG TAG TAG", ToastLength.Short).Show();
        //}

        //private void Receiver_ConnectivityChanged(object sender, EventArgs e)
        //{
        //    Toast.MakeText(this, "Receiver connectivity", ToastLength.Short).Show();
        //}

        //private void Receiver_NFCdiscovered(object sender, EventArgs e)
        //{
        //    Toast.MakeText(this, "Aptiktas RFID", ToastLength.Short).Show();
        //}
        #endregion

        protected override void OnNewIntent(Intent intent)
        {
            object obj = intent.GetParcelableExtra(NfcAdapter.ExtraTag);
            if (obj != null && obj is Tag)
            {
                Tag t = (Tag)obj;
                byte[] id = t.GetId();
                string[] techList = t.GetTechList();
                int con = t.DescribeContents();
                string objName = t.ToString();
                Toast.MakeText(this, $"{objName} - {con}", ToastLength.Long).Show();
            }
            
        }

        protected override void OnResume()
        {
            base.OnResume();
            //_ = RegisterReceiver(receiver, filter);

            if (mNfcAdapter != null)
            {
                var tagDetected = new IntentFilter(NfcAdapter.ActionTagDiscovered);//or try other Action type
                var filters = new[] { tagDetected };
                var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
                var pendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);
                mNfcAdapter.EnableForegroundDispatch(this, pendingIntent, filters, null);
            }
        }
        protected override void OnPause()
        {
            base.OnPause();
            //UnregisterReceiver(receiver);
            if (mNfcAdapter != null) mNfcAdapter.DisableForegroundDispatch(this);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (mNfcAdapter != null)
            {
                mNfcAdapter.Dispose();
                mNfcAdapter = null;
            }
        }
    }
}