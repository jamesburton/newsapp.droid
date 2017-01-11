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
        TextView statusText;
        CheckBox bbcCheck, reutersCheck, ukCheck, technologyCheck;

        LinearLayout itemDetails;
        TextView title, link, author, published, description;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            newsList = FindViewById<ListView>(Resource.Id.newsList);
            //statusText = FindViewById<TextView>(Resource.Id.statusText);

            bbcCheck = FindViewById<CheckBox>(Resource.Id.bbcCheck);
            reutersCheck = FindViewById<CheckBox>(Resource.Id.reutersCheck);
            ukCheck = FindViewById<CheckBox>(Resource.Id.ukCheck);
            technologyCheck = FindViewById<CheckBox>(Resource.Id.technologyCheck);

            itemDetails = FindViewById<LinearLayout>(Resource.Id.itemDetails);
            title = itemDetails.FindViewById<TextView>(Resource.Id.title);
            author = itemDetails.FindViewById<TextView>(Resource.Id.author);
            published = itemDetails.FindViewById<TextView>(Resource.Id.published);
            link = itemDetails.FindViewById<TextView>(Resource.Id.link);
            description = itemDetails.FindViewById<TextView>(Resource.Id.description);

            feeds = new List<Feed>(new[] {
                new Feed("http://feeds.bbci.co.uk/news/uk/rss.xml", "BBC", "UK"),
                new Feed("http://feeds.bbci.co.uk/news/technology/rss.xml", "BBC", "Technology"),
                new Feed("http://feeds.reuters.com/reuters/UKdomesticNews?format=xml", "Reuters", "UK"),
                new Feed("http://feeds.reuters.com/reuters/technologyNews?format=xml", "Reuters", "Technology")
            });
            updateItems();

            newsList.ItemClick += newsList_ItemClick;
        }

        void updateItems()
        {
            var sources = new List<string>();
            var categories = new List<string>();
            if (bbcCheck.Checked) sources.Add("BBC");
            if (reutersCheck.Checked) sources.Add("Reuters");
            if (ukCheck.Checked) categories.Add("UK");
            if (technologyCheck.Checked) categories.Add("Technology");

            //var allItems = feeds.SelectMany(feed => feed.FeedItems);
            var allItems = feeds
                .Where(feed => sources.Contains(feed.Source))
                .Where(feed => categories.Contains(feed.Category))
                .SelectMany(feed => feed.FeedItems);
            var itemArray = allItems.ToArray();
            currentItems = itemArray;
            var itemStrings = itemArray.Select(item => item.Title).ToArray();

            newsList.Adapter = new ArrayAdapter(this, Resource.Layout.TextViewItem, itemStrings);

            //statusText.Text = "Showing " + itemArray.Count() + " items";
        }

        void setItemDetails()
        {

        }

        void newsList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("newsList_ItemClick, e.Position=", e.Position, ", e.Selected=", e.View.Selected, ", e.Id=", e.Id);
#endif
            e.View.Selected = !e.View.Selected;
            //If the below two lines are added, it highlights the menu in the action bar and does nothing with the ListView
            //e.View.RequestFocusFromTouch();
            //e.View.RequestFocus();

            if(e.View.Selected)
            {
                // Set selected item and show details panel
                selectedItem = currentItems[e.Position];
                itemDetails.Visibility = ViewStates.Visible;
                setItemDetails();
#if DEBUG
                System.Diagnostics.Debug.WriteLine("newsList_ItemClick showing entry, e.Position=", e.Position, ", e.Selected=", e.View.Selected, ", e.Id=", e.Id);
#endif
            }
            else
            {
                // Clear selected item and hide details panel
                selectedItem = null;
                itemDetails.Visibility = ViewStates.Gone;
                setItemDetails();
#if DEBUG
                System.Diagnostics.Debug.WriteLine("newsList_ItemClick hiding entry, e.Position=", e.Position, ", e.Selected=", e.View.Selected, ", e.Id=", e.Id);
#endif
            }
        }
    }
}

