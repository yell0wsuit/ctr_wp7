using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml;

using ctr_wp7.game;
using ctr_wp7.utils;

namespace ctr_wp7.remotedata.cartoons
{
    public class VideoDataManager : ServerDataManager, ImageDownloader.ImageDownloadedListener
    {
        public VideoDataManager()
        {
            ImageDownloader.setListener(this);
            protocolVersion = 2;
            serverUrl = "http://vps.zeptolab.com/feeder/episodes?";
            object obj = readObject("BlockConfig", typeof(BlockConfig));
            if (obj == null)
            {
                blockConfig = new BlockConfig();
                return;
            }
            blockConfig = (BlockConfig)obj;
        }

        public void initWith(string app, int resolution)
        {
            this.resolution = resolution;
            request(app);
        }

        public void request(string app)
        {
            if (!success && !execution && !ImageDownloader.isBusy())
            {
                execution = true;
                LinkBuilder linkBuilder = new(serverUrl);
                linkBuilder.put("app", app);
                linkBuilder.put("platform", "winphone");
                if (blockConfig.hash != null)
                {
                    linkBuilder.put("hash", blockConfig.hash);
                }
                injectSizes(linkBuilder, resolution);
                injectAdditionalParameters(linkBuilder);
                string text = linkBuilder.ToString();
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(text));
                _ = httpWebRequest.BeginGetResponse(delegate (IAsyncResult r)
                {
                    try
                    {
                        HttpWebRequest httpWebRequest2 = (HttpWebRequest)r.AsyncState;
                        HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest2.EndGetResponse(r);
                        int totalBlocks = blockConfig.getTotalBlocks();
                        VideoDataSaxHandler videoDataSaxHandler = new(this);
                        using (videoDataSaxHandler.xmlReader = XmlReader.Create(httpWebResponse.GetResponseStream()))
                        {
                            videoDataSaxHandler.Parse();
                        }
                        success = true;
                        int totalBlocks2 = blockConfig.getTotalBlocks();
                        if (totalBlocks != totalBlocks2)
                        {
                            rebuildCartoonsSelect();
                        }
                        execution = false;
                    }
                    catch (Exception)
                    {
                        success = false;
                        execution = false;
                    }
                }, httpWebRequest);
            }
        }

        public void clear()
        {
            if (!execution && !ImageDownloader.isBusy())
            {
                removeObject("BlockConfig");
                blockConfig = new BlockConfig();
                success = false;
            }
        }

        protected override void injectSizes(LinkBuilder link, int set)
        {
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            switch (set)
            {
                case 0:
                    num = 250;
                    num2 = 40;
                    num3 = 230;
                    num4 = 110;
                    break;
                case 1:
                    num = 364;
                    num2 = 60;
                    num3 = 330;
                    num4 = 150;
                    break;
                case 2:
                    num = 524;
                    num2 = 85;
                    num3 = 480;
                    num4 = 220;
                    break;
            }
            link.put("ad_width", num);
            link.put("ad_height", num2);
            link.put("ep_width", num3);
            link.put("ep_height", num4);
        }

        public void imageDownloaded(string data, Block block)
        {
            bool flag = false;
            try
            {
                flag = saveBytes(Convert.FromBase64String(data), block.getName());
            }
            catch (Exception)
            {
            }
            if (flag)
            {
                block.loadState = Block.LoadState.DONE;
            }
            saveBlockConfig();
        }

        protected void saveBlockConfig()
        {
            _ = saveObject(blockConfig, "BlockConfig");
        }

        public BlockConfig getBlockConfig()
        {
            return blockConfig;
        }

        protected static void rebuildCartoonsSelect()
        {
            CartoonsSelectView.needrebuild = true;
        }

        protected const string TAG = "VideoDataManager";

        protected bool success;

        protected internal BlockConfig blockConfig;

        protected int resolution;

        private sealed class VideoDataSaxHandler(VideoDataManager parrent) : DefaultHandler
        {

            public override void StartElement(string URI, string localName, string qName, Dictionary<string, string> atts)
            {
                if (localName == "response")
                {
                    if (atts.TryGetValue("update", out string text) && text == "true")
                    {
                        updatehash = new Random().Next();
                    }
                }
                else if (localName == "hash")
                {
                    if (atts.TryGetValue("value", out string text2))
                    {
                        parrent.blockConfig.hash = text2;
                    }
                }
                else if (localName is "episode" or "adblock")
                {
                    _ = atts.TryGetValue("id", out string text3);
                    _ = atts.TryGetValue("hash", out string text4);
                    writeblock = parrent.blockConfig.getBlockWithIDandHash(text3, text4);
                    if (writeblock.hash == null)
                    {
                        writeblock.id = text3;
                        writeblock.hash = text4;
                        _ = atts.TryGetValue("number", out writeblock.number);
                        _ = atts.TryGetValue("url", out writeblock.url);
                        _ = atts.TryGetValue("image_id", out writeblock.image_id);
                        writeblock.loadState = writeblock.image_id == null || writeblock.image_id.Length == 0 ? Block.LoadState.NO_IMAGE : Block.LoadState.NOT_LOADED;
                    }
                    writeblock.langs.Clear();
                    writeblock.order = order++;
                    writeblock.updatehash = updatehash;
                }
                else if (localName == "text")
                {
                    textsaving = true;
                }
                if (textsaving)
                {
                    currentlang = localName;
                }
            }

            public override void EndElement(string URI, string localName, string qName)
            {
                if (localName is "episode" or "adblock")
                {
                    writeblock.type = localName;
                    writeblock = null;
                    return;
                }
                if (localName == "text")
                {
                    textsaving = false;
                    currentlang = null;
                    return;
                }
                if (localName == "response")
                {
                    try
                    {
                        if (updatehash != 0)
                        {
                            List<Block> list = parrent.blockConfig.removeOldFiles(updatehash);
                            foreach (Block block in list)
                            {
                                if (block.loadState == Block.LoadState.DONE)
                                {
                                    removeObject(block.getName());
                                }
                            }
                            if (order != parrent.blockConfig.getTotalBlocks())
                            {
                                parrent.blockConfig.setBroken();
                            }
                            parrent.saveBlockConfig();
                        }
                        List<Block> blocksWaitingForDownload = parrent.blockConfig.getBlocksWaitingForDownload();
                        foreach (Block block2 in blocksWaitingForDownload)
                        {
                            ImageDownloader.download(block2);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            public override void Characters(string ch)
            {
                if (textsaving && currentlang != null)
                {
                    StringBuilder stringBuilder = writeblock.langs.TryGetValue(currentlang, out string value) ? new StringBuilder(value) : new StringBuilder();
                    _ = stringBuilder.Append(ch);
                    writeblock.langs[currentlang] = stringBuilder.ToString();
                }
            }

            private int updatehash;

            private Block writeblock;

            private bool textsaving;

            private string currentlang;

            private int order;

            private readonly VideoDataManager parrent = parrent;
        }
    }
}
