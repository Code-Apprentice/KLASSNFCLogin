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

namespace KLASSNFCLogin_LoginOnly
{
    [Activity(Label = "LoginConfirm", Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen")]
    public class LoginConfirm : Activity
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
            wc.UploadValuesCompleted += Wc_UploadValsCompleted;

            //Finally Submit the Form:
            wc.UploadValuesAsync(new Uri
            ("https://docs.google.com/forms/d/e/1FAIpQLSdnFTo3k65_7w4cq3sy4qUMu68hMd0rX9g6aZ1Op-HtB75Vqw/formResponse"),
            "POST", keyval, Guid.NewGuid().ToString());
        }

        private async void Wc_UploadValsCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            TextView statusText = FindViewById<TextView>(Resource.Id.statusText);
            if(e.Error == null)
            {
                statusText.Text = "Logged in successfully!";
            }
            else
            {
                statusText.Text = "Error, Try Again." + e.Error.Message;
            }
            await System.Threading.Tasks.Task.Delay(1000);
            this.Finish();
        }

        private void SetLabels()
        {
            // put name and uid in the textviews
           
            TextView nameView = FindViewById<TextView>(Resource.Id.nameView);
            TextView uidView = FindViewById<TextView>(Resource.Id.uidView);

            nameView.Text = String.Format("Name: {0}", (name != null) ? name : "INVALID CARD");
            uidView.Text = String.Format("UID: {0}", tagUid);
        }

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            GetTagData();

            SetContentView(Resource.Layout.LoginConfirmed);

            SetLabels();

            if(name != null)
                SendForm(tagUid, STATUS.LOGIN);
            else
            {
                await System.Threading.Tasks.Task.Delay(1000);
                this.Finish();
            }

        }
    }
}