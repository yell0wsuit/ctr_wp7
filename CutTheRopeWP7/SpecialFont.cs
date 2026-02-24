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
    public class SpecialFont
    {
        private static void CheckCollison(int R1)
        {
            if (R1 > Regions.Count - 2)
            {
                return;
            }
            if (Regions[R1].End >= Regions[R1 + 1].Start)
            {
                Region region = new()
                {
                    Start = Regions[R1].Start,
                    End = Regions[R1 + 1].End
                };
                Regions[R1] = region;
                Regions.RemoveAt(R1 + 1);
            }
        }

        public static void AddString(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                AddCharacter(s[i]);
            }
        }

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
                    Region region2 = new()
                    {
                        Start = c,
                        End = c
                    };
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
            Region region5 = new()
            {
                Start = c,
                End = c
            };
            Regions.Add(region5);
        }

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
                    StreamWriter streamWriter = new(isolatedStorageFileStream);
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

        private static readonly List<Region> Regions = [];

        private sealed class Region
        {
            public char Start;

            public char End;
        }
    }
}
