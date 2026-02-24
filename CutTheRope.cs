using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ctre_wp7
{
	// Token: 0x02000027 RID: 39
	[DebuggerNonUserCode]
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal class CutTheRope
	{
		// Token: 0x0600017C RID: 380 RVA: 0x0000B5CF File Offset: 0x000097CF
		internal CutTheRope()
		{
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000B5D8 File Offset: 0x000097D8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(CutTheRope.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("ctre_wp7.CutTheRope", typeof(CutTheRope).Assembly);
					CutTheRope.resourceMan = resourceManager;
				}
				return CutTheRope.resourceMan;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000B617 File Offset: 0x00009817
		// (set) Token: 0x0600017F RID: 383 RVA: 0x0000B61E File Offset: 0x0000981E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return CutTheRope.resourceCulture;
			}
			set
			{
				CutTheRope.resourceCulture = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000B626 File Offset: 0x00009826
		internal static string FakeString
		{
			get
			{
				return CutTheRope.ResourceManager.GetString("FakeString", CutTheRope.resourceCulture);
			}
		}

		// Token: 0x040007BF RID: 1983
		private static ResourceManager resourceMan;

		// Token: 0x040007C0 RID: 1984
		private static CultureInfo resourceCulture;
	}
}
