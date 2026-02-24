using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ctr_wp7.remotedata.cartoons
{
    [DataContract]
    public class BlockConfig
    {
        public override string ToString()
        {
            string text = "";
            text = text + "hash " + hash;
            foreach (KeyValuePair<string, Block> keyValuePair in blocks)
            {
                string text2 = text;
                text = string.Concat(new string[]
                {
                    text2,
                    "\r\n bloks ",
                    keyValuePair.Key,
                    " ",
                    keyValuePair.Value.ToString()
                });
            }
            return text;
        }

        public BlockConfig()
        {
            blocks = [];
        }

        public Block getBlockWithIDandHash(string id, string hash)
        {
            _ = blocks.TryGetValue(id, out Block block);
            if (block == null || !block.hash.Equals(hash, StringComparison.Ordinal))
            {
                block = new Block();
                blocks[id] = block;
            }
            return block;
        }

        public List<Block> removeOldFiles(int newhash)
        {
            List<string> list = [];
            List<Block> list2 = [];
            foreach (KeyValuePair<string, Block> keyValuePair in blocks)
            {
                Block value = keyValuePair.Value;
                if (value.updatehash != newhash)
                {
                    list.Add(keyValuePair.Key);
                    list2.Add(value);
                }
            }
            foreach (string text in list)
            {
                _ = blocks.Remove(text);
            }
            return list2;
        }

        public void setBroken()
        {
            hash = null;
            blocks.Clear();
        }

        public List<Block> getBlocksWaitingForDownload()
        {
            List<Block> list = [];
            foreach (KeyValuePair<string, Block> keyValuePair in blocks)
            {
                Block value = keyValuePair.Value;
                if (value.loadState == Block.LoadState.NOT_LOADED)
                {
                    list.Add(value);
                }
            }
            return list;
        }

        public static void dumpContent()
        {
        }

        public Block getBlock(int block)
        {
            foreach (KeyValuePair<string, Block> keyValuePair in blocks)
            {
                Block value = keyValuePair.Value;
                if (value.order == block)
                {
                    return value;
                }
            }
            return null;
        }

        public int getTotalBlocks()
        {
            return blocks.Count;
        }

        private const long serialVersionUID = 957407124495095344L;

        public const string filename = "BlockConfig";

        [DataMember]
        public Dictionary<string, Block> blocks;

        [DataMember]
        public string hash;
    }
}
