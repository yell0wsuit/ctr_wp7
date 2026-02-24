using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ctr_wp7.remotedata.cartoons
{
    // Token: 0x02000055 RID: 85
    [DataContract]
    public class BlockConfig
    {
        // Token: 0x06000291 RID: 657 RVA: 0x000106DC File Offset: 0x0000E8DC
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

        // Token: 0x06000292 RID: 658 RVA: 0x00010788 File Offset: 0x0000E988
        public BlockConfig()
        {
            blocks = [];
        }

        // Token: 0x06000293 RID: 659 RVA: 0x0001079C File Offset: 0x0000E99C
        public Block getBlockWithIDandHash(string id, string hash)
        {
            _ = blocks.TryGetValue(id, out Block block);
            if (block == null || !block.hash.Equals(hash))
            {
                block = new Block();
                blocks[id] = block;
            }
            return block;
        }

        // Token: 0x06000294 RID: 660 RVA: 0x000107E0 File Offset: 0x0000E9E0
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

        // Token: 0x06000295 RID: 661 RVA: 0x000108A8 File Offset: 0x0000EAA8
        public void setBroken()
        {
            hash = null;
            blocks.Clear();
        }

        // Token: 0x06000296 RID: 662 RVA: 0x000108BC File Offset: 0x0000EABC
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

        // Token: 0x06000297 RID: 663 RVA: 0x00010928 File Offset: 0x0000EB28
        public void dumpContent()
        {
        }

        // Token: 0x06000298 RID: 664 RVA: 0x0001092C File Offset: 0x0000EB2C
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

        // Token: 0x06000299 RID: 665 RVA: 0x00010990 File Offset: 0x0000EB90
        public int getTotalBlocks()
        {
            return blocks.Count;
        }

        // Token: 0x04000890 RID: 2192
        private const long serialVersionUID = 957407124495095344L;

        // Token: 0x04000891 RID: 2193
        public const string filename = "BlockConfig";

        // Token: 0x04000892 RID: 2194
        [DataMember]
        public Dictionary<string, Block> blocks;

        // Token: 0x04000893 RID: 2195
        [DataMember]
        public string hash;
    }
}
