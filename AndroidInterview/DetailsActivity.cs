
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

namespace AndroidInterview
{
	[Activity (Label = "DetailsActivity")]			
	public class DetailsActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			var index = Intent.Extras.GetInt("current_play_id", 0);
			var link = Intent.Extras.GetString ("url", null);
			var details = DetailsFragment.NewInstance(index,link); // DetailsFragment.NewInstance is a factory method to create a Details Fragment
			var fragmentTransaction = FragmentManager.BeginTransaction();
			fragmentTransaction.Add(Android.Resource.Id.Content, details);
			fragmentTransaction.Commit();
		}
	}
}

