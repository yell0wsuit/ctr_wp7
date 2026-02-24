using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;

using ctr_wp7.utils;

namespace ctr_wp7.remotedata.cartoons
{
    // Token: 0x0200002A RID: 42
    public class ImageDownloader
    {
        // Token: 0x060001A3 RID: 419 RVA: 0x0000BD00 File Offset: 0x00009F00
        public static void setListener(ImageDownloadedListener LImageDownloaded)
        {
            lImageDownloaded = LImageDownloaded;
        }

        // Token: 0x060001A4 RID: 420 RVA: 0x0000BDA8 File Offset: 0x00009FA8
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
                    ImageDataSaxHandler imageDataSaxHandler = new ImageDataSaxHandler(block);
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

        // Token: 0x060001A5 RID: 421 RVA: 0x0000BDFB File Offset: 0x00009FFB
        public static bool isBusy()
        {
            return busy;
        }

        // Token: 0x040007CD RID: 1997
        protected const string serverUrl = "http://vps.zeptolab.com/feeder/images?ids=";

        // Token: 0x040007CE RID: 1998
        protected static volatile bool busy = false;

        // Token: 0x040007CF RID: 1999
        protected static ImageDownloadedListener lImageDownloaded;

        // Token: 0x0200002B RID: 43
        protected class ImageDataSaxHandler(Block pBlock) : DefaultHandler
        {

            // Token: 0x060001A9 RID: 425 RVA: 0x0000BE28 File Offset: 0x0000A028
            public override void StartElement(string URI, string localName, string qName, Dictionary<string, string> atts)
            {
                if (localName == "image")
                {
                    string text = null;
                    _ = atts.TryGetValue("data", out text);
                    lImageDownloaded.imageDownloaded(text, block);
                }
            }

            // Token: 0x060001AA RID: 426 RVA: 0x0000BE64 File Offset: 0x0000A064
            public override void EndElement(string URI, string localName, string qName)
            {
            }

            // Token: 0x060001AB RID: 427 RVA: 0x0000BE66 File Offset: 0x0000A066
            public override void Characters(string characters)
            {
            }

            // Token: 0x040007D0 RID: 2000
            private Block block = pBlock;
        }

        // Token: 0x0200002C RID: 44
        public interface ImageDownloadedListener
        {
            // Token: 0x060001AC RID: 428
            void imageDownloaded(string data, Block block);
        }
    }
}
