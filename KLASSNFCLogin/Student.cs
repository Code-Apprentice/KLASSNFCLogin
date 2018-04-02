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

using Newtonsoft.Json;

namespace KLASSNFCLogin
{
    public class Student
    {
        public string Name { get; set; }

        public string UID { get; set; }
    }
}