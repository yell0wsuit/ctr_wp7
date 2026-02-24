using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;

using ctr_wp7.iframework.core;
using ctr_wp7.ios;

namespace ctr_wp7.Banner
{
    public class Banner
    {
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

        public string getName()
        {
            return name;
        }

        public string getUrl()
        {
            return url;
        }

        public string getString()
        {
            string text = langs[ApplicationSettings.getString(8).ToString()];
            return text == null ? "" : text;
        }

        private static readonly long serialVersionUID = 1L;

        public bool saved;

        protected string name;

        public int id;

        protected string url;

        protected Dictionary<string, string> langs;
    }
}
