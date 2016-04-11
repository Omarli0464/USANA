
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml;
using System.Xml.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.IO;

namespace AndroidInterview
{
	public class TitlesFragment : ListFragment
	{
		//string[] values = new[] { "loading", "loading", "loading","loading","loading", "loading", "loading","loading"};

		public int _currentPlayId = 0;
		public bool _isDualPane = false;
		//List <FeedItem> feedItemsList  = new List<FeedItem>();
		List<string> values = new List<string>();
		//List<string> bl = new List<string>();

		public override async void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);
			List <FeedItem> al= await DownloadHomepageAsync ();
			/*foreach (FeedItem cao in al) {
				values.Add (cao.Title);
			}*/
			//var adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItemChecked,values.ToArray());
			var adapter = new HomeScreenAdapter(this.Activity,al);
			ListAdapter = adapter;
			//adapter.NotifyDataSetChanged ();
			if (savedInstanceState != null)
			{
				_currentPlayId = savedInstanceState.GetInt("current_play_id", 0);
			}
			var detailsFrame = Activity.FindViewById<View>(Resource.Id.details);
			_isDualPane = detailsFrame != null && detailsFrame.Visibility == ViewStates.Visible;
			if (_isDualPane)
			{
				ListView.ChoiceMode =  ChoiceMode.Single;
				ShowDetails(_currentPlayId);
			}
		}
		public async Task<List <FeedItem> > DownloadHomepageAsync ()
		{
			List <FeedItem> feedItemsList  = new List<FeedItem>();
			//List <String> al = new List<string> ();
			var httpClient = new HttpClient (); // Xamarin supports HttpClient!

			Task<string> contentsTask = httpClient.GetStringAsync ("http://feeds.feedburner.com/androidcentral?format=xml"); // async method!


			// await! control returns to the caller and the task continues to run on another thread
			string contents = await contentsTask;
			Console.WriteLine ("finish fetch contents");
			//int exampleInt = contents.Length;
			XDocument document = XDocument.Parse (contents);


			foreach (XElement element in document.Descendants("item")) {
				FeedItem item = new FeedItem ();
				item.Title = element.Descendants ("title").First ().Value;
				item.Link = element.Descendants ("link").First ().Value;
				item.PubDate = Convert.ToDateTime (element.Descendants ("pubDate").First ().Value);
				feedItemsList.Add (item);

			}

			return feedItemsList;
		}

	




		public override void OnListItemClick(ListView l, View v, int position, long id)
		{

			ShowDetails(position);
		}
		private void ShowDetails(int playId)
		{
			_currentPlayId = playId;
			if (_isDualPane)
			{
				// We can display everything in place with fragments.
				// Have the list highlight this item and show the data.
				ListView.SetItemChecked(playId, true);
				// Check what fragment is shown, replace if needed.
				var details = FragmentManager.FindFragmentById(Resource.Id.details) as DetailsFragment;
				if (details == null || details.ShownPlayId != playId)
				{
					// Make new fragment to show this selection.
					details = DetailsFragment.NewInstance(playId);
					// Execute a transaction, replacing any existing
					// fragment with this one inside the frame.
					var ft = FragmentManager.BeginTransaction();
					ft.Replace(Resource.Id.details, details);
					ft.SetTransition(FragmentTransit.FragmentFade);
					ft.Commit();
				}
			}
			else
			{
				// Otherwise we need to launch a new Activity to display
				// the dialog fragment with selected text.
				var intent = new Intent();
				intent.SetClass(Activity, typeof (DetailsActivity));
				intent.PutExtra("current_play_id", playId);
				StartActivity(intent);
			}
		}

		
	}
}

