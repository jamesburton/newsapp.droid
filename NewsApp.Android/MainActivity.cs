using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using NewsApp.Android.Models;
using System.Linq;

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
            var allItems = feeds.SelectMany(feed => feed.FeedItems);
            var itemArray = allItems.ToArray();
            //var itemString = itemArray.Select(item => item.Title).ToArray();
            var itemStrings = new[] { "Example entry", "Another entry", "Third item" };

            //newsList.Adapter = new ArrayAdapter(this, Resource.Layout.TextViewItem, itemStrings);
            newsList.Adapter = new ArrayAdapter(this, Resource.Layout.TextViewItem, itemStrings);

            var statusText = FindViewById<TextView>(Resource.Id.statusText);
            statusText.Text = "Showing " + itemArray.Count() + " items";

            newsList.ItemClick += newsList_ItemClick;
        }

        void newsList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            e.View.Selected = true;
            //If the below two lines are added, it highlights the menu in the action bar and does nothing with the ListView
            //e.View.RequestFocusFromTouch();
            //e.View.RequestFocus();
        }
    }
}

