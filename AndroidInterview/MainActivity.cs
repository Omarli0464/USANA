using Android.App;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using System;
using Android.Views;
using System.Linq;
using System.Collections;


using System.Threading.Tasks;
using System.Net.Http;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;

namespace AndroidInterview
{
	[Activity (Label = "AndroidInterview", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		
			protected override void  OnCreate(Bundle bundle)
			{
			base.OnCreate(bundle);
			SetContentView (Resource.Layout.activity_main);
			//var titleList = FragmentManager.FindFragmentById(Resource.Id.titles_fragment);
			//DownloadHomepageAsync();
			Console.WriteLine ("Done");

		}



		  




			



	}
}


