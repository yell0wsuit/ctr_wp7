using System;
using System.Reflection;
using Microsoft.Phone.Info;
using Microsoft.Phone.Net.NetworkInformation;

namespace ctre_wp7.utils
{
	// Token: 0x0200005C RID: 92
	public class SystemInfo
	{
		// Token: 0x060002BF RID: 703 RVA: 0x00011F06 File Offset: 0x00010106
		public static string getPhoneModel()
		{
			return DeviceStatus.DeviceName;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00011F10 File Offset: 0x00010110
		public static string getOSVersion()
		{
			string text = null;
			try
			{
				text = Environment.OSVersion.ToString();
			}
			catch (InvalidOperationException)
			{
				text = "";
			}
			return text;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00011F48 File Offset: 0x00010148
		public static string getAppName()
		{
			return "Cut The Rope";
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00011F4F File Offset: 0x0001014F
		public static string getAppMarket()
		{
			return "WindowsPhone";
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00011F58 File Offset: 0x00010158
		public static string getAppVersion()
		{
			return Assembly.GetExecutingAssembly().FullName.Split(new char[] { '=' })[1].Split(new char[] { ',' })[0];
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00011F99 File Offset: 0x00010199
		public static int getNetworkType()
		{
			if (!DeviceNetworkInformation.IsNetworkAvailable)
			{
				return 0;
			}
			if (DeviceNetworkInformation.IsWiFiEnabled)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00011FAE File Offset: 0x000101AE
		public static void setStoreTextureInRAM(bool value)
		{
			SystemInfo.storeTextureInRAM = value;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00011FB6 File Offset: 0x000101B6
		public static bool getStoreTextureInRAM()
		{
			return SystemInfo.storeTextureInRAM;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00011FBD File Offset: 0x000101BD
		public static string getDeviceManufacturer()
		{
			return DeviceStatus.DeviceManufacturer;
		}

		// Token: 0x040008B7 RID: 2231
		protected static bool storeTextureInRAM = true;
	}
}
