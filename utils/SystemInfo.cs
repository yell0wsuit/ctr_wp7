using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ctr_wp7.utils
{
    public static class SystemInfo
    {
        public static string getPhoneModel()
        {
            return Environment.MachineName;
        }

        public static string getOSVersion()
        {
            return Environment.OSVersion.VersionString;
        }

        public static string getAppName()
        {
            return "Cut The Rope";
        }

        public static string getAppMarket()
        {
            return "Desktop";
        }

        public static string getAppVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0.0";
        }

        public static int getNetworkType()
        {
            return 1;
        }

        public static void setStoreTextureInRAM(bool value)
        {
            _storeTextureInRAM = value;
        }

        public static bool getStoreTextureInRAM()
        {
            return _storeTextureInRAM;
        }

        public static string getDeviceManufacturer()
        {
            return RuntimeInformation.OSDescription;
        }

        private static bool _storeTextureInRAM = true;
    }
}
