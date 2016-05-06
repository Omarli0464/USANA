
using Android.App;
using Android.OS;
using System;
using Android.Support.V4.Widget;
using Android.Net;
using Xamarin.Forms;
using System.Diagnostics;

namespace AndroidInterview
{
    [Activity(Label = "AndroidInterview", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Android.Support.V4.App.FragmentActivity
    {
        TitlesFragment frag;

       SwipeRefreshLayout refresher;
        protected override void OnCreate(Bundle bundle)
        {
         
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);
            frag = FragmentManager.FindFragmentById<TitlesFragment>(Resource.Id.titles_fragment);
            refresher = FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
           refresher.Refresh += HandleRefresh;
        }
        async void HandleRefresh(object sender, EventArgs e)
        {
            ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            bool isOnline = (activeConnection != null) && activeConnection.IsConnected;
            if (isOnline)
            {
                await frag.DownloadHomepageAsync();
                refresher.Refreshing = false;
            }
            else
            {
                AlertClass al = new AlertClass();
                al.alert();
            }
        }
        private class AlertClass: ContentPage
        {
            public AlertClass()
            {

            }
            public void alert()
            {
                DisplayAlert("Alert", "No Internet Connection", "OK");
            }
        }

    }
}


