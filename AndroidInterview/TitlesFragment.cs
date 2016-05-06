
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.IO;
using Android.Net;
using Java.Lang;
using Java.Util;

namespace AndroidInterview
{
	public class TitlesFragment : ListFragment
	{
		
		private int _currentPlayId = 0;
		private bool _isDualPane = false;
        private FileCache cache;
        List <FeedItem> al;
        string[] values = new[] { "loading", "loading", "loading", "loading", "loading", "loading", "loading", "loading" };
        public override async void OnActivityCreated(Bundle savedInstanceState)
		{
            base.OnActivityCreated(savedInstanceState);
            ConnectivityManager connectivityManager = (ConnectivityManager)this.Activity.GetSystemService(Context.ConnectivityService);
            NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            bool isOnline = (activeConnection != null) && activeConnection.IsConnected;
            cache = new FileCache(this.Activity);
          


            if (!isOnline)
            {


                var adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItemChecked, values.ToArray());
                ListAdapter = adapter;
            }
            else
            {
                al = await DownloadHomepageAsync();
                var adapter = new HomeScreenAdapter(this.Activity, al);
                ListAdapter = adapter;
                adapter.NotifyDataSetChanged();
            }

                if (savedInstanceState != null)
                {
                    _currentPlayId = savedInstanceState.GetInt("current_play_id", 0);
                }
                var detailsFrame = Activity.FindViewById<View>(Resource.Id.details);
                _isDualPane = detailsFrame != null && detailsFrame.Visibility == ViewStates.Visible;

                if (_isDualPane)
                {
                    ListView.ChoiceMode = ChoiceMode.Single;
                    ShowDetails(_currentPlayId, al[0]);
                }
            }
		
		public async Task<List <FeedItem> > DownloadHomepageAsync ()
		{
			List <FeedItem> feedItemsList  = new List<FeedItem>();
			var httpClient = new HttpClient (); // Xamarin supports HttpClient!

			Task<string> contentsTask = httpClient.GetStringAsync ("http://feeds.feedburner.com/androidcentral?format=xml"); // async method!


			// await! control returns to the caller and the task continues to run on another thread
			string contents = await contentsTask;
			XDocument document = XDocument.Parse (contents);

            foreach (XElement element in document.Descendants("item")) {
				FeedItem item = new FeedItem ();
				item.Title = element.Descendants ("title").First ().Value;
				item.Link = element.Descendants ("link").First ().Value;
				item.PubDate = Convert.ToDateTime (element.Descendants ("pubDate").First ().Value);
                Task<string> contentsTask2 = httpClient.GetStringAsync(item.Link);
                feedItemsList.Add (item);
             File fl = cache.GetFile(item.Link);
                string contents2 = await contentsTask2;
                //sw.Write(contents2);
               
                Writer w = new BufferedWriter(new FileWriter(fl));
                w.Write(contents2);
                w.Close();
            

            }
      


            return feedItemsList;
		}



    
      
        



		public override void OnListItemClick(ListView l, View v, int position, long id)
		{
			FeedItem a = al [position];
			ShowDetails(position,a);
		}
		private void ShowDetails(int playId,FeedItem feed)
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
					details = DetailsFragment.NewInstance(playId,feed.Link);
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
				intent.PutExtra ("current_play_id", playId);
				intent.PutExtra ("url",feed.Link);
				StartActivity(intent);
			}
		}

		
	}
}

