using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;

using ctr_wp7.ctr_original;
using ctr_wp7.game;
using ctr_wp7.iframework;
using ctr_wp7.ios;

namespace ctr_wp7
{
    // Token: 0x0200005E RID: 94
    public class SpecialFont
    {
        // Token: 0x060002D1 RID: 721 RVA: 0x000121DC File Offset: 0x000103DC
        private static void CheckCollison(int R1)
        {
            if (R1 > Regions.Count - 2)
            {
                return;
            }
            if (Regions[R1].End >= Regions[R1 + 1].Start)
            {
                Region region = new Region();
                region.Start = Regions[R1].Start;
                region.End = Regions[R1 + 1].End;
                Regions[R1] = region;
                Regions.RemoveAt(R1 + 1);
            }
        }

        // Token: 0x060002D2 RID: 722 RVA: 0x0001226C File Offset: 0x0001046C
        public static void AddString(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                AddCharacter(s[i]);
            }
        }

        // Token: 0x060002D3 RID: 723 RVA: 0x00012298 File Offset: 0x00010498
        private static void AddCharacter(char c)
        {
            for (int i = 0; i < Regions.Count; i++)
            {
                Region region = Regions[i];
                if (c >= region.Start && c <= region.End)
                {
                    return;
                }
                if (c < region.Start - '\u0001')
                {
                    Region region2 = new Region();
                    region2.Start = c;
                    region2.End = c;
                    Regions.Insert(i, region2);
                    return;
                }
                if (c == region.Start - '\u0001')
                {
                    Region region3 = region;
                    region3.Start -= '\u0001';
                    return;
                }
                if (c == region.End + '\u0001')
                {
                    Region region4 = region;
                    region4.End += '\u0001';
                    CheckCollison(i);
                    return;
                }
            }
            Region region5 = new Region();
            region5.Start = c;
            region5.End = c;
            Regions.Add(region5);
        }

        // Token: 0x060002D4 RID: 724 RVA: 0x00012368 File Offset: 0x00010568
        public static void MakeSpriteFont()
        {
            string text = "custom.spritefont";
            using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    userStoreForApplication.DeleteFile(text);
                }
                catch (Exception)
                {
                }
                using (IsolatedStorageFileStream isolatedStorageFileStream = userStoreForApplication.CreateFile(text))
                {
                    StreamWriter streamWriter = new StreamWriter(isolatedStorageFileStream);
                    streamWriter.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    streamWriter.WriteLine("<XnaContent xmlns:Graphics=\"Microsoft.Xna.Framework.Content.Pipeline.Graphics\">");
                    streamWriter.WriteLine("<Asset Type=\"Graphics:FontDescription\">");
                    streamWriter.WriteLine("<FontName>Segoe UI Mono</FontName>");
                    streamWriter.WriteLine("<Size>14</Size>");
                    streamWriter.WriteLine("<Spacing>0</Spacing>");
                    streamWriter.WriteLine("<UseKerning>true</UseKerning>");
                    streamWriter.WriteLine("<Style>Regular</Style>");
                    streamWriter.WriteLine("<CharacterRegions>");
                    streamWriter.WriteLine("</CharacterRegions>");
                    for (int i = 0; i < Regions.Count; i++)
                    {
                        streamWriter.WriteLine("<CharacterRegion>");
                        streamWriter.WriteLine("<Start>&#" + (int)Regions[i].Start + "</Start>");
                        streamWriter.WriteLine("<End>&#" + (int)Regions[i].End + "</End>");
                        streamWriter.WriteLine("</CharacterRegion>");
                    }
                    streamWriter.WriteLine("</Asset>");
                    streamWriter.WriteLine("</XnaContent>");
                    streamWriter.Close();
                }
            }
        }

        // Token: 0x060002D5 RID: 725 RVA: 0x00012510 File Offset: 0x00010710
        internal static void ProcessMenuStrings(XMLNode xmlStrings, Language LANGUAGE)
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
            if (LANGUAGE == Language.LANG_KO)
            {
                text = "ko";
            }
            foreach (XMLNode xmlnode in xmlStrings.childs())
            {
                XMLNode xmlnode2 = xmlnode.findChildWithTagNameRecursively(text, false);
                AddString(xmlnode2.data.ToString());
            }
        }

        // Token: 0x060002D6 RID: 726 RVA: 0x000125D0 File Offset: 0x000107D0
        internal static void ProcessLevel(XMLNode Level)
        {
            int count = Level.childs().Count;
            for (int i = 0; i < count; i++)
            {
                XMLNode xmlnode = Level.childs()[i];
                int count2 = xmlnode.childs().Count;
                for (int j = 0; j < count2; j++)
                {
                    XMLNode xmlnode2 = xmlnode.childs()[j];
                    if (xmlnode2.Name == "tutorialText" && !GameScene.shouldSkipTutorialElement(xmlnode2))
                    {
                        NSString nsstring = xmlnode2["text"];
                        AddString(nsstring.ToString());
                    }
                }
            }
        }

        // Token: 0x060002D7 RID: 727 RVA: 0x00012664 File Offset: 0x00010864
        internal static void ProcessAllLevels()
        {
            int packsCount = CTRPreferences.getPacksCount();
            int levelsInPackCount = CTRPreferences.getLevelsInPackCount();
            for (int i = 0; i < packsCount; i++)
            {
                for (int j = 0; j < levelsInPackCount; j++)
                {
                    XMLNode xmlnode = XMLNode.parseXML("maps/" + LevelsList.LEVEL_NAMES[i, j].ToString());
                    ProcessLevel(xmlnode);
                }
            }
        }

        // Token: 0x040008BA RID: 2234
        private static List<Region> Regions = new List<Region>();

        // Token: 0x0200005F RID: 95
        private class Region
        {
            // Token: 0x040008BB RID: 2235
            public char Start;

            // Token: 0x040008BC RID: 2236
            public char End;
        }
    }
}
