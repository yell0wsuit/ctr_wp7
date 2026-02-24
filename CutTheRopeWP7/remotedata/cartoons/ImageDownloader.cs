using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;

using ctr_wp7.utils;

namespace ctr_wp7.remotedata.cartoons
{
    public class ImageDownloader
    {
        public static void setListener(ImageDownloadedListener LImageDownloaded)
        {
            lImageDownloaded = LImageDownloaded;
        }

        public static void download(Block block)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri("http://vps.zeptolab.com/feeder/images?ids=" + block.image_id));
            _ = httpWebRequest.BeginGetResponse(delegate (IAsyncResult r)
            {
                try
                {
                    busy = true;
                    HttpWebRequest httpWebRequest2 = (HttpWebRequest)r.AsyncState;
                    HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest2.EndGetResponse(r);
                    ImageDataSaxHandler imageDataSaxHandler = new(block);
                    using (imageDataSaxHandler.xmlReader = XmlReader.Create(httpWebResponse.GetResponseStream()))
                    {
                        imageDataSaxHandler.Parse();
                    }
                    busy = false;
                }
                catch (Exception)
                {
                    busy = false;
                }
            }, httpWebRequest);
        }

        public static bool isBusy()
        {
            return busy;
        }

        protected const string serverUrl = "http://vps.zeptolab.com/feeder/images?ids=";

        protected static volatile bool busy;

        protected static ImageDownloadedListener lImageDownloaded;

        protected class ImageDataSaxHandler(Block pBlock) : DefaultHandler
        {

            public override void StartElement(string URI, string localName, string qName, Dictionary<string, string> atts)
            {
                if (localName == "image")
                {
                    _ = atts.TryGetValue("data", out string text);
                    lImageDownloaded.imageDownloaded(text, block);
                }
            }

            public override void EndElement(string URI, string localName, string qName)
            {
            }

            public override void Characters(string characters)
            {
            }

            private readonly Block block = pBlock;
        }

        public interface ImageDownloadedListener
        {
            void imageDownloaded(string data, Block block);
        }
    }
}
