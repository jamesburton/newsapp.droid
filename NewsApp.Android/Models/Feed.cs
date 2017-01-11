using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                try
                {
                    var success = SaveJson().Result;
                } catch(Exception saveException)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine("Error saving feed from " + Source + "/" + Category + " (" + Url + "): " + saveException.Message);
#endif
                }
            } catch(Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine("Error loading feed from " + Source + "/" + Category + " (" + Url + "): " + ex.Message);
#endif
                var loaded = LoadJson(JsonName).Result;
                if(loaded != null)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine("Loaded feed json for " + Source + "/" + Category + " (" + Url + ")");
#endif
                    FeedItems = loaded.FeedItems;
                }
            }
        }

        public static string GetJsonName(string source, string category) => source + "-" + category;
        public string JsonName { get { return GetJsonName(Source, Category); } }

        public async Task<bool> SaveJson()
        {
            return await this.SaveJson(JsonName);
        }

        public static async Task<Feed> LoadJson(string source, string category)
            => await LoadJson(GetJsonName(source, category));

        public static async Task<Feed> LoadJson(string jsonName)
            => await jsonName.LoadJson<Feed>();
    }
}