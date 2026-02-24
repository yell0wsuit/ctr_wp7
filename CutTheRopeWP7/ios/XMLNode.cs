using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;

using ctr_wp7.Banner;
using ctr_wp7.ctr_original;
using ctr_wp7.wp7utilities;

using Microsoft.Xna.Framework;

namespace ctr_wp7.ios
{
    // Token: 0x020000F4 RID: 244
    internal sealed class XMLNode
    {
        // Token: 0x06000762 RID: 1890 RVA: 0x0003B173 File Offset: 0x00039373
        public XMLNode()
        {
            parent = null;
            childs_ = [];
            attributes_ = [];
        }

        // Token: 0x1700001F RID: 31
        // (get) Token: 0x06000763 RID: 1891 RVA: 0x0003B198 File Offset: 0x00039398
        public string Name => name;

        // Token: 0x17000020 RID: 32
        // (get) Token: 0x06000764 RID: 1892 RVA: 0x0003B1A0 File Offset: 0x000393A0
        public NSString data => value;

        // Token: 0x06000765 RID: 1893 RVA: 0x0003B1A8 File Offset: 0x000393A8
        public bool attributes()
        {
            return attributes_ != null && attributes_.Count > 0;
        }

        // Token: 0x06000766 RID: 1894 RVA: 0x0003B1C2 File Offset: 0x000393C2
        public List<XMLNode> childs()
        {
            return childs_;
        }

        // Token: 0x17000021 RID: 33
        public NSString this[string key]
        {
            get
            {
                return !attributes_.TryGetValue(key, out string text) ? new NSString("") : new NSString(text);
            }
        }

        // Token: 0x06000768 RID: 1896 RVA: 0x0003B1FC File Offset: 0x000393FC
        public XMLNode findChildWithTagNameAndAttributeNameValueRecursively(string tag, string attrName, string attrVal, bool recursively)
        {
            if (childs() == null)
            {
                return null;
            }
            foreach (XMLNode xmlnode in childs_)
            {
                if (xmlnode.name == tag && xmlnode.attributes() && xmlnode.attributes_.TryGetValue(attrName, out string text) && text == attrVal)
                {
                    return xmlnode;
                }
                if (recursively && xmlnode.childs() != null)
                {
                    XMLNode xmlnode2 = xmlnode.findChildWithTagNameRecursively(tag, recursively);
                    if (xmlnode2 != null)
                    {
                        return xmlnode2;
                    }
                }
            }
            return null;
        }

        // Token: 0x06000769 RID: 1897 RVA: 0x0003B2A8 File Offset: 0x000394A8
        public XMLNode findChildWithTagNameRecursively(NSString tag, bool recursively)
        {
            return findChildWithTagNameRecursively(tag.ToString(), recursively);
        }

        // Token: 0x0600076A RID: 1898 RVA: 0x0003B2B8 File Offset: 0x000394B8
        public XMLNode findChildWithTagNameRecursively(string tag, bool recursively)
        {
            if (childs() == null)
            {
                return null;
            }
            foreach (XMLNode xmlnode in childs_)
            {
                if (xmlnode.name == tag)
                {
                    return xmlnode;
                }
                if (recursively && xmlnode.childs() != null)
                {
                    XMLNode xmlnode2 = xmlnode.findChildWithTagNameRecursively(tag, recursively);
                    if (xmlnode2 != null)
                    {
                        return xmlnode2;
                    }
                }
            }
            return null;
        }

        // Token: 0x0600076B RID: 1899 RVA: 0x0003B340 File Offset: 0x00039540
        public List<XMLNode> getElementsByTagName(string tag)
        {
            List<XMLNode> list = [];
            foreach (XMLNode xmlnode in childs_)
            {
                if (xmlnode.name == tag)
                {
                    list.Add(xmlnode);
                }
            }
            return list;
        }

        // Token: 0x0600076C RID: 1900 RVA: 0x0003B3A8 File Offset: 0x000395A8
        private static XMLNode ReadNode(XmlReader textReader, XMLNode parent)
        {
            while (textReader.NodeType != XmlNodeType.Element && textReader.Read())
            {
            }
            if (textReader.NodeType != XmlNodeType.Element)
            {
                return null;
            }
            XMLNode xmlnode = new();
            if (parent != null)
            {
                xmlnode.parent = parent;
                parent.childs_.Add(xmlnode);
            }
            xmlnode.name = textReader.Name;
            xmlnode.depth = textReader.Depth;
            if (textReader.HasAttributes)
            {
                while (textReader.MoveToNextAttribute())
                {
                    xmlnode.attributes_.Add(textReader.Name, textReader.Value);
                }
                _ = textReader.MoveToElement();
            }
            bool flag = false;
            try
            {
                xmlnode.value = new NSString(textReader.ReadElementContentAsString());
                goto IL_00A5;
            }
            catch (Exception)
            {
                flag = true;
                goto IL_00A5;
            }
        IL_009D:
            _ = ReadNode(textReader, xmlnode);
        IL_00A5:
            if ((!flag && !textReader.Read()) || textReader.Depth <= xmlnode.depth)
            {
                return xmlnode;
            }
            goto IL_009D;
        }

        // Token: 0x0600076D RID: 1901 RVA: 0x0003B488 File Offset: 0x00039688
        public static XMLNode parseXML(string fileName)
        {
            return ParseLINQ(fileName);
        }

        // Token: 0x0600076E RID: 1902 RVA: 0x0003B490 File Offset: 0x00039690
        public static void parseXML_URL(string URL, RemoteDataManager_Java MGR)
        {
            ParseLINQ_URL(URL, MGR);
        }

        // Token: 0x0600076F RID: 1903 RVA: 0x0003B49C File Offset: 0x0003969C
        private static XMLNode ReadNodeLINQ(XElement nodeLinq, XMLNode parent)
        {
            XMLNode xmlnode = new();
            if (parent != null)
            {
                xmlnode.parent = parent;
                parent.childs_.Add(xmlnode);
            }
            xmlnode.name = nodeLinq.Name.ToString();
            string text = (string)nodeLinq;
            if (text != null)
            {
                xmlnode.value = new NSString(text);
            }
            IEnumerable<XAttribute> enumerable = nodeLinq.Attributes();
            foreach (XAttribute xattribute in enumerable)
            {
                xmlnode.attributes_.Add(xattribute.Name.ToString(), xattribute.Value);
            }
            IEnumerable<XElement> enumerable2 = nodeLinq.Elements();
            foreach (XElement xelement in enumerable2)
            {
                _ = ReadNodeLINQ(xelement, xmlnode);
            }
            return xmlnode;
        }

        // Token: 0x06000770 RID: 1904 RVA: 0x0003B594 File Offset: 0x00039794
        private static XMLNode ParseLINQ(string fileName)
        {
            XDocument xdocument = null;
            if (fileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
            {
                xdocument = TryLoadXmlFromContent(fileName);
            }
            if (xdocument == null)
            {
                string text = TryGetFallbackXml(fileName);
                if (!string.IsNullOrWhiteSpace(text))
                {
                    xdocument = XDocument.Parse(text);
                }
            }
            if (xdocument == null)
            {
                throw new InvalidOperationException("Unable to load XML resource '" + fileName + "'.");
            }
            IEnumerable<XElement> enumerable = xdocument.Elements();
            return ReadNodeLINQ(Enumerable.First(enumerable), null);
        }

        private static XDocument TryLoadXmlFromContent(string fileName)
        {
            foreach (string text in GetContentXmlCandidatePaths(fileName))
            {
                try
                {
                    using (Stream stream = TitleContainer.OpenStream(text))
                    {
                        return XDocument.Load(stream);
                    }
                }
                catch (Exception)
                {
                }
            }
            return null;
        }

        private static IEnumerable<string> GetContentXmlCandidatePaths(string fileName)
        {
            string text = fileName.Replace('\\', '/').TrimStart('/');
            string text2 = (WP7Singletons.Content != null && !string.IsNullOrWhiteSpace(WP7Singletons.Content.RootDirectory)) ? WP7Singletons.Content.RootDirectory : "content";
            string text3 = (ResDataPhoneFull.ContentFolder ?? "").Replace('\\', '/').Trim('/');
            HashSet<string> hashSet = [];
            foreach (string text4 in new string[] { text2, "content", "Content" })
            {
                string[] array = text3.Length == 0 ? [text] : [text3 + "/" + text, text];
                foreach (string text5 in array)
                {
                    string text6 = (text4.TrimEnd('/') + "/" + text5.TrimStart('/')).Replace('\\', '/');
                    if (hashSet.Add(text6))
                    {
                        yield return text6;
                    }
                }
            }
        }

        private static string TryGetFallbackXml(string fileName)
        {
            HashSet<string> hashSet = [];
            foreach (string text in new string[] { fileName, Path.GetFileName(fileName), Path.GetFileNameWithoutExtension(fileName) })
            {
                if (!string.IsNullOrWhiteSpace(text) && hashSet.Add(text))
                {
                    string xml = ResDataPhoneFull.GetXml(text);
                    if (!string.IsNullOrWhiteSpace(xml))
                    {
                        return xml;
                    }
                }
            }
            return null;
        }

        // Token: 0x06000771 RID: 1905 RVA: 0x0003B638 File Offset: 0x00039838
        private static void ParseLINQ_URL(string URL, RemoteDataManager_Java MGR)
        {
            MGR_STORED = MGR;
            WebRequest webRequest = WebRequest.Create(URL);
            _ = webRequest.BeginGetResponse(new AsyncCallback(Response_Completed), webRequest);
        }

        // Token: 0x06000772 RID: 1906 RVA: 0x0003B668 File Offset: 0x00039868
        private static void Response_Completed(IAsyncResult result)
        {
            try
            {
                WebRequest webRequest = (WebRequest)result.AsyncState;
                WebResponse webResponse = webRequest.EndGetResponse(result);
                Stream responseStream = webResponse.GetResponseStream();
                XDocument xdocument = XDocument.Load(responseStream);
                responseStream.Dispose();
                IEnumerable<XElement> enumerable = xdocument.Elements();
                _ = MGR_STORED.XMLDownloadFinished(ReadNodeLINQ(Enumerable.First(enumerable), null));
            }
            catch (WebException)
            {
            }
            catch (Exception)
            {
            }
        }

        // Token: 0x04000CE6 RID: 3302
        private int depth;

        // Token: 0x04000CE7 RID: 3303
        private XMLNode parent;

        // Token: 0x04000CE8 RID: 3304
        private readonly List<XMLNode> childs_;

        // Token: 0x04000CE9 RID: 3305
        private string name;

        // Token: 0x04000CEA RID: 3306
        private NSString value;

        // Token: 0x04000CEB RID: 3307
        private readonly Dictionary<string, string> attributes_;

        // Token: 0x04000CEC RID: 3308
        private static RemoteDataManager_Java MGR_STORED;
    }
}
