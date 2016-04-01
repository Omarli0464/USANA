using Android.App;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using System;
using Android.Views;
namespace AndroidInterview
{
	[Activity (Label = "AndroidInterview", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : ListActivity
	{
		
			string[] items;
			protected override void OnCreate(Bundle bundle)
			{
				base.OnCreate(bundle);
			     ListView.FastScrollEnabled = true;
				items = new string[] { "news1","news2","news3","news4"};
				ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);
			}
			


		protected override void OnListItemClick(ListView l, View v, int position, long id){
		var t = items[position];
		Android.Widget.Toast.MakeText(this, t, Android.Widget.ToastLength.Short).Show();
	    Console.WriteLine("Clicked on " + t);
	}
	}
}


