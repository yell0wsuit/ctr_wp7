using System;
using System.Collections.Generic;

using ctr_wp7.ctr_original;
using ctr_wp7.iframework.helpers;
using ctr_wp7.iframework.visual;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.core
{
    internal class ResourceMgr : NSObject
    {
        public virtual bool hasResource(int resID)
        {
            _ = s_Resources.TryGetValue(resID, out NSObject nsobject);
            return nsobject != null;
        }

        public virtual void addResourceToLoadQueue(int resID)
        {
            loadQueue.Add(resID);
            loadCount++;
        }

        public virtual NSObject loadResource(int resID, ResourceType resType)
        {
            if (s_Resources.TryGetValue(resID, out NSObject nsobject))
            {
                return nsobject;
            }
            string text = (resType != ResourceType.STRINGS) ? CTRResourceMgr.XNA_ResName(resID) : "";
            bool flag = isWvgaResource(resID);
            float num = getNormalScaleX(resID);
            float num2 = getNormalScaleY(resID);
            if (flag)
            {
                num = getWvgaScaleX(resID);
                num2 = getWvgaScaleY(resID);
            }
            switch (resType)
            {
                case ResourceType.IMAGE:
                    nsobject = loadTextureImageInfo(text, null, flag, num, num2);
                    break;
                case ResourceType.FONT:
                    nsobject = loadVariableFontInfo(text, resID, flag);
                    _ = s_Resources.Remove(resID);
                    break;
                case ResourceType.SOUND:
                    nsobject = loadSoundInfo(text);
                    break;
                case ResourceType.STRINGS:
                    {
                        nsobject = loadStringsInfo(resID);
                        string text2 = nsobject.ToString();
                        nsobject = NSS(text2.Replace('\u00a0', ' '));
                        break;
                    }
            }
            if (nsobject != null)
            {
                s_Resources.Add(resID, nsobject);
            }
            return nsobject;
        }

        public virtual NSObject loadSoundInfo(string path)
        {
            return new NSObject().init();
        }

        public NSString loadStringsInfo(int key)
        {
            key &= 65535;
            xmlStrings ??= XMLNode.parseXML("menu_strings.xml");
            XMLNode xmlnode = xmlStrings.childs()[key];
            if (xmlnode != null)
            {
                string text = "en";
                if (LANGUAGE == Language.LANG_RU)
                {
                    text = "ru";
                }
                if (LANGUAGE == Language.LANG_FR)
                {
                    text = "fr";
                }
                if (LANGUAGE == Language.LANG_DE)
                {
                    text = "de";
                }
                if (LANGUAGE == Language.LANG_IT)
                {
                    text = "it";
                }
                if (LANGUAGE == Language.LANG_NL)
                {
                    text = "nl";
                }
                if (LANGUAGE == Language.LANG_BR)
                {
                    text = "br";
                }
                if (LANGUAGE == Language.LANG_ES)
                {
                    text = "es";
                }
                if (LANGUAGE == Language.LANG_ZH)
                {
                    text = "zh";
                }
                if (LANGUAGE == Language.LANG_KO)
                {
                    text = "ko";
                }
                if (LANGUAGE == Language.LANG_JA)
                {
                    text = "ja";
                }
                XMLNode xmlnode2 = xmlnode.findChildWithTagNameRecursively(text, false);
                return xmlnode2.data;
            }
            return new NSString();
        }

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
                _ = xmlnode3.data;
            }
            FontGeneric fontGeneric = new Font().initWithVariableSizeCharscharMapFileKerning(data, (Texture2D)loadResource(resID, ResourceType.IMAGE), null);
            fontGeneric.setCharOffsetLineOffsetSpaceWidth(num, num2, num3);
            return fontGeneric;
        }

        public virtual Texture2D loadTextureImageInfo(string path, XMLNode i, bool isWvga, float scaleX, float scaleY)
        {
            i ??= XMLNode.parseXML(path);
            if (i == null)
            {
                throw new InvalidOperationException("Texture metadata not found for '" + path + "'.");
            }
            int num = i["filter"].intValue();
            bool flag = (num & 1) == 1;
            int num2 = i["format"].intValue();
            string text = fullPathFromRelativePath(path);
            Texture2D texture2D = tryLoadTextureAsset(text, flag, num2);
            if (texture2D == null && isWvga && path.EndsWith("_hd", StringComparison.OrdinalIgnoreCase))
            {
                string text2 = path[..^3];
                XMLNode xmlnode = XMLNode.parseXML(text2);
                if (xmlnode != null)
                {
                    path = text2;
                    i = xmlnode;
                    num = i["filter"].intValue();
                    flag = (num & 1) == 1;
                    num2 = i["format"].intValue();
                    text = fullPathFromRelativePath(path);
                    isWvga = false;
                    scaleX = 1f;
                    scaleY = 1f;
                    texture2D = tryLoadTextureAsset(text, flag, num2);
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
            setTextureInfo(texture2D, i, isWvga, scaleX, scaleY);
            return texture2D;
        }

        private static Texture2D tryLoadTextureAsset(string contentKey, bool useLinearFilter, int format)
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

        public virtual Texture2D loadTextureImageInfo(string path)
        {
            Texture2D.setAntiAliasTexParameters();
            Texture2D texture2D = new Texture2D().initWithPath(path, false);
            if (IS_WVGA)
            {
                texture2D.setWvga();
            }
            texture2D.setScale(getScaleX(-1), getScaleY(-1));
            return texture2D;
        }

        public virtual void setTextureInfo(Texture2D t, XMLNode i, bool isWvga, float scaleX, float scaleY)
        {
            t.preCutSize = vectUndefined;
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
                    setQuadsInfo(t, array, list.Count, scaleX, scaleY);
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
                    setOffsetsInfo(t, array2, list2.Count, scaleX, scaleY);
                    XMLNode xmlnode3 = i.findChildWithTagNameRecursively(NSS("preCutWidth"), false);
                    XMLNode xmlnode4 = i.findChildWithTagNameRecursively(NSS("preCutHeight"), false);
                    if (xmlnode3 != null && xmlnode4 != null)
                    {
                        t.preCutSize = vect(xmlnode3.data.intValue(), xmlnode4.data.intValue());
                        if (isWvga)
                        {
                            t.preCutSize.x /= 1.5f;
                            t.preCutSize.y /= 1.5f;
                        }
                    }
                }
            }
        }

        private static string fullPathFromRelativePath(string relPath)
        {
            return ContentFolder + relPath;
        }

        private static void setQuadsInfo(Texture2D t, float[] data, int size, float scaleX, float scaleY)
        {
            int num = data.Length / 4;
            t.setQuadsCapacity(num);
            int num2 = -1;
            for (int i = 0; i < num; i++)
            {
                int num3 = i * 4;
                Rectangle rectangle = MakeRectangle(data[num3], data[num3 + 1], data[num3 + 2], data[num3 + 3]);
                if (num2 < rectangle.h + rectangle.y)
                {
                    num2 = (int)ceil((double)(rectangle.h + rectangle.y));
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
            Texture2D.optimizeMemory();
        }

        private static void setOffsetsInfo(Texture2D t, float[] data, int size, float scaleX, float scaleY)
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

        public virtual bool isWvgaResource(int r)
        {
            return true;
        }

        public virtual float getNormalScaleX(int r)
        {
            return 1f;
        }

        public virtual float getNormalScaleY(int r)
        {
            return 1f;
        }

        public virtual float getWvgaScaleX(int r)
        {
            return 1.5f;
        }

        public virtual float getWvgaScaleY(int r)
        {
            return 1.5f;
        }

        public virtual void initLoading()
        {
            loadQueue.Clear();
            loaded = 0;
            loadCount = 0;
        }

        public virtual int getPercentLoaded()
        {
            return loadCount == 0 ? 100 : 100 * loaded / getLoadCount();
        }

        public virtual void loadPack(int[] pack)
        {
            int num = 0;
            while (pack[num] != -1)
            {
                addResourceToLoadQueue(pack[num]);
                num++;
            }
        }

        public virtual void freePack(int[] pack)
        {
            int num = 0;
            while (pack[num] != -1)
            {
                freeResource(pack[num]);
                num++;
            }
        }

        public virtual void loadImmediately()
        {
            while (loadQueue.Count != 0)
            {
                int num = loadQueue[0];
                loadQueue.RemoveAt(0);
                loadResource(num);
                loaded++;
            }
        }

        public virtual void startLoading()
        {
            if (resourcesDelegate != null)
            {
                Timer = NSTimer.schedule(new DelayedDispatcher.DispatchFunc(rmgr_internalUpdate), this, 0.022222223f);
            }
            bUseFake = loadQueue.Count < 100;
        }

        private int getLoadCount()
        {
            return !bUseFake ? loadCount : 100;
        }

        public void update()
        {
            if (loadQueue.Count > 0)
            {
                int num = loadQueue[0];
                loadQueue.RemoveAt(0);
                loadResource(num);
            }
            loaded++;
            if (loaded >= getLoadCount())
            {
                if (Timer >= 0)
                {
                    NSTimer.stopTimer(Timer);
                }
                Timer = -1;
                resourcesDelegate.allResourcesLoaded();
            }
        }

        private static void rmgr_internalUpdate(NSObject obj)
        {
            ((ResourceMgr)obj).update();
        }

        private void loadResource(int resId)
        {
            if (411 < resId)
            {
                return;
            }
            if (20 == resId)
            {
                if (xmlStrings == null)
                {
                    xmlStrings = XMLNode.parseXML("menu_strings.xml");
                    return;
                }
            }
            else
            {
                if (resId is 58 or 59)
                {
                    Application.sharedSoundMgr().LoadMusic(resId);
                    return;
                }
                if (isSound(resId))
                {
                    _ = Application.sharedSoundMgr().getSound(resId);
                    return;
                }
                if (isFont(resId))
                {
                    _ = Application.getFont(resId);
                    return;
                }
                _ = Application.getTexture(resId);
            }
        }

        public virtual void freeResource(int resId)
        {
            if (411 < resId)
            {
                return;
            }
            if (20 == resId)
            {
                xmlStrings = null;
                return;
            }
            if (isSound(resId))
            {
                Application.sharedSoundMgr().freeSound(resId);
                return;
            }
            if (s_Resources.TryGetValue(resId, out NSObject nsobject))
            {
                nsobject?.dealloc();
                _ = s_Resources.Remove(resId);
            }
        }

        public virtual float getScaleX(int r)
        {
            return 1f;
        }

        public virtual float getScaleY(int r)
        {
            return 1f;
        }

        public ResourceMgrDelegate resourcesDelegate;

        private readonly Dictionary<int, NSObject> s_Resources = [];

        private XMLNode xmlStrings;

        private int loaded;

        private int loadCount;

        private readonly List<int> loadQueue = [];

        private int Timer;

        private bool bUseFake;

        public enum ResourceType
        {
            IMAGE,
            FONT,
            SOUND,
            BINARY,
            STRINGS,
            ELEMENT
        }
    }
}
