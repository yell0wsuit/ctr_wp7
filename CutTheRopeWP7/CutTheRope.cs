using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ctr_wp7
{
	[DebuggerNonUserCode]
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal class CutTheRope
	{
		internal CutTheRope()
		{
		}

		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000B5D8 File Offset: 0x000097D8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(CutTheRope.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("ctr_wp7.CutTheRope", typeof(CutTheRope).Assembly);
					CutTheRope.resourceMan = resourceManager;
				}
				return CutTheRope.resourceMan;
			}
		}

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

		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000B626 File Offset: 0x00009826
		internal static string FakeString
		{
			get
			{
				return CutTheRope.ResourceManager.GetString("FakeString", CutTheRope.resourceCulture);
			}
		}

		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;
	}
}
