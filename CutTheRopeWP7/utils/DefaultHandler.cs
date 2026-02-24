using System.Collections.Generic;
using System.Xml;

namespace ctr_wp7.utils
{
    // Token: 0x02000002 RID: 2
    public abstract class DefaultHandler
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        public void Parse()
        {
            if (xmlReader != null)
            {
                while (xmlReader.Read())
                {
                    XmlNodeType nodeType = xmlReader.NodeType;
                    switch (nodeType)
                    {
                        case XmlNodeType.Element:
                            {
                                Dictionary<string, string> dictionary = [];
                                string baseURI = xmlReader.BaseURI;
                                string localName = xmlReader.LocalName;
                                string name = xmlReader.Name;
                                bool flag = xmlReader.MoveToFirstAttribute();
                                while (flag)
                                {
                                    string text = "";
                                    if (xmlReader.HasValue)
                                    {
                                        text = xmlReader.Value;
                                    }
                                    dictionary.Add(xmlReader.Name, text);
                                    flag = xmlReader.MoveToNextAttribute();
                                }
                                StartElement(baseURI, localName, name, dictionary);
                                break;
                            }
                        case XmlNodeType.Attribute:
                            break;
                        case XmlNodeType.Text:
                            Characters(xmlReader.Value);
                            break;
                        default:
                            if (nodeType == XmlNodeType.EndElement)
                            {
                                EndElement(xmlReader.BaseURI, xmlReader.LocalName, xmlReader.Name);
                            }
                            break;
                    }
                }
            }
        }

        // Token: 0x06000002 RID: 2
        public abstract void StartElement(string URI, string localName, string qName, Dictionary<string, string> atts);

        // Token: 0x06000003 RID: 3
        public abstract void EndElement(string URI, string localName, string qName);

        // Token: 0x06000004 RID: 4
        public abstract void Characters(string characters);

        // Token: 0x04000001 RID: 1
        public XmlReader xmlReader;
    }
}
