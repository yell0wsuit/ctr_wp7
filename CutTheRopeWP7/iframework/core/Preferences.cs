using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

using ctr_wp7.ios;

namespace ctr_wp7.iframework.core
{
    internal class Preferences : NSObject
    {
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

        public static void ResetPreferences()
        {
            data_ = [];
            dataStrings_ = [];
        }

        public virtual void setIntforKey(int v, string k, bool comit)
        {
            _setIntforKey(v, k, comit);
        }

        public virtual void setBooleanforKey(bool v, string k, bool comit)
        {
            _setBooleanforKey(v, k, comit);
        }

        public virtual void setStringforKey(string v, string k, bool comit)
        {
            _setStringforKey(v, k, comit);
        }

        public virtual int getIntForKey(string k)
        {
            return _getIntForKey(k);
        }

        public virtual float getFloatForKey(string k)
        {
            return 0f;
        }

        public virtual bool getBooleanForKey(string k)
        {
            return _getBooleanForKey(k);
        }

        public virtual string getStringForKey(string k)
        {
            return _getStringForKey(k);
        }

        public static void _setIntforKey(int v, string key, bool comit)
        {
            data_[key] = v;
            if (comit)
            {
                _savePreferences();
            }
        }

        public static void _setStringforKey(string v, string k, bool comit)
        {
            dataStrings_[k] = v;
            if (comit)
            {
                _savePreferences();
            }
        }

        public static int _getIntForKey(string k)
        {
            return data_.TryGetValue(k, out int num) ? num : 0;
        }

        private static float _getFloatForKey(string k)
        {
            return 0f;
        }

        public static bool _getBooleanForKey(string k)
        {
            int num = _getIntForKey(k);
            return num != 0;
        }

        public static void _setBooleanforKey(bool v, string k, bool comit)
        {
            _setIntforKey(v ? 1 : 0, k, comit);
        }

        public static string _getStringForKey(string k)
        {
            return dataStrings_.TryGetValue(k, out string text) ? text : "";
        }

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

        public static bool isSaveFinished()
        {
            return !saveInProcess;
        }

        public virtual void savePreferences()
        {
            _savePreferences();
        }

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

        public virtual bool loadPreferences()
        {
            return _loadPreferences();
        }

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

        private static Dictionary<string, int> data_ = [];

        private static Dictionary<string, string> dataStrings_ = [];

        private static char save_check;

        private static readonly string saveFileName_ = "ctrwp7_preferences.json";

        private static readonly string saveDirectoryName_ = "CutTheRopeWP7_savedata";

        private static string saveDirectoryPath_;

        private static bool initialised;

        private static readonly bool saveInProcess;

        private static readonly byte[] saveArray;

        public static bool firstStart = true;

        private sealed class PreferencesSaveData
        {
            public Dictionary<string, int> ints { get; set; } = [];

            public Dictionary<string, string> strings { get; set; } = [];
        }
    }
}
