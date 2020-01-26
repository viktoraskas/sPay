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
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using RestSharp;
using RestSharp.Serialization.Json;
using sPay.Helpers;
using Android.Support.V7.App;

namespace sPay
{
    [Activity(Label = "ChekCredit")]
    public class CheckCredit : Activity
    {
        //Receiver receiver;
        //IntentFilter filter;
        NfcAdapter mNfcAdapter;
        RestClient client;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppCompatDelegate.CompatVectorFromResourcesEnabled = true;
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
            client = new RestClient();
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
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
            if (intent.Action == NfcAdapter.ActionTagDiscovered)
            {
                string msgx = string.Empty;
                var tag = intent.GetParcelableExtra(NfcAdapter.ExtraTag) as Tag;
                if (tag != null)
                {
                    var TagByteArray = tag.GetId();
                    //string TagString = Encoding.UTF8.GetString(TagByteArray, 0, TagByteArray.Length);
                    string TagString = new SoapHexBinary(TagByteArray).ToString();
                    //Toast.MakeText(this, TagString, ToastLength.Long).Show();
                    client.BaseUrl = new Uri("https://rv.naftosdujos.lt/ws2/");
                    var req = new RestRequest("r_card2/");
                    req.RequestFormat = DataFormat.Json;
                    var response = client.Get(req);
                    
                    if (response.ResponseStatus==ResponseStatus.Error)
                    {
                        Toast.MakeText(this, "Problem with network", ToastLength.Short).Show();
                        return;
                    }
                    
                    if (response.StatusCode==System.Net.HttpStatusCode.OK)
                    {
                        Toast.MakeText(this, "HTTP - OK", ToastLength.Long).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, "ERR", ToastLength.Long).Show();
                    }

                    //var res = response.Content;
                    JsonDeserializer deserial = new JsonDeserializer();
                    var JSONObj = deserial.Deserialize<rcard2>(response);
                    string res = JSONObj.kreditas.ToString();
                    //int rowCount = JSONObj["Count"]; //rowCount will be 234 based on the example {"Count":234} 

                    var alertMessage = new Android.App.AlertDialog.Builder(this).Create();

                    alertMessage.SetMessage(res);
                    alertMessage.Show();

                }
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
            else
            {
                var alert = new Android.Support.V7.App.AlertDialog.Builder(this).Create();
                alert.SetMessage("NFC is not supported on this device.");
                alert.SetTitle("NFC Unavailable");
                alert.Show();
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