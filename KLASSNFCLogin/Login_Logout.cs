using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;

using Android.App;
using Android.Content;
using Android.Nfc;
using Android.Nfc.Tech;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Media;

using Newtonsoft.Json;

namespace KLASSNFCLogin
{
    [Activity(Label = "Login_Logout")]
    public class Login_Logout : Activity
    {
        public Tag tag = null;
        public string tagUid = null;
        public string name = null;

        private static string ByteArrayToString(byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private void GetTagData()
        {
            Tag tag = this.Intent.GetParcelableExtra(NfcAdapter.ExtraTag) as Tag;
            if (tag == null)
            {
                Toast.MakeText(this, "Invalid NFC Tag!", ToastLength.Short).Show();
                this.Finish();
            }

            // id is reversed
            byte[] tagId = tag.GetId();
            Array.Reverse(tagId);
            tagUid = ByteArrayToString(tagId);

            try
            {
                name = MainActivity.StudentDict[tagUid];
            }
            catch (Exception e)
            {
                Toast.MakeText(this, "Invalid card!", ToastLength.Short).Show();
            }
        }

        enum STATUS { LOGIN, LOGOUT }

        private void SendForm(string uid, STATUS status)
        {
            WebClient wc = new WebClient();
            var keyval = new NameValueCollection();
            keyval.Add("entry.327165977", tagUid);
            keyval.Add("entry.712576828", (status == STATUS.LOGIN)?"On Campus":"Off Campus");

            keyval.Add("submit", "Submit");

            wc.Headers.Add("Origin", "https://docs.google.com");
            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit / 534.10(KHTML, like Gecko) Chrome / 8.0.552.224 Safari / 534.10");

            //Finally Submit the Form:
            wc.UploadValuesAsync(new Uri
            ("https://docs.google.com/forms/d/e/1FAIpQLSdnFTo3k65_7w4cq3sy4qUMu68hMd0rX9g6aZ1Op-HtB75Vqw/formResponse"),
            "POST", keyval, Guid.NewGuid().ToString());

            this.Finish();
        }

        private void SetButtonsAndForm()
        {
            // put name and uid in the textviews
           
            TextView nameView = FindViewById<TextView>(Resource.Id.nameView);
            TextView uidView = FindViewById<TextView>(Resource.Id.uidView);

            nameView.Text = String.Format("Name: {0}", name);
            uidView.Text = String.Format("UID: {0}", tagUid);

            // set buttons

            Button loginButton = FindViewById<Button>(Resource.Id.loginButton);
            Button logoutButton = FindViewById<Button>(Resource.Id.logoutButton);

            loginButton.Click += delegate { SendForm(tagUid, STATUS.LOGIN); };
            logoutButton.Click += delegate { SendForm(tagUid, STATUS.LOGOUT); };
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            GetTagData();

            SetContentView(Resource.Layout.Loginlogout);

            SetButtonsAndForm();
        }
    }
}