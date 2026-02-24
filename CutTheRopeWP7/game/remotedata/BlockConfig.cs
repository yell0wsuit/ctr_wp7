using ctr_wp7.ios;
using ctr_wp7.remotedata.cartoons;

namespace ctr_wp7.game.remotedata
{
    // Token: 0x0200003D RID: 61
    internal sealed class BlockConfig : NSObject
    {
        // Token: 0x06000216 RID: 534 RVA: 0x0000DE2F File Offset: 0x0000C02F
        public NSObject initWithJObject(ctr_wp7.remotedata.cartoons.BlockConfig pBlockConfig)
        {
            if (base.init() != null)
            {
                jblockConfig = pBlockConfig;
                hardcode = (BlockInterface)new BlockHardcode().init();
            }
            return this;
        }

        // Token: 0x06000217 RID: 535 RVA: 0x0000DE58 File Offset: 0x0000C058
        public int getTotalBlocks()
        {
            if (jblockConfig != null)
            {
                return jblockConfig.getTotalBlocks();
            }
            return 0;
        }

        // Token: 0x06000218 RID: 536 RVA: 0x0000DE7C File Offset: 0x0000C07C
        public BlockInterface getBlock(int blocknum)
        {
            if (blocknum == -1)
            {
                return hardcode;
            }
            Block block = jblockConfig.getBlock(blocknum);
            return (BlockInterface)new BlockInternet().initWithJObject(block);
        }

        // Token: 0x06000219 RID: 537 RVA: 0x0000DEB4 File Offset: 0x0000C0B4
        public int getNextSameType(BlockInterface block)
        {
            int totalBlocks = getTotalBlocks();
            int i;
            for (i = 0; i < totalBlocks; i++)
            {
                BlockInterface block2 = getBlock(i);
                if (block.getId().isEqualToString(block2.getId()))
                {
                    break;
                }
            }
            if (i >= totalBlocks)
            {
                return -1;
            }
            int type = block.getType();
            for (int j = i - 1; j >= 0; j--)
            {
                if (getBlock(j).getType() == type)
                {
                    return j;
                }
            }
            return -1;
        }

        // Token: 0x0600021A RID: 538 RVA: 0x0000DF22 File Offset: 0x0000C122
        public override void dealloc()
        {
            base.dealloc();
        }

        // Token: 0x04000817 RID: 2071
        protected ctr_wp7.remotedata.cartoons.BlockConfig jblockConfig;

        // Token: 0x04000818 RID: 2072
        protected BlockInterface hardcode;
    }
}
