using System;
using System.Linq;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Android.Support.V4.Widget;
using Android.Util;

namespace AndroidInterview
{
	[Activity (Label = "AndroidInterview", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		TitlesFragment frag;
		SwipeRefreshLayout refresher;
			protected override void  OnCreate(Bundle bundle)
			{
			base.OnCreate(bundle);
			SetContentView (Resource.Layout.activity_main);
			frag=FragmentManager.FindFragmentById<TitlesFragment>(Resource.Id.titles_fragment);


			/*frag= PostListFragment.Instantiate ("android", "Android");
			SupportFragmentManager.BeginTransaction ()
				.Add (Resource.Id.container, forum, "post-list")
				.Commit ();*/
			refresher = FindViewById<SwipeRefreshLayout> (Resource.Id.refresher);
			refresher.SetColorScheme (Resource.Color.primary_material_dark,
				Resource.Color.material_blue_grey_950,
				Resource.Color.ripple_material_light,
				Resource.Color.abc_secondary_text_material_light);
			refresher.Refresh += HandleRefresh;
			//var titleList = FragmentManager.FindFragmentById(Resource.Id.titles_fragment);
			//DownloadHomepageAsync();

			Console.WriteLine ("Done");

		}
		async void HandleRefresh (object sender, EventArgs e)
		{
			await frag.DownloadHomepageAsync();
			refresher.Refreshing = false;
		}


	}
}


