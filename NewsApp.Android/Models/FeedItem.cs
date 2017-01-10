using System;

namespace NewsApp.Android.Models
{
    public class FeedItem
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime Published { get; set; }
        public string Author { get; set; }
    }
}