using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.game
{
    internal sealed class PackSelectInfo : NSObject
    {
        public NSObject initWithSize(int pSize)
        {
            if (init() != null)
            {
                i = 0;
                size = pSize;
                nextpack = -1;
                content = new int[size];
                elements = new BaseElement[size];
            }
            return this;
        }

        public bool isBoxExist(int box)
        {
            for (int i = 0; i < size; i++)
            {
                if (content[i] == box)
                {
                    return true;
                }
            }
            return false;
        }

        public int firstIndexOf(int box)
        {
            for (int i = 0; i < size; i++)
            {
                if (content[i] == box)
                {
                    return i;
                }
            }
            return 0;
        }

        public int getFirstGameBox()
        {
            for (int i = 0; i < size; i++)
            {
                if (BoxFabric.isGameBox(content[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public void add(int box)
        {
            content[i] = box;
            i++;
        }

        public override void dealloc()
        {
            content = null;
            elements = null;
            base.dealloc();
        }

        private int i;

        public int size;

        public int nextpack;

        public int[] content;

        public BaseElement[] elements;
    }
}
