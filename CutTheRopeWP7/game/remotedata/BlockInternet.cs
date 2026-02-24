using ctr_wp7.ios;
using ctr_wp7.remotedata.cartoons;

namespace ctr_wp7.game.remotedata
{
    internal sealed class BlockInternet : BlockInterface
    {
        public NSObject initWithJObject(Block pBlock)
        {
            if (init() != null)
            {
                jblock = pBlock;
            }
            return this;
        }

        public override int getType()
        {
            string type = jblock.getType();
            return type == "adblock" ? 0 : 1;
        }

        public override NSString getName()
        {
            string name = jblock.getName();
            return (name == null) ? null : NSS(name);
        }

        public override NSString getId()
        {
            string id = jblock.getId();
            return NSS(id);
        }

        public override NSString getUrl()
        {
            string url = jblock.getUrl();
            return NSS(url);
        }

        public override NSString getText()
        {
            string text = jblock.getText();
            return NSS(text);
        }

        public override NSString getNumber()
        {
            string number = jblock.getNumber();
            return NSS(number);
        }

        public override bool isImageExists()
        {
            return jblock.isImageExists();
        }

        public override bool isImageReady()
        {
            return jblock.isImageReady();
        }

        public override void dealloc()
        {
            base.dealloc();
        }

        private Block jblock;
    }
}
