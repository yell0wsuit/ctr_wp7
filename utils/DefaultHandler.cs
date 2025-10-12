using System;
using System.Collections.Generic;
using System.Xml;

namespace ctre_wp7.utils
{
	// Token: 0x02000002 RID: 2
	public abstract class DefaultHandler
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public void Parse()
		{
			if (this.xmlReader != null)
			{
				while (this.xmlReader.Read())
				{
					XmlNodeType nodeType = this.xmlReader.NodeType;
					switch (nodeType)
					{
					case 1:
					{
						Dictionary<string, string> dictionary = new Dictionary<string, string>();
						string baseURI = this.xmlReader.BaseURI;
						string localName = this.xmlReader.LocalName;
						string name = this.xmlReader.Name;
						bool flag = this.xmlReader.MoveToFirstAttribute();
						while (flag)
						{
							string text = "";
							if (this.xmlReader.HasValue)
							{
								text = this.xmlReader.Value;
							}
							dictionary.Add(this.xmlReader.Name, text);
							flag = this.xmlReader.MoveToNextAttribute();
						}
						this.StartElement(baseURI, localName, name, dictionary);
						break;
					}
					case 2:
						break;
					case 3:
						this.Characters(this.xmlReader.Value);
						break;
					default:
						if (nodeType == 15)
						{
							this.EndElement(this.xmlReader.BaseURI, this.xmlReader.LocalName, this.xmlReader.Name);
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
