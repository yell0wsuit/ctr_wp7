using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;

using ctr_wp7.iframework;
using ctr_wp7.iframework.core;
using ctr_wp7.utils;

namespace ctr_wp7.remotedata.cartoons
{
    public abstract class ServerDataManager
    {
        protected static object readObject(string file, Type serializedObjectType)
        {
            object obj = null;
            using (IsolatedStorageFile userStoreForApplication = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (userStoreForApplication.FileExists(file))
                {
                    try
                    {
                        using (IsolatedStorageFileStream isolatedStorageFileStream = userStoreForApplication.OpenFile(file, FileMode.Open))
                        {
                            DataContractSerializer dataContractSerializer = new(serializedObjectType);
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

        protected static bool saveObject(object obj, string file)
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
                        DataContractSerializer dataContractSerializer = new(obj.GetType());
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

        protected static bool saveBytes(byte[] bytes, string file)
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
                        BinaryWriter binaryWriter = new(isolatedStorageFileStream);
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

        protected internal static void removeObject(string file)
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

        protected static void removeObjects(string[] prefixes)
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

        protected abstract void injectSizes(LinkBuilder link, int set);

        protected static void injectAdditionalParameters(LinkBuilder link)
        {
            string[] array = SystemInfo.getAppVersion().Split(['.']);
            string text = "";
            if (array.Length >= 2)
            {
                text = array[0] + "." + array[1];
            }
            link.put("model", SystemInfo.getPhoneModel());
            link.put("os", SystemInfo.getOSVersion());
            link.put("locale", ApplicationSettings.getString(8));
            link.put("lang", ApplicationSettings.getString(8));
            link.put("version", text);
            link.put("net", SystemInfo.getNetworkType());
            link.put("store", SystemInfo.getAppMarket());
        }

        public static void public_InjectParameters(LinkBuilder link)
        {
            injectAdditionalParameters(link);
        }

        protected int protocolVersion;

        protected string serverUrl;

        protected bool execution;
    }
}
