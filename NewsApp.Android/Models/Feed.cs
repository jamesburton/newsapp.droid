using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsApp.Android.Models
{
    public class Feed
    {
        public string Source { get; set; }
        public string Category { get; set; }
        public string Url { get; set; }
        public IList<FeedItem> FeedItems { get; set; }

        public Feed(string url, string source, string category, bool loadItems = true)
        {
            Url = url;
            Source = source;
            Category = category;
            if (loadItems)
                LoadItems();
        }

        public void LoadItems()
        {
            try
            {
                var feed = TNX.RssReader.RssHelper.ReadFeed(Url);
                FeedItems = feed.Items.Select(feedItem => new FeedItem { Title = feedItem.Title, Description = feedItem.Description, Author = feedItem.Author, Link = feedItem.Link, Published = feedItem.PublicationUtcTime }).ToList();
#if DEBUG
                System.Diagnostics.Debug.WriteLine("Loaded " + FeedItems.Count + " items from " + Source + "/" + Category + " (" + Url + ")");
#endif
            } catch(Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine("Error loading feed from " + Source + "/" + Category + " (" + Url + "): " + ex.Message);
#endif
            }
        }
    }
}