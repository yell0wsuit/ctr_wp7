using System;
using ctre_wp7.iframework.core;
using ctre_wp7.iframework.helpers;
using ctre_wp7.ios;

namespace ctre_wp7.game
{
	// Token: 0x02000010 RID: 16
	internal class CTRGameObject : GameObject
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00008410 File Offset: 0x00006610
		public override void parseMover(XMLNode xml)
		{
			this.rotation = 0f;
			NSString nsstring = xml["angle"];
			if (nsstring != null)
			{
				this.rotation = nsstring.floatValue();
			}
			NSString nsstring2 = xml["path"];
			if (nsstring2 != null && nsstring2.length() != 0)
			{
				int num = 100;
				if (nsstring2.characterAtIndex(0) == 'R')
				{
					NSString nsstring3 = nsstring2.substringFromIndex(2);
					int num2 = nsstring3.intValue();
					num = num2 / 2 + 1;
				}
				float num3 = xml["moveSpeed"].floatValue();
				float num4 = num3;
				float num5 = xml["rotateSpeed"].floatValue();
				CTRMover ctrmover = (CTRMover)new CTRMover().initWithPathCapacityMoveSpeedRotateSpeed(num, num4, num5);
				ctrmover.angle = (double)this.rotation;
				ctrmover.setPathFromStringandStart(nsstring2, new Vector(this.x, this.y));
				this.setMover(ctrmover);
				ctrmover.start();
			}
		}
	}
}
