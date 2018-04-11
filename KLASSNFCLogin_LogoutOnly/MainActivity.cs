using System;
using System.IO;
using System.Collections.Generic;

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

namespace KLASSNFCLogin_LogoutOnly
{
    [Activity(Label = "Tap Here to Logout", MainLauncher = true)]
    public class MainActivity : Activity
    {
        public List<Student> Students = null;
        public static Dictionary<string, string> StudentDict { get; set; } // UID : Name

        public class VideoLoop : Java.Lang.Object, Android.Media.MediaPlayer.IOnPreparedListener
        {
            public void OnPrepared(MediaPlayer mp)
            {
                mp.Looping = true;
            }
        }

        private void PopulateStudentListDict()
        {
            StudentDict = new Dictionary<string, string>();
            Students = JsonConvert.DeserializeObject<List<Student>>(new StreamReader(Assets.Open("StudentDatabase/students.json")).ReadToEnd());
            foreach (Student s in Students)
            {
                StudentDict.Add(s.UID, s.Name);
            }
        }

        private void StartNFCGif()
        {
            VideoView anim = FindViewById<VideoView>(Resource.Id.videoView1);
            anim.SetOnPreparedListener(new VideoLoop());
            String uriPath = "android.resource://" + PackageName + "/" + Resource.Drawable.nfctaphere;
            Android.Net.Uri uri = Android.Net.Uri.Parse(uriPath);
            anim.SetVideoURI(uri);
            anim.Start();
        }

        private void StartListeningForNFC()
        {
            IntentFilter tagDetected = new IntentFilter(NfcAdapter.ActionTagDiscovered);
            var filters = new[] { tagDetected };
            var pendingIntent = PendingIntent.GetActivity(this,
                                                          0,
                                                          new Intent(this, typeof(LogoutConfirm)), // start logout activity
                                                          0);

            NfcAdapter.GetDefaultAdapter(this).EnableForegroundDispatch(this, pendingIntent, filters, null);
        }

        protected override void OnResume()
        {
            base.OnResume();

            StartNFCGif();
            StartListeningForNFC();
        }

        protected override void OnPause()
        {
            base.OnPause();

            NfcAdapter.GetDefaultAdapter(this).DisableForegroundDispatch(this);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            PopulateStudentListDict();
        }
    }
}

