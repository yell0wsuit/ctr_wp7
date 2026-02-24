using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;

using ctr_wp7.iframework.core;
using ctr_wp7.ios;

namespace ctr_wp7.Banner
{
    // Token: 0x02000091 RID: 145
    public class Banner
    {
        // Token: 0x0600044F RID: 1103 RVA: 0x0001E4F0 File Offset: 0x0001C6F0
        internal Banner(XMLNode xmlBanner, int width, int height)
        {
            id = xmlBanner["id"].intValue();
            name = string.Format("banner_{0}_{1}_{2}.jpg", id, width, height);
            string text = xmlBanner.findChildWithTagNameRecursively(NSObject.NSS("data"), false).data.ToString();
            saved = saveImage(text);
            url = xmlBanner.findChildWithTagNameRecursively(NSObject.NSS("url"), false).data.ToString();
            langs = [];
            List<XMLNode> list = xmlBanner.findChildWithTagNameRecursively(NSObject.NSS("text"), false).childs();
            int i = 0;
            int count = list.Count;
            while (i < count)
            {
                XMLNode xmlnode = list[i];
                langs.Add(xmlnode.Name, xmlnode.data.ToString());
                i++;
            }
        }

        // Token: 0x06000450 RID: 1104 RVA: 0x0001E5E8 File Offset: 0x0001C7E8
        internal Banner(BinaryReader file)
        {
            saved = file.ReadBoolean();
            name = file.ReadString();
            id = file.ReadInt32();
            url = file.ReadString();
            int num = file.ReadInt32();
            langs = [];
            for (int i = 0; i < num; i++)
            {
                string text = file.ReadString();
                string text2 = file.ReadString();
                langs.Add(text, text2);
            }
        }

        // Token: 0x06000451 RID: 1105 RVA: 0x0001E664 File Offset: 0x0001C864
        public void SaveToFile(BinaryWriter file)
        {
            file.Write(saved);
            file.Write(name);
            file.Write(id);
            file.Write(url);
            file.Write(langs.Count);
            foreach (KeyValuePair<string, string> keyValuePair in langs)
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
                    using (IsolatedStorageFileStream isolatedStorageFileStream = new(name, FileMode.Create, userStoreForApplication))
                    {
                        BinaryWriter binaryWriter = new(isolatedStorageFileStream);
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
            return name;
        }

        // Token: 0x06000454 RID: 1108 RVA: 0x0001E7A0 File Offset: 0x0001C9A0
        public string getUrl()
        {
            return url;
        }

        // Token: 0x06000455 RID: 1109 RVA: 0x0001E7A8 File Offset: 0x0001C9A8
        public string getString()
        {
            string text = langs[ApplicationSettings.getString(8).ToString()];
            return text == null ? "" : text;
        }

        // Token: 0x04000998 RID: 2456
        private static readonly long serialVersionUID = 1L;

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
