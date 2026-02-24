using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using ctre_wp7.iframework.core;
using ctre_wp7.ios;

namespace ctre_wp7.Banner
{
	// Token: 0x02000091 RID: 145
	public class Banner
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x0001E4F0 File Offset: 0x0001C6F0
		internal Banner(XMLNode xmlBanner, int width, int height)
		{
			this.id = xmlBanner["id"].intValue();
			this.name = string.Format("banner_{0}_{1}_{2}.jpg", this.id, width, height);
			string text = xmlBanner.findChildWithTagNameRecursively(NSObject.NSS("data"), false).data.ToString();
			this.saved = this.saveImage(text);
			this.url = xmlBanner.findChildWithTagNameRecursively(NSObject.NSS("url"), false).data.ToString();
			this.langs = new Dictionary<string, string>();
			List<XMLNode> list = xmlBanner.findChildWithTagNameRecursively(NSObject.NSS("text"), false).childs();
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				XMLNode xmlnode = list[i];
				this.langs.Add(xmlnode.Name, xmlnode.data.ToString());
				i++;
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001E5E8 File Offset: 0x0001C7E8
		internal Banner(BinaryReader file)
		{
			this.saved = file.ReadBoolean();
			this.name = file.ReadString();
			this.id = file.ReadInt32();
			this.url = file.ReadString();
			int num = file.ReadInt32();
			this.langs = new Dictionary<string, string>();
			for (int i = 0; i < num; i++)
			{
				string text = file.ReadString();
				string text2 = file.ReadString();
				this.langs.Add(text, text2);
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001E664 File Offset: 0x0001C864
		public void SaveToFile(BinaryWriter file)
		{
			file.Write(this.saved);
			file.Write(this.name);
			file.Write(this.id);
			file.Write(this.url);
			file.Write(this.langs.Count);
			foreach (KeyValuePair<string, string> keyValuePair in this.langs)
			{
				file.Write(keyValuePair.Key);
				file.Write(keyValuePair.Value);
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001E70C File Offset: 0x0001C90C
		protected bool saveImage(string base64)
		{
			bool flag;
			try
			{
				byte[] array = Convert.FromBase64String(base64);
				using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
				{
					using (IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream(this.name, System.IO.FileMode.Create, userStoreForApplication))
					{
						BinaryWriter binaryWriter = new BinaryWriter(isolatedStorageFileStream);
						binaryWriter.Write(array);
						binaryWriter.Close();
					}
				}
				flag = true;
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001E798 File Offset: 0x0001C998
		public string getName()
		{
			return this.name;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0001E7A0 File Offset: 0x0001C9A0
		public string getUrl()
		{
			return this.url;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001E7A8 File Offset: 0x0001C9A8
		public string getString()
		{
			string text = this.langs[Application.sharedAppSettings().getString(8).ToString()];
			if (text == null)
			{
				return "";
			}
			return text;
		}

		// Token: 0x04000998 RID: 2456
		private static long serialVersionUID = 1L;

		// Token: 0x04000999 RID: 2457
		public bool saved;

		// Token: 0x0400099A RID: 2458
		protected string name;

		// Token: 0x0400099B RID: 2459
		public int id;

		// Token: 0x0400099C RID: 2460
		protected string url;

		// Token: 0x0400099D RID: 2461
		protected Dictionary<string, string> langs;
	}
}
