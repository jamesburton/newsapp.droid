using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using NewsApp.Android.Models;

namespace NewsApp.Android
{
    [Activity(Label = "NewsApp.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        ListView newsList;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            //Button button = FindViewById<Button>(Resource.Id.MyButton);

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            newsList = FindViewById<ListView>(Resource.Id.newsList);

            var feeds = new[] {
                new Feed("http://feeds.bbci.co.uk/news/uk/rss.xml", "BBC", "UK"),
                new Feed("http://feeds.bbci.co.uk/news/technology/rss.xml", "BBC", "Technology"),
                new Feed("http://feeds.reuters.com/reuters/UKdomesticNews?format=xml", "Reuters", "UK"),
                new Feed("http://feeds.reuters.com/reuters/technologyNews?format=xml", "Reuters", "Technology")
            };
        }
    }
}

