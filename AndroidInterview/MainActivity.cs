using Android.App;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using System;
using Android.Views;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

namespace AndroidInterview
{
	[Activity (Label = "AndroidInterview", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		
			//string[] items;
			protected override void OnCreate(Bundle bundle)
			{
				base.OnCreate(bundle);
			    SetContentView (Resource.Layout.activity_main);


			}


		public async Task<int> DownloadHomepageAsync()
		{
			
			var httpClient = new HttpClient(); // Xamarin supports HttpClient!

			Task<string> contentsTask = httpClient.GetStringAsync("http://feeds.feedburner.com/androidcentral?format=xml"); // async method!


			// await! control returns to the caller and the task continues to run on another thread
			string contents = await contentsTask;
			int exampleInt = contents.Length;
			XDocument document =XDocument.Parse(contents);
			/*foreach (XElement element in document.Descendants("item").Descendants("title"))
			{
				Console.WriteLine(element);
			}*/
			foreach (XElement element in document.Descendants("item").Descendants("pubDate"))
			{
				Console.WriteLine(element);
			}


			return exampleInt;
		

		}



			



	}
}


