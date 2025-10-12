using System;
using System.Collections.Generic;
using ctre_wp7.ios;

namespace ctre_wp7.iframework.helpers
{
	// Token: 0x0200007F RID: 127
	internal class DelayedDispatcher : NSObject
	{
		// Token: 0x060003B2 RID: 946 RVA: 0x00017F9E File Offset: 0x0001619E
		public DelayedDispatcher()
		{
			this.dispatchers = new List<Dispatch>();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00017FB1 File Offset: 0x000161B1
		public override void dealloc()
		{
			this.dispatchers.Clear();
			this.dispatchers = null;
			base.dealloc();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00017FCB File Offset: 0x000161CB
		public virtual void callObjectSelectorParamafterDelay(DelayedDispatcher.DispatchFunc s, NSObject p, double d)
		{
			this.callObjectSelectorParamafterDelay(s, p, (float)d);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00017FD8 File Offset: 0x000161D8
		public virtual void callObjectSelectorParamafterDelay(DelayedDispatcher.DispatchFunc s, NSObject p, float d)
		{
			Dispatch dispatch = new Dispatch().initWithObjectSelectorParamafterDelay(s, p, d);
			this.dispatchers.Add(dispatch);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00018000 File Offset: 0x00016200
		public virtual void update(float d)
		{
			int num = this.dispatchers.Count;
			for (int i = 0; i < num; i++)
			{
				Dispatch dispatch = this.dispatchers[i];
				dispatch.delay -= d;
				if ((double)dispatch.delay <= 0.0)
				{
					dispatch.dispatch();
					this.dispatchers.Remove(dispatch);
					i--;
					num--;
				}
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001806D File Offset: 0x0001626D
		public virtual void cancelAllDispatches()
		{
			this.dispatchers.Clear();
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001807A File Offset: 0x0001627A
		public virtual void cancelDispatchWithObjectSelectorParam(DelayedDispatcher.DispatchFunc s, NSObject p)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000941 RID: 2369
		private List<Dispatch> dispatchers;

		// Token: 0x02000080 RID: 128
		// (Invoke) Token: 0x060003BA RID: 954
		public delegate void DispatchFunc(NSObject param);
	}
}
