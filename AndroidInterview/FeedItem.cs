
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
	public class FeedItem :Java.Lang.Object
	{
		public FeedItem()
		{
		}

		public String Title { get; set; }
		public String Link { get; set; }
		public DateTime PubDate { get; set; }

	}
}

