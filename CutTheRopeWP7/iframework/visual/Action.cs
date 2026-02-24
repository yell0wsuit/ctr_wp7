using ctr_wp7.ios;

namespace ctr_wp7.iframework.visual
{
    // Token: 0x020000A6 RID: 166
    internal sealed class Action : NSObject
    {
        // Token: 0x060004A9 RID: 1193 RVA: 0x00021E77 File Offset: 0x00020077
        public Action()
        {
            data = new ActionData();
        }

        // Token: 0x060004AA RID: 1194 RVA: 0x00021E8C File Offset: 0x0002008C
        public static Action createAction(BaseElement target, string action, int p, int sp)
        {
            Action action2 = (Action)new Action().init();
            action2.actionTarget = target;
            action2.data.actionName = action;
            action2.data.actionParam = p;
            action2.data.actionSubParam = sp;
            return action2;
        }

        // Token: 0x040009EC RID: 2540
        public BaseElement actionTarget;

        // Token: 0x040009ED RID: 2541
        public ActionData data;
    }
}
