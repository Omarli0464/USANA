
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
		List<string> al = new List<string>();
		List<string> bl = new List<string>();

		public override async void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);
			await DownloadHomepageAsync ();
			var adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItemChecked,al);
			ListAdapter = adapter;
			adapter.NotifyDataSetChanged ();
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
		public async Task DownloadHomepageAsync()
		{
			//List <FeedItem> feedItemsList  = new List<FeedItem>();
			//List <String> al = new List<string> ();
			var httpClient = new HttpClient(); // Xamarin supports HttpClient!

			Task<string> contentsTask = httpClient.GetStringAsync("http://feeds.feedburner.com/androidcentral?format=xml"); // async method!


			// await! control returns to the caller and the task continues to run on another thread
			string contents = await contentsTask;
			Console.WriteLine ("finish fetch contents");
			//int exampleInt = contents.Length;
			XDocument document =XDocument.Parse(contents);


			foreach (XElement element in document.Descendants("item").Descendants("title"))
			{
				//Console.WriteLine (element.Value);
				/*FeedItem item= new FeedItem();
				foreach (XElement c in element.Descendants("title")) {
					item.Title = c.Value;
				}*/
				al.Add(element.Value);
				//Console.WriteLine (element.Value);

				//al.Add(item);
			}foreach (XElement element in document.Descendants("item").Descendants("ti"))
			{
				
				bl.Add(element.Value);
			
			}






			//return exampleInt;


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

		//background task download and parse the feed
		/**
		 * public async Task<int> DownloadHomepageAsync()
		{
			

			var httpClient = new HttpClient(); // Xamarin supports HttpClient!

			Task<string> contentsTask = httpClient.GetStringAsync("http://feeds.feedburner.com/androidcentral?format=xml"); // async method!


			// await! control returns to the caller and the task continues to run on another thread
			string contents = await contentsTask;
			int exampleInt = contents.Length;
			XDocument document =XDocument.Parse(contents);
			try{
				FeedItem item=null;
				foreach (XElement element in document.Descendants("item").Descendants("title"))
				{


					Console.WriteLine (element);
					if(element.Descendants("title")!=null){
						Console.WriteLine ("statge1", element.Descendants("title").ToString());
						item.Title=element.Descendants("title").ToString();
					}
					if(element.Descendants("link")!=null){
						item.Link=element.Descendants("link").ToString();
					}
					if(element.Descendants("pubDate")!=null){
						item.PubDate=Convert.ToDateTime(element.Descendants("pubDate").ToString());
					}
				}
				feedItemsList.Add(item);
			}
			catch(Exception){
				Console.WriteLine ("throwed");
				throw;
			}



			return exampleInt;


		}
*/
	}
}

