using System.Collections.Generic;
using System.Runtime.Serialization;

using ctr_wp7.iframework.core;

namespace ctr_wp7.remotedata.cartoons
{
    // Token: 0x020000D8 RID: 216
    [DataContract]
    public class Block
    {
        // Token: 0x06000649 RID: 1609 RVA: 0x0003074C File Offset: 0x0002E94C
        public override string ToString()
        {
            string text = "";
            object obj = text;
            text = string.Concat(new object[]
            {
                obj, "type ", type, " order ", order, " hash ", hash, " id ", id, " number ",
                number, " url ", url, " image_id ", image_id, " loadState ", loadState, " updatehash ", updatehash
            });
            foreach (KeyValuePair<string, string> keyValuePair in langs)
            {
                string text2 = text;
                text = string.Concat(new string[] { text2, " ", keyValuePair.Key, " ", keyValuePair.Value });
            }
            return text;
        }

        // Token: 0x0600064A RID: 1610 RVA: 0x000308A8 File Offset: 0x0002EAA8
        public Block()
        {
            langs = [];
            updatehash = 0;
        }

        // Token: 0x0600064B RID: 1611 RVA: 0x000308C2 File Offset: 0x0002EAC2
        public string getType()
        {
            return type;
        }

        // Token: 0x0600064C RID: 1612 RVA: 0x000308CA File Offset: 0x0002EACA
        public string getName()
        {
            if (image_id == null)
            {
                return null;
            }
            return "block_" + image_id;
        }

        // Token: 0x0600064D RID: 1613 RVA: 0x000308E6 File Offset: 0x0002EAE6
        public string getId()
        {
            return id;
        }

        // Token: 0x0600064E RID: 1614 RVA: 0x000308EE File Offset: 0x0002EAEE
        public string getNumber()
        {
            return number;
        }

        // Token: 0x0600064F RID: 1615 RVA: 0x000308F6 File Offset: 0x0002EAF6
        public string getUrl()
        {
            return url;
        }

        // Token: 0x06000650 RID: 1616 RVA: 0x00030900 File Offset: 0x0002EB00
        public string getText()
        {
            string text = null;
            _ = langs.TryGetValue(Application.sharedAppSettings().getString(8).ToString(), ref text);
            if (text == null)
            {
                return "";
            }
            return text;
        }

        // Token: 0x06000651 RID: 1617 RVA: 0x00030937 File Offset: 0x0002EB37
        public bool isImageExists()
        {
            return loadState != LoadState.NO_IMAGE;
        }

        // Token: 0x06000652 RID: 1618 RVA: 0x00030944 File Offset: 0x0002EB44
        public bool isImageReady()
        {
            return loadState == LoadState.DONE;
        }

        // Token: 0x04000BB4 RID: 2996
        private const long serialVersionUID = -6687496632472033020L;

        // Token: 0x04000BB5 RID: 2997
        public const string filename_prefix = "block_";

        // Token: 0x04000BB6 RID: 2998
        [DataMember]
        public string type;

        // Token: 0x04000BB7 RID: 2999
        [DataMember]
        public int order;

        // Token: 0x04000BB8 RID: 3000
        [DataMember]
        public string hash;

        // Token: 0x04000BB9 RID: 3001
        [DataMember]
        public string id;

        // Token: 0x04000BBA RID: 3002
        [DataMember]
        public string number;

        // Token: 0x04000BBB RID: 3003
        [DataMember]
        public string url;

        // Token: 0x04000BBC RID: 3004
        [DataMember]
        public string image_id;

        // Token: 0x04000BBD RID: 3005
        [DataMember]
        public LoadState loadState;

        // Token: 0x04000BBE RID: 3006
        [DataMember]
        public int updatehash;

        // Token: 0x04000BBF RID: 3007
        [DataMember]
        public Dictionary<string, string> langs;

        // Token: 0x020000D9 RID: 217
        public enum LoadState
        {
            // Token: 0x04000BC1 RID: 3009
            NO_IMAGE,
            // Token: 0x04000BC2 RID: 3010
            NOT_LOADED,
            // Token: 0x04000BC3 RID: 3011
            DONE
        }
    }
}
