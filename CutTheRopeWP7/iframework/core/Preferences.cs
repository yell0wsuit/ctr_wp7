using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

using ctr_wp7.ios;

namespace ctr_wp7.iframework.core
{
    // Token: 0x02000083 RID: 131
    internal class Preferences : NSObject
    {
        // Token: 0x060003C4 RID: 964 RVA: 0x00018139 File Offset: 0x00016339
        public override NSObject init()
        {
            if (base.init() == null)
            {
                return null;
            }
            _ = _loadPreferences();
            initialised = true;
            return this;
        }

        // Token: 0x060003C5 RID: 965 RVA: 0x00018152 File Offset: 0x00016352
        public static void ResetPreferences()
        {
            data_ = [];
            dataStrings_ = [];
        }

        // Token: 0x060003C6 RID: 966 RVA: 0x00018174 File Offset: 0x00016374
        public virtual void setIntforKey(int v, string k, bool comit)
        {
            _setIntforKey(v, k, comit);
        }

        // Token: 0x060003C7 RID: 967 RVA: 0x0001817E File Offset: 0x0001637E
        public virtual void setBooleanforKey(bool v, string k, bool comit)
        {
            _setBooleanforKey(v, k, comit);
        }

        // Token: 0x060003C8 RID: 968 RVA: 0x00018188 File Offset: 0x00016388
        public virtual void setStringforKey(string v, string k, bool comit)
        {
            _setStringforKey(v, k, comit);
        }

        // Token: 0x060003C9 RID: 969 RVA: 0x00018192 File Offset: 0x00016392
        public virtual int getIntForKey(string k)
        {
            return _getIntForKey(k);
        }

        // Token: 0x060003CA RID: 970 RVA: 0x0001819A File Offset: 0x0001639A
        public virtual float getFloatForKey(string k)
        {
            return 0f;
        }

        // Token: 0x060003CB RID: 971 RVA: 0x000181A1 File Offset: 0x000163A1
        public virtual bool getBooleanForKey(string k)
        {
            return _getBooleanForKey(k);
        }

        // Token: 0x060003CC RID: 972 RVA: 0x000181A9 File Offset: 0x000163A9
        public virtual string getStringForKey(string k)
        {
            return _getStringForKey(k);
        }

        // Token: 0x060003CD RID: 973 RVA: 0x000181B4 File Offset: 0x000163B4
        public static void _setIntforKey(int v, string key, bool comit)
        {
            data_[key] = v;
            if (comit)
            {
                _savePreferences();
            }
        }

        // Token: 0x060003CE RID: 974 RVA: 0x000181F4 File Offset: 0x000163F4
        public static void _setStringforKey(string v, string k, bool comit)
        {
            dataStrings_[k] = v;
            if (comit)
            {
                _savePreferences();
            }
        }

        // Token: 0x060003CF RID: 975 RVA: 0x00018234 File Offset: 0x00016434
        public static int _getIntForKey(string k)
        {
            return data_.TryGetValue(k, out int num) ? num : 0;
        }

        // Token: 0x060003D0 RID: 976 RVA: 0x00018253 File Offset: 0x00016453
        private static float _getFloatForKey(string k)
        {
            return 0f;
        }

        // Token: 0x060003D1 RID: 977 RVA: 0x0001825C File Offset: 0x0001645C
        public static bool _getBooleanForKey(string k)
        {
            int num = _getIntForKey(k);
            return num != 0;
        }

        // Token: 0x060003D2 RID: 978 RVA: 0x00018277 File Offset: 0x00016477
        public static void _setBooleanforKey(bool v, string k, bool comit)
        {
            _setIntforKey(v ? 1 : 0, k, comit);
        }

        // Token: 0x060003D3 RID: 979 RVA: 0x00018288 File Offset: 0x00016488
        public static string _getStringForKey(string k)
        {
            return dataStrings_.TryGetValue(k, out string text) ? text : "";
        }

        // Token: 0x060003D4 RID: 980 RVA: 0x000182AC File Offset: 0x000164AC
        public static void _deleteKey(string k, bool comit)
        {
            if (dataStrings_.TryGetValue(k, out _))
            {
                _ = dataStrings_.Remove(k);
            }

            if (data_.TryGetValue(k, out _))
            {
                _ = data_.Remove(k);
            }
            if (comit)
            {
                _savePreferences();
            }
        }

        // Token: 0x060003D5 RID: 981 RVA: 0x000182F8 File Offset: 0x000164F8
        public static void _deleteKeysStartWith(string ks, bool comit)
        {
            List<string> list = [];
            foreach (KeyValuePair<string, string> keyValuePair in dataStrings_)
            {
                string key = keyValuePair.Key;
                if (key.StartsWith(ks))
                {
                    list.Add(key);
                }
            }
            foreach (string text in list)
            {
                _ = dataStrings_.Remove(text);
            }
            list.Clear();
            foreach (KeyValuePair<string, int> keyValuePair2 in data_)
            {
                string key2 = keyValuePair2.Key;
                if (key2.StartsWith(ks))
                {
                    list.Add(key2);
                }
            }
            foreach (string text2 in list)
            {
                _ = data_.Remove(text2);
            }
            if (comit)
            {
                _savePreferences();
            }
        }

        // Token: 0x060003D6 RID: 982 RVA: 0x00018450 File Offset: 0x00016650
        public static bool isSaveFinished()
        {
            return !saveInProcess;
        }

        // Token: 0x060003D7 RID: 983 RVA: 0x0001845A File Offset: 0x0001665A
        public virtual void savePreferences()
        {
            _savePreferences();
        }

        // Token: 0x060003D8 RID: 984 RVA: 0x00018464 File Offset: 0x00016664
        public static void _savePreferences()
        {
            string saveFilePath = getSaveFilePath();
            try
            {
                _ = Directory.CreateDirectory(getSaveDirectoryPath());
                PreferencesSaveData preferencesSaveData = new()
                {
                    ints = data_,
                    strings = dataStrings_
                };
                string contents = JsonSerializer.Serialize(preferencesSaveData);
                File.WriteAllText(saveFilePath, contents);
            }
            catch (Exception ex2)
            {
                _LOG("Error: cannot save, " + ex2.ToString());
            }
        }

        // Token: 0x060003D9 RID: 985 RVA: 0x00018728 File Offset: 0x00016928
        public static void loadFromFile(string fname)
        {
            try
            {
                save_check = 'N';
                string json = File.ReadAllText(fname);
                PreferencesSaveData preferencesSaveData = JsonSerializer.Deserialize<PreferencesSaveData>(json) ?? throw new InvalidDataException("Save file is empty.");
                data_ = preferencesSaveData.ints ?? [];
                dataStrings_ = preferencesSaveData.strings ?? [];
                firstStart = false;
            }
            catch (Exception ex)
            {
                _LOG("Error:" + ex.ToString());
                _LOG("Error:" + fname + "corrupted");
                save_check = 'C';
            }
        }

        // Token: 0x060003DA RID: 986 RVA: 0x00018824 File Offset: 0x00016A24
        public virtual bool loadPreferences()
        {
            return _loadPreferences();
        }

        // Token: 0x060003DB RID: 987 RVA: 0x0001882C File Offset: 0x00016A2C
        internal static bool _loadPreferences()
        {
            string saveFilePath = getSaveFilePath();
            if (!File.Exists(saveFilePath))
            {
                _LOG("Info: nothing to load");
                return !firstStart;
            }

            save_check = 'C';
            loadFromFile(saveFilePath);
            if (save_check != 'N')
            {
                _LOG("Error:" + saveFilePath + "corrupted");
                try
                {
                    File.Delete(saveFilePath);
                }
                catch (Exception ex)
                {
                    _LOG("Error: cannot delete corrupted save, " + ex.ToString());
                }
                ResetPreferences();
            }
            return !firstStart;
        }

        private static string getSaveDirectoryPath()
        {
            if (saveDirectoryPath_ != null)
            {
                return saveDirectoryPath_;
            }

            saveDirectoryPath_ = determineSaveDirectoryPath();
            _LOG("Info: using save directory: " + saveDirectoryPath_);
            return saveDirectoryPath_;
        }

        private static string getSaveFilePath()
        {
            return Path.Combine(getSaveDirectoryPath(), saveFileName_);
        }

        private static string determineSaveDirectoryPath()
        {
            string exeDir = AppContext.BaseDirectory;
            if (!isInsideMacAppBundle(exeDir))
            {
                string exeSaveDir = Path.Combine(exeDir, saveDirectoryName_);
                if (tryCreateDirectory(exeSaveDir))
                {
                    return exeSaveDir;
                }
            }

            string documentsDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                saveDirectoryName_);
            if (tryCreateDirectory(documentsDir))
            {
                return documentsDir;
            }

            string localAppDataDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                saveDirectoryName_);
            if (tryCreateDirectory(localAppDataDir))
            {
                return localAppDataDir;
            }

            _LOG("Warning: all save directory options failed, using current directory");
            return ".";
        }

        private static bool tryCreateDirectory(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    _ = Directory.CreateDirectory(path);
                }
                return isDirectoryWritable(path);
            }
            catch
            {
                return false;
            }
        }

        private static bool isDirectoryWritable(string path)
        {
            try
            {
                string testFilePath = Path.Combine(path, ".write_test_" + Guid.NewGuid().ToString("N"));
                File.WriteAllText(testFilePath, "test");
                File.Delete(testFilePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool isInsideMacAppBundle(string path)
        {
            DirectoryInfo directoryInfo = new(path);
            while (directoryInfo != null)
            {
                if (directoryInfo.Name.Equals("MacOS", StringComparison.OrdinalIgnoreCase) &&
                    directoryInfo.Parent?.Name.Equals("Contents", StringComparison.OrdinalIgnoreCase) == true &&
                    directoryInfo.Parent.Parent?.Name.EndsWith(".app", StringComparison.OrdinalIgnoreCase) == true)
                {
                    return true;
                }
                directoryInfo = directoryInfo.Parent;
            }

            return false;
        }

        // Token: 0x04000945 RID: 2373
        private static Dictionary<string, int> data_ = [];

        // Token: 0x04000946 RID: 2374
        private static Dictionary<string, string> dataStrings_ = [];

        // Token: 0x04000947 RID: 2375
        private static char save_check;

        // Token: 0x04000948 RID: 2376
        private static readonly string saveFileName_ = "ctrwp7_preferences.json";

        // Token: 0x04000949 RID: 2377
        private static readonly string saveDirectoryName_ = "CutTheRopeWP7_savedata";

        // Token: 0x0400094A RID: 2378
        private static string saveDirectoryPath_;

        // Token: 0x0400094B RID: 2379
        private static bool initialised;

        // Token: 0x0400094C RID: 2380
        private static readonly bool saveInProcess;

        // Token: 0x0400094D RID: 2381
        private static readonly byte[] saveArray;

        // Token: 0x0400094E RID: 2382
        public static bool firstStart = true;

        private sealed class PreferencesSaveData
        {
            public Dictionary<string, int> ints { get; set; } = [];

            public Dictionary<string, string> strings { get; set; } = [];
        }
    }
}
