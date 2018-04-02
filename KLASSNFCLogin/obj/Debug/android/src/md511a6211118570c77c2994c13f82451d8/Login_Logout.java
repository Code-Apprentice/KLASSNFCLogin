package md511a6211118570c77c2994c13f82451d8;


public class Login_Logout
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("KLASSNFCLogin.Login_Logout, KLASSNFCLogin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Login_Logout.class, __md_methods);
	}


	public Login_Logout ()
	{
		super ();
		if (getClass () == Login_Logout.class)
			mono.android.TypeManager.Activate ("KLASSNFCLogin.Login_Logout, KLASSNFCLogin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
