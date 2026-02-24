using System.Collections.Generic;
using System.Xml;

namespace ctr_wp7.utils
{
    public abstract class DefaultHandler
    {
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

        public abstract void StartElement(string URI, string localName, string qName, Dictionary<string, string> atts);

        public abstract void EndElement(string URI, string localName, string qName);

        public abstract void Characters(string characters);

        public XmlReader xmlReader;
    }
}
