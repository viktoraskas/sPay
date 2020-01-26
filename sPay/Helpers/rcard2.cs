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

namespace sPay.Helpers
{
    public class rcard2
    {
            public string status { get; set; }
            public string description { get; set; }
            public double kreditas { get; set; }
            public string partner { get; set; }
            public string im_kodas { get; set; }
            public string pvm_kodas { get; set; }
            public string pavad { get; set; }
            public string adresas { get; set; }
            public List<object> list { get; set; }
            public List<object> list2 { get; set; }
    }
}