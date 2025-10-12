using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml;
using ctre_wp7.game;
using ctre_wp7.utils;

namespace ctre_wp7.remotedata.cartoons
{
	// Token: 0x02000070 RID: 112
	public class VideoDataManager : ServerDataManager, ImageDownloader.ImageDownloadedListener
	{
		// Token: 0x0600035E RID: 862 RVA: 0x00015698 File Offset: 0x00013898
		public VideoDataManager()
		{
			ImageDownloader.setListener(this);
			this.protocolVersion = 2;
			this.serverUrl = "http://vps.zeptolab.com/feeder/episodes?";
			object obj = base.readObject("BlockConfig", typeof(BlockConfig));
			if (obj == null)
			{
				this.blockConfig = new BlockConfig();
				return;
			}
			this.blockConfig = (BlockConfig)obj;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000156F4 File Offset: 0x000138F4
		public void initWith(string app, int resolution)
		{
			this.resolution = resolution;
			this.request(app);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000157C0 File Offset: 0x000139C0
		public void request(string app)
		{
			if (!this.success && !this.execution && !ImageDownloader.isBusy())
			{
				this.execution = true;
				LinkBuilder linkBuilder = new LinkBuilder(this.serverUrl);
				linkBuilder.put("app", app);
				linkBuilder.put("platform", "winphone");
				if (this.blockConfig.hash != null)
				{
					linkBuilder.put("hash", this.blockConfig.hash);
				}
				this.injectSizes(linkBuilder, this.resolution);
				ServerDataManager.injectAdditionalParameters(linkBuilder);
				string text = linkBuilder.ToString();
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(text));
				httpWebRequest.BeginGetResponse(delegate(IAsyncResult r)
				{
					try
					{
						HttpWebRequest httpWebRequest2 = (HttpWebRequest)r.AsyncState;
						HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest2.EndGetResponse(r);
						int totalBlocks = this.blockConfig.getTotalBlocks();
						VideoDataManager.VideoDataSaxHandler videoDataSaxHandler = new VideoDataManager.VideoDataSaxHandler(this);
						using (videoDataSaxHandler.xmlReader = XmlReader.Create(httpWebResponse.GetResponseStream()))
						{
							videoDataSaxHandler.Parse();
						}
						this.success = true;
						int totalBlocks2 = this.blockConfig.getTotalBlocks();
						if (totalBlocks != totalBlocks2)
						{
							this.rebuildCartoonsSelect();
						}
						this.execution = false;
					}
					catch (Exception)
					{
						this.success = false;
						this.execution = false;
					}
				}, httpWebRequest);
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00015885 File Offset: 0x00013A85
		public void clear()
		{
			if (!this.execution && !ImageDownloader.isBusy())
			{
				base.removeObject("BlockConfig");
				this.blockConfig = new BlockConfig();
				this.success = false;
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000158B4 File Offset: 0x00013AB4
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

		// Token: 0x06000363 RID: 867 RVA: 0x00015968 File Offset: 0x00013B68
		public void imageDownloaded(string data, Block block)
		{
			bool flag = false;
			try
			{
				flag = base.saveBytes(Convert.FromBase64String(data), block.getName());
			}
			catch (Exception)
			{
			}
			if (flag)
			{
				block.loadState = Block.LoadState.DONE;
			}
			this.saveBlockConfig();
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000159B0 File Offset: 0x00013BB0
		protected void saveBlockConfig()
		{
			base.saveObject(this.blockConfig, "BlockConfig");
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000159C4 File Offset: 0x00013BC4
		public BlockConfig getBlockConfig()
		{
			return this.blockConfig;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000159CC File Offset: 0x00013BCC
		protected void rebuildCartoonsSelect()
		{
			CartoonsSelectView.needrebuild = true;
		}

		// Token: 0x040008EF RID: 2287
		protected const string TAG = "VideoDataManager";

		// Token: 0x040008F0 RID: 2288
		protected bool success;

		// Token: 0x040008F1 RID: 2289
		protected internal BlockConfig blockConfig;

		// Token: 0x040008F2 RID: 2290
		protected int resolution;

		// Token: 0x02000071 RID: 113
		private class VideoDataSaxHandler : DefaultHandler
		{
			// Token: 0x06000368 RID: 872 RVA: 0x000159D4 File Offset: 0x00013BD4
			public VideoDataSaxHandler(VideoDataManager parrent)
			{
				this.parrent = parrent;
			}

			// Token: 0x06000369 RID: 873 RVA: 0x000159E4 File Offset: 0x00013BE4
			public override void StartElement(string URI, string localName, string qName, Dictionary<string, string> atts)
			{
				if (localName == "response")
				{
					string text;
					if (atts.TryGetValue("update", ref text) && text == "true")
					{
						this.updatehash = new Random().Next();
					}
				}
				else if (localName == "hash")
				{
					string text2;
					if (atts.TryGetValue("value", ref text2))
					{
						this.parrent.blockConfig.hash = text2;
					}
				}
				else if (localName == "episode" || localName == "adblock")
				{
					string text3 = null;
					atts.TryGetValue("id", ref text3);
					string text4 = null;
					atts.TryGetValue("hash", ref text4);
					this.writeblock = this.parrent.blockConfig.getBlockWithIDandHash(text3, text4);
					if (this.writeblock.hash == null)
					{
						this.writeblock.id = text3;
						this.writeblock.hash = text4;
						atts.TryGetValue("number", ref this.writeblock.number);
						atts.TryGetValue("url", ref this.writeblock.url);
						atts.TryGetValue("image_id", ref this.writeblock.image_id);
						if (this.writeblock.image_id == null || this.writeblock.image_id.Length == 0)
						{
							this.writeblock.loadState = Block.LoadState.NO_IMAGE;
						}
						else
						{
							this.writeblock.loadState = Block.LoadState.NOT_LOADED;
						}
					}
					this.writeblock.langs.Clear();
					this.writeblock.order = this.order++;
					this.writeblock.updatehash = this.updatehash;
				}
				else if (localName == "text")
				{
					this.textsaving = true;
				}
				if (this.textsaving)
				{
					this.currentlang = localName;
				}
			}

			// Token: 0x0600036A RID: 874 RVA: 0x00015BD0 File Offset: 0x00013DD0
			public override void EndElement(string URI, string localName, string qName)
			{
				if (localName == "episode" || localName == "adblock")
				{
					this.writeblock.type = localName;
					this.writeblock = null;
					return;
				}
				if (localName == "text")
				{
					this.textsaving = false;
					this.currentlang = null;
					return;
				}
				if (localName == "response")
				{
					try
					{
						if (this.updatehash != 0)
						{
							List<Block> list = this.parrent.blockConfig.removeOldFiles(this.updatehash);
							foreach (Block block in list)
							{
								if (block.loadState == Block.LoadState.DONE)
								{
									this.parrent.removeObject(block.getName());
								}
							}
							if (this.order != this.parrent.blockConfig.getTotalBlocks())
							{
								this.parrent.blockConfig.setBroken();
							}
							this.parrent.saveBlockConfig();
						}
						List<Block> blocksWaitingForDownload = this.parrent.blockConfig.getBlocksWaitingForDownload();
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

			// Token: 0x0600036B RID: 875 RVA: 0x00015D40 File Offset: 0x00013F40
			public override void Characters(string ch)
			{
				if (this.textsaving && this.currentlang != null)
				{
					StringBuilder stringBuilder;
					if (this.writeblock.langs.ContainsKey(this.currentlang))
					{
						stringBuilder = new StringBuilder(this.writeblock.langs[this.currentlang]);
					}
					else
					{
						stringBuilder = new StringBuilder();
					}
					stringBuilder.Append(ch);
					this.writeblock.langs[this.currentlang] = stringBuilder.ToString();
				}
			}

			// Token: 0x040008F3 RID: 2291
			private int updatehash;

			// Token: 0x040008F4 RID: 2292
			private Block writeblock;

			// Token: 0x040008F5 RID: 2293
			private bool textsaving;

			// Token: 0x040008F6 RID: 2294
			private string currentlang;

			// Token: 0x040008F7 RID: 2295
			private int order;

			// Token: 0x040008F8 RID: 2296
			private VideoDataManager parrent;
		}
	}
}
