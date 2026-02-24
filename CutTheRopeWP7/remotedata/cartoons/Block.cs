using System.Collections.Generic;
using System.Runtime.Serialization;

using ctr_wp7.iframework.core;

namespace ctr_wp7.remotedata.cartoons
{
    [DataContract]
    public class Block
    {
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

        public Block()
        {
            langs = [];
            updatehash = 0;
        }

        public string getType()
        {
            return type;
        }

        public string getName()
        {
            return image_id == null ? null : "block_" + image_id;
        }

        public string getId()
        {
            return id;
        }

        public string getNumber()
        {
            return number;
        }

        public string getUrl()
        {
            return url;
        }

        public string getText()
        {
            string text = null;
            _ = langs.TryGetValue(ApplicationSettings.getString(8).ToString(), ref text);
            return text == null ? "" : text;
        }

        public bool isImageExists()
        {
            return loadState != LoadState.NO_IMAGE;
        }

        public bool isImageReady()
        {
            return loadState == LoadState.DONE;
        }

        private const long serialVersionUID = -6687496632472033020L;

        public const string filename_prefix = "block_";

        [DataMember]
        public string type;

        [DataMember]
        public int order;

        [DataMember]
        public string hash;

        [DataMember]
        public string id;

        [DataMember]
        public string number;

        [DataMember]
        public string url;

        [DataMember]
        public string image_id;

        [DataMember]
        public LoadState loadState;

        [DataMember]
        public int updatehash;

        [DataMember]
        public Dictionary<string, string> langs;

        public enum LoadState
        {
            NO_IMAGE,
            NOT_LOADED,
            DONE
        }
    }
}
