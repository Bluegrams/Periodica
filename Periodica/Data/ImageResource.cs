using System;

namespace Bluegrams.Periodica.Data
{
    public class ImageResource
    {
        public Uri Uri { get; private set; }

        public string Tag { get; set; }

        public Uri SourceUrl { get; set; }

        public string Description { get; set; }

        public ImageResource(string uri, string tag, string sourceUrl)
        {
            this.Uri = new Uri("ms-appx:///ImgElements/" + uri);
            this.Tag = tag;
            this.SourceUrl = sourceUrl == null ? null : new Uri(sourceUrl);
        }
    }
}
