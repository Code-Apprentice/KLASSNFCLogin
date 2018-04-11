package md5476ef68c93dbccfb81a2936123deabd8;


public class MainActivity_VideoLoop
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.media.MediaPlayer.OnPreparedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPrepared:(Landroid/media/MediaPlayer;)V:GetOnPrepared_Landroid_media_MediaPlayer_Handler:Android.Media.MediaPlayer/IOnPreparedListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("KLASSNFCLogin_LogoutOnly.MainActivity+VideoLoop, KLASSNFCLogin_LogoutOnly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainActivity_VideoLoop.class, __md_methods);
	}


	public MainActivity_VideoLoop ()
	{
		super ();
		if (getClass () == MainActivity_VideoLoop.class)
			mono.android.TypeManager.Activate ("KLASSNFCLogin_LogoutOnly.MainActivity+VideoLoop, KLASSNFCLogin_LogoutOnly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onPrepared (android.media.MediaPlayer p0)
	{
		n_onPrepared (p0);
	}

	private native void n_onPrepared (android.media.MediaPlayer p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
