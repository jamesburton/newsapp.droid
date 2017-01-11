using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using NewsApp.Android.Models;
using System.Linq;
using System.Collections.Generic;

namespace NewsApp.Android
{
    [Activity(Label = "NewsApp.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        ListView newsList;
        IList<Feed> feeds;
        IList<FeedItem> currentItems;
        FeedItem selectedItem;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            newsList = FindViewById<ListView>(Resource.Id.newsList);

            feeds = new List<Feed>(new[] {
                new Feed("http://feeds.bbci.co.uk/news/uk/rss.xml", "BBC", "UK"),
                new Feed("http://feeds.bbci.co.uk/news/technology/rss.xml", "BBC", "Technology"),
                new Feed("http://feeds.reuters.com/reuters/UKdomesticNews?format=xml", "Reuters", "UK"),
                new Feed("http://feeds.reuters.com/reuters/technologyNews?format=xml", "Reuters", "Technology")
            });
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
            e.View.Selected = !e.View.Selected;
            //If the below two lines are added, it highlights the menu in the action bar and does nothing with the ListView
            //e.View.RequestFocusFromTouch();
            //e.View.RequestFocus();

            if(e.View.Selected)
            {
                // Set selected item and show details panel
                selectedItem = currentItems[e.Position];
                // TODO: Show Details Panel
            } else
            {
                // Clear selected item and hide details panel
                selectedItem = null;
                // TODO: Hide Details Panel
            }
        }
    }
}

