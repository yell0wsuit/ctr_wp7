using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using ctre_wp7.ios;

namespace ctre_wp7.iframework.core
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
			Preferences._loadPreferences();
			Preferences.initialised = true;
			return this;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00018152 File Offset: 0x00016352
		public static void ResetPreferences()
		{
			Preferences.data_ = null;
			Preferences.dataStrings_ = null;
			Preferences.data_ = new Dictionary<string, int>();
			Preferences.dataStrings_ = new Dictionary<string, string>();
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00018174 File Offset: 0x00016374
		public virtual void setIntforKey(int v, string k, bool comit)
		{
			Preferences._setIntforKey(v, k, comit);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0001817E File Offset: 0x0001637E
		public virtual void setBooleanforKey(bool v, string k, bool comit)
		{
			Preferences._setBooleanforKey(v, k, comit);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00018188 File Offset: 0x00016388
		public virtual void setStringforKey(string v, string k, bool comit)
		{
			Preferences._setStringforKey(v, k, comit);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00018192 File Offset: 0x00016392
		public virtual int getIntForKey(string k)
		{
			return Preferences._getIntForKey(k);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001819A File Offset: 0x0001639A
		public virtual float getFloatForKey(string k)
		{
			return 0f;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000181A1 File Offset: 0x000163A1
		public virtual bool getBooleanForKey(string k)
		{
			return Preferences._getBooleanForKey(k);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000181A9 File Offset: 0x000163A9
		public virtual string getStringForKey(string k)
		{
			return Preferences._getStringForKey(k);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000181B4 File Offset: 0x000163B4
		public static void _setIntforKey(int v, string key, bool comit)
		{
			int num;
			if (Preferences.data_.TryGetValue(key, ref num))
			{
				Preferences.data_[key] = v;
			}
			else
			{
				Preferences.data_.Add(key, v);
			}
			if (comit)
			{
				Preferences._savePreferences();
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000181F4 File Offset: 0x000163F4
		public static void _setStringforKey(string v, string k, bool comit)
		{
			string text;
			if (Preferences.dataStrings_.TryGetValue(k, ref text))
			{
				Preferences.dataStrings_[k] = v;
			}
			else
			{
				Preferences.dataStrings_.Add(k, v);
			}
			if (comit)
			{
				Preferences._savePreferences();
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00018234 File Offset: 0x00016434
		public static int _getIntForKey(string k)
		{
			int num;
			if (Preferences.data_.TryGetValue(k, ref num))
			{
				return num;
			}
			return 0;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00018253 File Offset: 0x00016453
		private static float _getFloatForKey(string k)
		{
			return 0f;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001825C File Offset: 0x0001645C
		public static bool _getBooleanForKey(string k)
		{
			int num = Preferences._getIntForKey(k);
			return num != 0;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00018277 File Offset: 0x00016477
		public static void _setBooleanforKey(bool v, string k, bool comit)
		{
			Preferences._setIntforKey(v ? 1 : 0, k, comit);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00018288 File Offset: 0x00016488
		public static string _getStringForKey(string k)
		{
			string text;
			if (Preferences.dataStrings_.TryGetValue(k, ref text))
			{
				return text;
			}
			return "";
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000182AC File Offset: 0x000164AC
		public void _deleteKey(string k, bool comit)
		{
			string text;
			if (Preferences.dataStrings_.TryGetValue(k, ref text))
			{
				Preferences.dataStrings_.Remove(k);
			}
			int num;
			if (Preferences.data_.TryGetValue(k, ref num))
			{
				Preferences.data_.Remove(k);
			}
			if (comit)
			{
				Preferences._savePreferences();
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000182F8 File Offset: 0x000164F8
		public void _deleteKeysStartWith(string ks, bool comit)
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, string> keyValuePair in Preferences.dataStrings_)
			{
				string key = keyValuePair.Key;
				if (key.StartsWith(ks))
				{
					list.Add(key);
				}
			}
			foreach (string text in list)
			{
				Preferences.dataStrings_.Remove(text);
			}
			list.Clear();
			foreach (KeyValuePair<string, int> keyValuePair2 in Preferences.data_)
			{
				string key2 = keyValuePair2.Key;
				if (key2.StartsWith(ks))
				{
					list.Add(key2);
				}
			}
			foreach (string text2 in list)
			{
				Preferences.data_.Remove(text2);
			}
			if (comit)
			{
				Preferences._savePreferences();
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00018450 File Offset: 0x00016650
		public static bool isSaveFinished()
		{
			return !Preferences.saveInProcess;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0001845A File Offset: 0x0001665A
		public virtual void savePreferences()
		{
			Preferences._savePreferences();
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00018464 File Offset: 0x00016664
		public static void _savePreferences()
		{
			bool flag = false;
			bool flag2 = false;
			try
			{
				using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
				{
					try
					{
						userStoreForApplication.DeleteFile(Preferences.saveBakFileName_);
						userStoreForApplication.MoveFile(Preferences.saveFileName_, Preferences.saveBakFileName_);
						flag = true;
					}
					catch (Exception ex)
					{
						FrameworkTypes._LOG("Error: cannot save, " + ex.ToString());
					}
					using (IsolatedStorageFileStream isolatedStorageFileStream = userStoreForApplication.CreateFile(Preferences.saveFileName_))
					{
						BinaryWriter binaryWriter = new BinaryWriter(isolatedStorageFileStream);
						binaryWriter.Write(Preferences.data_.Count);
						foreach (KeyValuePair<string, int> keyValuePair in Preferences.data_)
						{
							binaryWriter.Write(keyValuePair.Key);
							binaryWriter.Write(keyValuePair.Value);
						}
						binaryWriter.Write(Preferences.dataStrings_.Count);
						foreach (KeyValuePair<string, string> keyValuePair2 in Preferences.dataStrings_)
						{
							binaryWriter.Write(keyValuePair2.Key);
							binaryWriter.Write(keyValuePair2.Value);
						}
						binaryWriter.Close();
						flag2 = true;
					}
				}
			}
			catch (Exception ex2)
			{
				FrameworkTypes._LOG("Error: cannot save, " + ex2.ToString());
			}
			if (!flag2 && flag)
			{
				using (IsolatedStorageFile userStoreForApplication2 = IsolatedStorageFile.GetUserStoreForApplication())
				{
					try
					{
						userStoreForApplication2.DeleteFile(Preferences.saveFileName_);
						userStoreForApplication2.MoveFile(Preferences.saveBakFileName_, Preferences.saveFileName_);
					}
					catch (Exception ex3)
					{
						FrameworkTypes._LOG("Error: cannot save, " + ex3.ToString());
					}
					return;
				}
			}
			if (flag2)
			{
				using (IsolatedStorageFile userStoreForApplication3 = IsolatedStorageFile.GetUserStoreForApplication())
				{
					userStoreForApplication3.DeleteFile(Preferences.saveBakFileName_);
					userStoreForApplication3.CopyFile(Preferences.saveFileName_, Preferences.saveBakFileName_);
				}
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00018728 File Offset: 0x00016928
		public static void loadFromFile(IsolatedStorageFile isf, string fname)
		{
			using (IsolatedStorageFileStream isolatedStorageFileStream = isf.OpenFile(fname, 3))
			{
				BinaryReader binaryReader = new BinaryReader(isolatedStorageFileStream);
				try
				{
					Preferences.save_check = 'N';
					int num = binaryReader.ReadInt32();
					for (int i = 0; i < num; i++)
					{
						string text = binaryReader.ReadString();
						int num2 = binaryReader.ReadInt32();
						Preferences.data_.Add(text, num2);
					}
					num = binaryReader.ReadInt32();
					for (int j = 0; j < num; j++)
					{
						string text2 = binaryReader.ReadString();
						string text3 = binaryReader.ReadString();
						Preferences.dataStrings_.Add(text2, text3);
					}
					Preferences.firstStart = false;
					binaryReader.Close();
				}
				catch (Exception ex)
				{
					FrameworkTypes._LOG("Error:" + ex.ToString());
					FrameworkTypes._LOG("Error:" + fname + "corrupted");
					Preferences.save_check = 'C';
					binaryReader.Close();
				}
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00018824 File Offset: 0x00016A24
		public virtual bool loadPreferences()
		{
			return Preferences._loadPreferences();
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001882C File Offset: 0x00016A2C
		internal static bool _loadPreferences()
		{
			bool flag;
			using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
			{
				if (userStoreForApplication.FileExists(Preferences.saveFileName_))
				{
					Preferences.save_check = 'C';
					Preferences.loadFromFile(userStoreForApplication, Preferences.saveFileName_);
					if (Preferences.save_check == 'N')
					{
						if (userStoreForApplication.FileExists(Preferences.saveBakFileName_))
						{
							userStoreForApplication.DeleteFile(Preferences.saveBakFileName_);
						}
						userStoreForApplication.CopyFile(Preferences.saveFileName_, Preferences.saveBakFileName_);
						flag = !Preferences.firstStart;
					}
					else
					{
						FrameworkTypes._LOG("Error:" + Preferences.saveFileName_ + "corrupted");
						userStoreForApplication.DeleteFile(Preferences.saveFileName_);
						Preferences.ResetPreferences();
						if (userStoreForApplication.FileExists(Preferences.saveBakFileName_))
						{
							Preferences.save_check = 'C';
							Preferences.loadFromFile(userStoreForApplication, Preferences.saveBakFileName_);
							if (Preferences.save_check == 'N')
							{
								flag = !Preferences.firstStart;
							}
							else
							{
								Preferences.ResetPreferences();
								flag = !Preferences.firstStart;
							}
						}
						else
						{
							Preferences.ResetPreferences();
							flag = !Preferences.firstStart;
						}
					}
				}
				else
				{
					FrameworkTypes._LOG("Info: nothing to load - trying to load .bak");
					if (userStoreForApplication.FileExists(Preferences.saveBakFileName_))
					{
						Preferences.save_check = 'C';
						Preferences.loadFromFile(userStoreForApplication, Preferences.saveBakFileName_);
						if (Preferences.save_check == 'N')
						{
							flag = !Preferences.firstStart;
						}
						else
						{
							userStoreForApplication.DeleteFile(Preferences.saveBakFileName_);
							Preferences.ResetPreferences();
							flag = !Preferences.firstStart;
						}
					}
					else
					{
						flag = !Preferences.firstStart;
					}
				}
			}
			return flag;
		}

		// Token: 0x04000945 RID: 2373
		private static Dictionary<string, int> data_ = new Dictionary<string, int>();

		// Token: 0x04000946 RID: 2374
		private static Dictionary<string, string> dataStrings_ = new Dictionary<string, string>();

		// Token: 0x04000947 RID: 2375
		private static char save_check;

		// Token: 0x04000948 RID: 2376
		private static string saveFileName_ = "ctr.sav";

		// Token: 0x04000949 RID: 2377
		private static string saveBakFileName_ = "ctr.sav.bak";

		// Token: 0x0400094A RID: 2378
		private static bool initialised = false;

		// Token: 0x0400094B RID: 2379
		private static bool saveInProcess = false;

		// Token: 0x0400094C RID: 2380
		private static byte[] saveArray = null;

		// Token: 0x0400094D RID: 2381
		public static bool firstStart = true;
	}
}
