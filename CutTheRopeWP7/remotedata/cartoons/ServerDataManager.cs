using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;

using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.utils;

namespace ctr_wp7.remotedata.cartoons
{
    // Token: 0x0200006F RID: 111
    public abstract class ServerDataManager
    {
        // Token: 0x06000356 RID: 854 RVA: 0x0001523C File Offset: 0x0001343C
        protected object readObject(string file, Type serializedObjectType)
        {
            object obj = null;
            using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (userStoreForApplication.FileExists(file))
                {
                    try
                    {
                        using (IsolatedStorageFileStream isolatedStorageFileStream = userStoreForApplication.OpenFile(file, System.IO.FileMode.Open))
                        {
                            DataContractSerializer dataContractSerializer = new DataContractSerializer(serializedObjectType);
                            obj = dataContractSerializer.ReadObject(isolatedStorageFileStream);
                        }
                        goto IL_0053;
                    }
                    catch (Exception ex)
                    {
                        FrameworkTypes._LOG("Error: cannot load, " + ex.ToString());
                        goto IL_0053;
                    }
                }
                obj = null;
            IL_0053:;
            }
            return obj;
        }

        // Token: 0x06000357 RID: 855 RVA: 0x000152D4 File Offset: 0x000134D4
        protected bool saveObject(object obj, string file)
        {
            string text = file + ".bak";
            bool flag = false;
            bool flag2 = false;
            try
            {
                using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        userStoreForApplication.DeleteFile(text);
                        userStoreForApplication.MoveFile(file, text);
                        flag = true;
                    }
                    catch (Exception)
                    {
                    }
                    using (IsolatedStorageFileStream isolatedStorageFileStream = userStoreForApplication.CreateFile(file))
                    {
                        DataContractSerializer dataContractSerializer = new DataContractSerializer(obj.GetType());
                        dataContractSerializer.WriteObject(isolatedStorageFileStream, obj);
                        flag2 = true;
                    }
                }
            }
            catch (Exception ex)
            {
                FrameworkTypes._LOG("Error: cannot save, " + ex.ToString());
            }
            if (!flag2 && flag)
            {
                using (IsolatedStorageFile userStoreForApplication2 = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        userStoreForApplication2.DeleteFile(file);
                        userStoreForApplication2.MoveFile(text, file);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return flag2;
        }

        // Token: 0x06000358 RID: 856 RVA: 0x000153E4 File Offset: 0x000135E4
        protected bool saveBytes(byte[] bytes, string file)
        {
            string text = file + ".bak";
            bool flag = false;
            bool flag2 = false;
            try
            {
                using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        userStoreForApplication.DeleteFile(text);
                        userStoreForApplication.MoveFile(file, text);
                        flag = true;
                    }
                    catch (Exception)
                    {
                    }
                    using (IsolatedStorageFileStream isolatedStorageFileStream = userStoreForApplication.CreateFile(file))
                    {
                        BinaryWriter binaryWriter = new BinaryWriter(isolatedStorageFileStream);
                        binaryWriter.Write(bytes);
                        binaryWriter.Close();
                        flag2 = true;
                    }
                }
            }
            catch (Exception ex)
            {
                FrameworkTypes._LOG("Error: cannot save, " + ex.ToString());
            }
            if (!flag2 && flag)
            {
                using (IsolatedStorageFile userStoreForApplication2 = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        userStoreForApplication2.DeleteFile(file);
                        userStoreForApplication2.MoveFile(text, file);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return flag2;
        }

        // Token: 0x06000359 RID: 857 RVA: 0x000154F4 File Offset: 0x000136F4
        protected internal void removeObject(string file)
        {
            using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    userStoreForApplication.DeleteFile(file);
                }
                catch (Exception)
                {
                }
            }
        }

        // Token: 0x0600035A RID: 858 RVA: 0x0001553C File Offset: 0x0001373C
        protected void removeObjects(string[] prefixes)
        {
            using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    string[] fileNames = userStoreForApplication.GetFileNames();
                    foreach (string text in fileNames)
                    {
                        foreach (string text2 in prefixes)
                        {
                            if (text.StartsWith(text2))
                            {
                                userStoreForApplication.DeleteFile(text);
                                break;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        // Token: 0x0600035B RID: 859
        protected abstract void injectSizes(LinkBuilder link, int set);

        // Token: 0x0600035C RID: 860 RVA: 0x000155D0 File Offset: 0x000137D0
        protected static void injectAdditionalParameters(LinkBuilder link)
        {
            string[] array = SystemInfo.getAppVersion().Split(new char[] { '.' });
            string text = "";
            if (array.Length >= 2)
            {
                text = array[0] + "." + array[1];
            }
            link.put("model", SystemInfo.getPhoneModel());
            link.put("os", SystemInfo.getOSVersion());
            link.put("locale", Application.sharedAppSettings().getString(8));
            link.put("lang", Application.sharedAppSettings().getString(8));
            link.put("version", text);
            link.put("net", SystemInfo.getNetworkType());
            link.put("store", SystemInfo.getAppMarket());
        }

        // Token: 0x0600035D RID: 861 RVA: 0x0001568F File Offset: 0x0001388F
        public static void public_InjectParameters(LinkBuilder link)
        {
            ServerDataManager.injectAdditionalParameters(link);
        }

        // Token: 0x040008EC RID: 2284
        protected int protocolVersion;

        // Token: 0x040008ED RID: 2285
        protected string serverUrl;

        // Token: 0x040008EE RID: 2286
        protected bool execution;
    }
}
