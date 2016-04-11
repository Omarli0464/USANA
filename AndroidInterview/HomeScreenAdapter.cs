
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
	public class HomeScreenAdapter : BaseAdapter {
		List<FeedItem> items;
		Activity context;
		public HomeScreenAdapter(Activity context, List<FeedItem> items) : base() {
			this.context = context;
			this.items = items;
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override Java.Lang.Object  GetItem(int position) {  
			return null;
			//return items[position]; 
		}
		public FeedItem getFeed(int position){
			return items[position];
		}

		public override int Count {
			get { return items.Count; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Resource.Layout.listitem, null);
			view.FindViewById<TextView>(Resource.Id.textView1).Text = items[position].Title;
			view.FindViewById<TextView>(Resource.Id.textView2).Text = items[position].PubDate.ToString();
			return view;
		}
	}
}

