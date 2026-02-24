using System;
using System.Collections.Generic;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.core
{
    // Token: 0x0200006D RID: 109
    internal class ResourceMgr : NSObject
    {
        // Token: 0x06000336 RID: 822 RVA: 0x000148D0 File Offset: 0x00012AD0
        public virtual bool hasResource(int resID)
        {
            NSObject nsobject = null;
            this.s_Resources.TryGetValue(resID, out nsobject);
            return nsobject != null;
        }

        // Token: 0x06000337 RID: 823 RVA: 0x000148F5 File Offset: 0x00012AF5
        public virtual void addResourceToLoadQueue(int resID)
        {
            this.loadQueue.Add(resID);
            this.loadCount++;
        }

        // Token: 0x06000338 RID: 824 RVA: 0x00014914 File Offset: 0x00012B14
        public virtual NSObject loadResource(int resID, ResourceMgr.ResourceType resType)
        {
            NSObject nsobject = null;
            if (this.s_Resources.TryGetValue(resID, out nsobject))
            {
                return nsobject;
            }
            string text = ((resType != ResourceMgr.ResourceType.STRINGS) ? CTRResourceMgr.XNA_ResName(resID) : "");
            bool flag = this.isWvgaResource(resID);
            float num = this.getNormalScaleX(resID);
            float num2 = this.getNormalScaleY(resID);
            if (flag)
            {
                num = this.getWvgaScaleX(resID);
                num2 = this.getWvgaScaleY(resID);
            }
            switch (resType)
            {
                case ResourceMgr.ResourceType.IMAGE:
                    nsobject = this.loadTextureImageInfo(text, null, flag, num, num2);
                    break;
                case ResourceMgr.ResourceType.FONT:
                    nsobject = this.loadVariableFontInfo(text, resID, flag);
                    this.s_Resources.Remove(resID);
                    break;
                case ResourceMgr.ResourceType.SOUND:
                    nsobject = this.loadSoundInfo(text);
                    break;
                case ResourceMgr.ResourceType.STRINGS:
                    {
                        nsobject = this.loadStringsInfo(resID);
                        string text2 = nsobject.ToString();
                        nsobject = NSObject.NSS(text2.Replace('\u00a0', ' '));
                        break;
                    }
            }
            if (nsobject != null)
            {
                this.s_Resources.Add(resID, nsobject);
            }
            return nsobject;
        }

        // Token: 0x06000339 RID: 825 RVA: 0x000149FF File Offset: 0x00012BFF
        public virtual NSObject loadSoundInfo(string path)
        {
            return new NSObject().init();
        }

        // Token: 0x0600033A RID: 826 RVA: 0x00014A0C File Offset: 0x00012C0C
        public NSString loadStringsInfo(int key)
        {
            key &= 65535;
            if (this.xmlStrings == null)
            {
                this.xmlStrings = XMLNode.parseXML("menu_strings.xml");
            }
            XMLNode xmlnode = this.xmlStrings.childs()[key];
            if (xmlnode != null)
            {
                string text = "en";
                if (ResDataPhoneFull.LANGUAGE == Language.LANG_RU)
                {
                    text = "ru";
                }
                if (ResDataPhoneFull.LANGUAGE == Language.LANG_FR)
                {
                    text = "fr";
                }
                if (ResDataPhoneFull.LANGUAGE == Language.LANG_DE)
                {
                    text = "de";
                }
                if (ResDataPhoneFull.LANGUAGE == Language.LANG_IT)
                {
                    text = "it";
                }
                if (ResDataPhoneFull.LANGUAGE == Language.LANG_NL)
                {
                    text = "nl";
                }
                if (ResDataPhoneFull.LANGUAGE == Language.LANG_BR)
                {
                    text = "br";
                }
                if (ResDataPhoneFull.LANGUAGE == Language.LANG_ES)
                {
                    text = "es";
                }
                if (ResDataPhoneFull.LANGUAGE == Language.LANG_ZH)
                {
                    text = "zh";
                }
                if (ResDataPhoneFull.LANGUAGE == Language.LANG_KO)
                {
                    text = "ko";
                }
                if (ResDataPhoneFull.LANGUAGE == Language.LANG_JA)
                {
                    text = "ja";
                }
                XMLNode xmlnode2 = xmlnode.findChildWithTagNameRecursively(text, false);
                return xmlnode2.data;
            }
            return new NSString();
        }

        // Token: 0x0600033B RID: 827 RVA: 0x00014AFC File Offset: 0x00012CFC
        public virtual FontGeneric loadVariableFontInfo(string path, int resID, bool isWvga)
        {
            XMLNode xmlnode = XMLNode.parseXML(path);
            int num = xmlnode["charoff"].intValue();
            int num2 = xmlnode["lineoff"].intValue();
            int num3 = xmlnode["space"].intValue();
            XMLNode xmlnode2 = xmlnode.findChildWithTagNameRecursively("chars", false);
            XMLNode xmlnode3 = xmlnode.findChildWithTagNameRecursively("kerning", false);
            NSString data = xmlnode2.data;
            if (xmlnode3 != null)
            {
                NSString data2 = xmlnode3.data;
            }
            FontGeneric fontGeneric = new Font().initWithVariableSizeCharscharMapFileKerning(data, (Texture2D)this.loadResource(resID, ResourceMgr.ResourceType.IMAGE), null);
            fontGeneric.setCharOffsetLineOffsetSpaceWidth((float)num, (float)num2, (float)num3);
            return fontGeneric;
        }

        // Token: 0x0600033C RID: 828 RVA: 0x00014BA0 File Offset: 0x00012DA0
        public virtual Texture2D loadTextureImageInfo(string path, XMLNode i, bool isWvga, float scaleX, float scaleY)
        {
            if (i == null)
            {
                i = XMLNode.parseXML(path);
            }
            if (i == null)
            {
                throw new InvalidOperationException("Texture metadata not found for '" + path + "'.");
            }
            int num = i["filter"].intValue();
            bool flag = (num & 1) == 1;
            int num2 = i["format"].intValue();
            string text = ResourceMgr.fullPathFromRelativePath(path);
            Texture2D texture2D = this.tryLoadTextureAsset(text, flag, num2);
            if (texture2D == null && isWvga && path.EndsWith("_hd", StringComparison.OrdinalIgnoreCase))
            {
                string text2 = path.Substring(0, path.Length - 3);
                XMLNode xmlnode = XMLNode.parseXML(text2);
                if (xmlnode != null)
                {
                    path = text2;
                    i = xmlnode;
                    num = i["filter"].intValue();
                    flag = (num & 1) == 1;
                    num2 = i["format"].intValue();
                    text = ResourceMgr.fullPathFromRelativePath(path);
                    isWvga = false;
                    scaleX = 1f;
                    scaleY = 1f;
                    texture2D = this.tryLoadTextureAsset(text, flag, num2);
                }
            }
            if (texture2D == null)
            {
                throw new InvalidOperationException("Texture asset not found for '" + path + "' (content key '" + text + "'). Place mobile XNB files under content/.");
            }
            if (isWvga)
            {
                texture2D.setWvga();
            }
            texture2D.setScale(scaleX, scaleY);
            this.setTextureInfo(texture2D, i, isWvga, scaleX, scaleY);
            return texture2D;
        }

        private Texture2D tryLoadTextureAsset(string contentKey, bool useLinearFilter, int format)
        {
            if (useLinearFilter)
            {
                Texture2D.setAntiAliasTexParameters();
            }
            else
            {
                Texture2D.setAliasTexParameters();
            }
            Texture2D.setDefaultAlphaPixelFormat((Texture2D.Texture2DPixelFormat)format);
            Texture2D texture2D = new Texture2D().initWithPath(contentKey, true);
            Texture2D.setDefaultAlphaPixelFormat(Texture2D.kTexture2DPixelFormat_Default);
            return texture2D;
        }

        // Token: 0x0600033D RID: 829 RVA: 0x00014C3C File Offset: 0x00012E3C
        public virtual Texture2D loadTextureImageInfo(string path)
        {
            Texture2D.setAntiAliasTexParameters();
            Texture2D texture2D = new Texture2D().initWithPath(path, false);
            if (FrameworkTypes.IS_WVGA)
            {
                texture2D.setWvga();
            }
            texture2D.setScale(this.getScaleX(-1), this.getScaleY(-1));
            return texture2D;
        }

        // Token: 0x0600033E RID: 830 RVA: 0x00014C80 File Offset: 0x00012E80
        public virtual void setTextureInfo(Texture2D t, XMLNode i, bool isWvga, float scaleX, float scaleY)
        {
            t.preCutSize = MathHelper.vectUndefined;
            XMLNode xmlnode = i.findChildWithTagNameRecursively("quads", false);
            if (xmlnode != null)
            {
                List<NSString> list = xmlnode.data.componentsSeparatedByString(',');
                if (list != null && list.Count > 0)
                {
                    float[] array = new float[list.Count];
                    for (int j = 0; j < list.Count; j++)
                    {
                        array[j] = list[j].floatValue();
                    }
                    this.setQuadsInfo(t, array, list.Count, scaleX, scaleY);
                }
            }
            XMLNode xmlnode2 = i.findChildWithTagNameRecursively("offsets", false);
            if (xmlnode2 != null)
            {
                List<NSString> list2 = xmlnode2.data.componentsSeparatedByString(',');
                if (list2 != null && list2.Count > 0)
                {
                    float[] array2 = new float[list2.Count];
                    for (int k = 0; k < list2.Count; k++)
                    {
                        array2[k] = list2[k].floatValue();
                    }
                    this.setOffsetsInfo(t, array2, list2.Count, scaleX, scaleY);
                    XMLNode xmlnode3 = i.findChildWithTagNameRecursively(NSObject.NSS("preCutWidth"), false);
                    XMLNode xmlnode4 = i.findChildWithTagNameRecursively(NSObject.NSS("preCutHeight"), false);
                    if (xmlnode3 != null && xmlnode4 != null)
                    {
                        t.preCutSize = MathHelper.vect((float)xmlnode3.data.intValue(), (float)xmlnode4.data.intValue());
                        if (isWvga)
                        {
                            t.preCutSize.x = t.preCutSize.x / 1.5f;
                            t.preCutSize.y = t.preCutSize.y / 1.5f;
                        }
                    }
                }
            }
        }

        // Token: 0x0600033F RID: 831 RVA: 0x00014E08 File Offset: 0x00013008
        private static string fullPathFromRelativePath(string relPath)
        {
            return ResDataPhoneFull.ContentFolder + relPath;
        }

        // Token: 0x06000340 RID: 832 RVA: 0x00014E18 File Offset: 0x00013018
        private void setQuadsInfo(Texture2D t, float[] data, int size, float scaleX, float scaleY)
        {
            int num = data.Length / 4;
            t.setQuadsCapacity(num);
            int num2 = -1;
            for (int i = 0; i < num; i++)
            {
                int num3 = i * 4;
                Rectangle rectangle = FrameworkTypes.MakeRectangle(data[num3], data[num3 + 1], data[num3 + 2], data[num3 + 3]);
                if ((float)num2 < rectangle.h + rectangle.y)
                {
                    num2 = (int)MathHelper.ceil((double)(rectangle.h + rectangle.y));
                }
                rectangle.x /= scaleX;
                rectangle.y /= scaleY;
                rectangle.w /= scaleX;
                rectangle.h /= scaleY;
                t.setQuadAt(rectangle, i);
            }
            if (num2 != -1)
            {
                t._lowypoint = num2;
            }
            t.optimizeMemory();
        }

        // Token: 0x06000341 RID: 833 RVA: 0x00014EE8 File Offset: 0x000130E8
        private void setOffsetsInfo(Texture2D t, float[] data, int size, float scaleX, float scaleY)
        {
            int num = size / 2;
            for (int i = 0; i < num; i++)
            {
                int num2 = i * 2;
                t.quadOffsets[i].x = data[num2];
                t.quadOffsets[i].y = data[num2 + 1];
                Vector[] quadOffsets = t.quadOffsets;
                int num3 = i;
                quadOffsets[num3].x = quadOffsets[num3].x / scaleX;
                Vector[] quadOffsets2 = t.quadOffsets;
                int num4 = i;
                quadOffsets2[num4].y = quadOffsets2[num4].y / scaleY;
            }
        }

        // Token: 0x06000342 RID: 834 RVA: 0x00014F67 File Offset: 0x00013167
        public virtual bool isWvgaResource(int r)
        {
            return true;
        }

        // Token: 0x06000343 RID: 835 RVA: 0x00014F6A File Offset: 0x0001316A
        public virtual float getNormalScaleX(int r)
        {
            return 1f;
        }

        // Token: 0x06000344 RID: 836 RVA: 0x00014F71 File Offset: 0x00013171
        public virtual float getNormalScaleY(int r)
        {
            return 1f;
        }

        // Token: 0x06000345 RID: 837 RVA: 0x00014F78 File Offset: 0x00013178
        public virtual float getWvgaScaleX(int r)
        {
            return 1.5f;
        }

        // Token: 0x06000346 RID: 838 RVA: 0x00014F7F File Offset: 0x0001317F
        public virtual float getWvgaScaleY(int r)
        {
            return 1.5f;
        }

        // Token: 0x06000347 RID: 839 RVA: 0x00014F86 File Offset: 0x00013186
        public virtual void initLoading()
        {
            this.loadQueue.Clear();
            this.loaded = 0;
            this.loadCount = 0;
        }

        // Token: 0x06000348 RID: 840 RVA: 0x00014FA1 File Offset: 0x000131A1
        public virtual int getPercentLoaded()
        {
            if (this.loadCount == 0)
            {
                return 100;
            }
            return 100 * this.loaded / this.getLoadCount();
        }

        // Token: 0x06000349 RID: 841 RVA: 0x00014FC0 File Offset: 0x000131C0
        public virtual void loadPack(int[] pack)
        {
            int num = 0;
            while (pack[num] != -1)
            {
                this.addResourceToLoadQueue(pack[num]);
                num++;
            }
        }

        // Token: 0x0600034A RID: 842 RVA: 0x00014FE4 File Offset: 0x000131E4
        public virtual void freePack(int[] pack)
        {
            int num = 0;
            while (pack[num] != -1)
            {
                this.freeResource(pack[num]);
                num++;
            }
        }

        // Token: 0x0600034B RID: 843 RVA: 0x00015008 File Offset: 0x00013208
        public virtual void loadImmediately()
        {
            while (this.loadQueue.Count != 0)
            {
                int num = this.loadQueue[0];
                this.loadQueue.RemoveAt(0);
                this.loadResource(num);
                this.loaded++;
            }
        }

        // Token: 0x0600034C RID: 844 RVA: 0x00015052 File Offset: 0x00013252
        public virtual void startLoading()
        {
            if (this.resourcesDelegate != null)
            {
                this.Timer = NSTimer.schedule(new DelayedDispatcher.DispatchFunc(ResourceMgr.rmgr_internalUpdate), this, 0.022222223f);
            }
            this.bUseFake = this.loadQueue.Count < 100;
        }

        // Token: 0x0600034D RID: 845 RVA: 0x0001508E File Offset: 0x0001328E
        private int getLoadCount()
        {
            if (!this.bUseFake)
            {
                return this.loadCount;
            }
            return 100;
        }

        // Token: 0x0600034E RID: 846 RVA: 0x000150A4 File Offset: 0x000132A4
        public void update()
        {
            if (this.loadQueue.Count > 0)
            {
                int num = this.loadQueue[0];
                this.loadQueue.RemoveAt(0);
                this.loadResource(num);
            }
            this.loaded++;
            if (this.loaded >= this.getLoadCount())
            {
                if (this.Timer >= 0)
                {
                    NSTimer.stopTimer(this.Timer);
                }
                this.Timer = -1;
                this.resourcesDelegate.allResourcesLoaded();
            }
        }

        // Token: 0x0600034F RID: 847 RVA: 0x00015121 File Offset: 0x00013321
        private static void rmgr_internalUpdate(NSObject obj)
        {
            ((ResourceMgr)obj).update();
        }

        // Token: 0x06000350 RID: 848 RVA: 0x00015130 File Offset: 0x00013330
        private void loadResource(int resId)
        {
            if (411 < resId)
            {
                return;
            }
            if (20 == resId)
            {
                if (this.xmlStrings == null)
                {
                    this.xmlStrings = XMLNode.parseXML("menu_strings.xml");
                    return;
                }
            }
            else
            {
                if (resId == 58 || resId == 59)
                {
                    Application.sharedSoundMgr().LoadMusic(resId);
                    return;
                }
                if (ResDataPhoneFull.isSound(resId))
                {
                    Application.sharedSoundMgr().getSound(resId);
                    return;
                }
                if (ResDataPhoneFull.isFont(resId))
                {
                    Application.getFont(resId);
                    return;
                }
                Application.getTexture(resId);
            }
        }

        // Token: 0x06000351 RID: 849 RVA: 0x000151A8 File Offset: 0x000133A8
        public virtual void freeResource(int resId)
        {
            if (411 < resId)
            {
                return;
            }
            if (20 == resId)
            {
                this.xmlStrings = null;
                return;
            }
            if (ResDataPhoneFull.isSound(resId))
            {
                Application.sharedSoundMgr().freeSound(resId);
                return;
            }
            NSObject nsobject = null;
            if (this.s_Resources.TryGetValue(resId, out nsobject))
            {
                if (nsobject != null)
                {
                    nsobject.dealloc();
                }
                this.s_Resources.Remove(resId);
            }
        }

        // Token: 0x06000352 RID: 850 RVA: 0x00015207 File Offset: 0x00013407
        public virtual float getScaleX(int r)
        {
            return 1f;
        }

        // Token: 0x06000353 RID: 851 RVA: 0x0001520E File Offset: 0x0001340E
        public virtual float getScaleY(int r)
        {
            return 1f;
        }

        // Token: 0x040008DD RID: 2269
        public ResourceMgrDelegate resourcesDelegate;

        // Token: 0x040008DE RID: 2270
        private Dictionary<int, NSObject> s_Resources = new Dictionary<int, NSObject>();

        // Token: 0x040008DF RID: 2271
        private XMLNode xmlStrings;

        // Token: 0x040008E0 RID: 2272
        private int loaded;

        // Token: 0x040008E1 RID: 2273
        private int loadCount;

        // Token: 0x040008E2 RID: 2274
        private List<int> loadQueue = new List<int>();

        // Token: 0x040008E3 RID: 2275
        private int Timer;

        // Token: 0x040008E4 RID: 2276
        private bool bUseFake;

        // Token: 0x0200006E RID: 110
        public enum ResourceType
        {
            // Token: 0x040008E6 RID: 2278
            IMAGE,
            // Token: 0x040008E7 RID: 2279
            FONT,
            // Token: 0x040008E8 RID: 2280
            SOUND,
            // Token: 0x040008E9 RID: 2281
            BINARY,
            // Token: 0x040008EA RID: 2282
            STRINGS,
            // Token: 0x040008EB RID: 2283
            ELEMENT
        }
    }
}
