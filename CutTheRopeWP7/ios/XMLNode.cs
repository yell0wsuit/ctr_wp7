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
    internal sealed class XMLNode
    {
        public XMLNode()
        {
            parent = null;
            childs_ = [];
            attributes_ = [];
        }

        // (get) Token: 0x06000763 RID: 1891 RVA: 0x0003B198 File Offset: 0x00039398
        public string Name => name;

        // (get) Token: 0x06000764 RID: 1892 RVA: 0x0003B1A0 File Offset: 0x000393A0
        public NSString data => value;

        public bool attributes()
        {
            return attributes_ != null && attributes_.Count > 0;
        }

        public List<XMLNode> childs()
        {
            return childs_;
        }

        public NSString this[string key]
        {
            get
            {
                return !attributes_.TryGetValue(key, out string text) ? new NSString("") : new NSString(text);
            }
        }

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

        public XMLNode findChildWithTagNameRecursively(NSString tag, bool recursively)
        {
            return findChildWithTagNameRecursively(tag.ToString(), recursively);
        }

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

        public static XMLNode parseXML(string fileName)
        {
            return ParseLINQ(fileName);
        }

        public static void parseXML_URL(string URL, RemoteDataManager_Java MGR)
        {
            ParseLINQ_URL(URL, MGR);
        }

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

        private static void ParseLINQ_URL(string URL, RemoteDataManager_Java MGR)
        {
            MGR_STORED = MGR;
            WebRequest webRequest = WebRequest.Create(URL);
            _ = webRequest.BeginGetResponse(new AsyncCallback(Response_Completed), webRequest);
        }

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

        private int depth;

        private XMLNode parent;

        private readonly List<XMLNode> childs_;

        private string name;

        private NSString value;

        private readonly Dictionary<string, string> attributes_;

        private static RemoteDataManager_Java MGR_STORED;
    }
}
