using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SkiaSharp;

namespace sPay
{
    [Activity(Label = "ChekCredit")]
    public class CheckCredit : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CheckCreditLay);
            ImageView imageView1 = FindViewById<ImageView>(Resource.Id.imageView1);
            ImageView imageView2 = FindViewById<ImageView>(Resource.Id.imageView2);
            imageView1.SetImageResource(Resource.Drawable.credit_card_outline);
            // Create your application here

        }
    }
}