using ctr_wp7.ios;
using ctr_wp7.remotedata.cartoons;

namespace ctr_wp7.game.remotedata
{
    internal sealed class BlockConfig : NSObject
    {
        public NSObject initWithJObject(ctr_wp7.remotedata.cartoons.BlockConfig pBlockConfig)
        {
            if (init() != null)
            {
                jblockConfig = pBlockConfig;
                hardcode = (BlockInterface)new BlockHardcode().init();
            }
            return this;
        }

        public int getTotalBlocks()
        {
            return jblockConfig != null ? jblockConfig.getTotalBlocks() : 0;
        }

        public BlockInterface getBlock(int blocknum)
        {
            if (blocknum == -1)
            {
                return hardcode;
            }
            Block block = jblockConfig.getBlock(blocknum);
            return (BlockInterface)new BlockInternet().initWithJObject(block);
        }

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

        public override void dealloc()
        {
            base.dealloc();
        }

        private ctr_wp7.remotedata.cartoons.BlockConfig jblockConfig;

        private BlockInterface hardcode;
    }
}
