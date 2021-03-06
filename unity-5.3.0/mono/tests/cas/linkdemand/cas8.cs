using System;
using System.Security;
using System.Security.Permissions;

[assembly: SecurityPermission (SecurityAction.RequestRefuse, ControlPrincipal=true)]

public delegate int MyReturnCode (int rc);

public class Program {

	[SecurityPermission (SecurityAction.LinkDemand, ControlPrincipal=true)]
	public virtual int InstanceTest (int rc)
	{
		Console.WriteLine ("*1* Instance delegate call expected to fail!");
		return rc;
	}

	static int Test ()
	{
		Program p = new Program ();
		MyReturnCode rc = new MyReturnCode (p.InstanceTest);
		return rc (1);
	}

	static int Main ()
	{
		try {
			return Test ();
		}
		catch (SecurityException se) {
			Console.WriteLine ("*0* Expected SecurityException\n{0}", se);
			return 0;
		}
		catch (Exception e) {
			Console.WriteLine ("*2* Unexpected Exception\n{0}", e);
			return 2;
		}
	}
}
