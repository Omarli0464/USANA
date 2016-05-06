
using System;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Webkit;
using Android.Net;
using Java.IO;

namespace AndroidInterview
{
	public class DetailsFragment : Fragment
	{
         private FileCache cache;
        //string path = @"Android/data/";
        public static DetailsFragment NewInstance(int playId, string url)
		{
            var detailsFrag = new DetailsFragment {Arguments = new Bundle()};
			detailsFrag.Arguments.PutInt("current_play_id", playId);
			detailsFrag.Arguments.PutString ("url", url);
			return detailsFrag;
		}
		public int ShownPlayId
		{
			get { return Arguments.GetInt("current_play_id", 0); }
		}
		public string ShownUrl
		{
			get { return Arguments.GetString("url", null); }
		}
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
            ConnectivityManager connectivityManager = (ConnectivityManager)this.Activity.GetSystemService(Context.ConnectivityService);
            NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            bool isOnline = (activeConnection != null) && activeConnection.IsConnected;
          
             cache = new FileCache(this.Activity);
           
            var web = new WebView(Activity);
            if (container == null)
			{
				// Currently in a layout without a container, so no reason to create our view.
				return null;
			}
           
            if (isOnline)
            {
                web.SetWebViewClient(new WebViewClient());
                web.Settings.JavaScriptEnabled = true;
                web.LoadUrl(ShownUrl);
            }
            else
            {
              File fl = cache.GetFile(ShownUrl);
               BufferedReader br = new BufferedReader(new FileReader(fl));
                StringBuilder sb = new StringBuilder();
                while (br.ReadLine() != null) { sb.Append(br.ReadLine()); }
                String html_value = sb.ToString();
                
                web.SetWebViewClient(new WebViewClient());
                web.Settings.JavaScriptEnabled = true;
                web.LoadData(html_value, "text/html", "UTF_8");
            }
            return web;
        }


	}
}

