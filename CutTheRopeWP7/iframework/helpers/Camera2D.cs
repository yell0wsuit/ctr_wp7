using System;
using ctr_wp7.iframework.core;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.helpers
{
	// Token: 0x0200003B RID: 59
	internal class Camera2D : NSObject
	{
		// Token: 0x0600020E RID: 526 RVA: 0x0000DB3C File Offset: 0x0000BD3C
		public virtual Camera2D initWithSpeedandType(float s, CAMERA_TYPE t)
		{
			if (base.init() != null)
			{
				this.speed = s;
				this.type = t;
			}
			return this;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000DB58 File Offset: 0x0000BD58
		public virtual void moveToXYImmediate(float x, float y, bool immediate)
		{
			this.target.x = x;
			this.target.y = y;
			if (immediate)
			{
				this.pos = this.target;
				return;
			}
			if (this.type == CAMERA_TYPE.CAMERA_SPEED_DELAY)
			{
				this.offset = MathHelper.vectMult(MathHelper.vectSub(this.target, this.pos), this.speed);
				return;
			}
			if (this.type == CAMERA_TYPE.CAMERA_SPEED_PIXELS)
			{
				this.offset = MathHelper.vectMult(MathHelper.vectNormalize(MathHelper.vectSub(this.target, this.pos)), this.speed);
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000DBE8 File Offset: 0x0000BDE8
		public virtual void update(float delta)
		{
			if (!MathHelper.vectEqual(this.pos, this.target))
			{
				this.pos = MathHelper.vectAdd(this.pos, MathHelper.vectMult(this.offset, delta));
				this.pos = MathHelper.vect(MathHelper.round((double)this.pos.x), MathHelper.round((double)this.pos.y));
				if (!MathHelper.sameSign(this.offset.x, this.target.x - this.pos.x) || !MathHelper.sameSign(this.offset.y, this.target.y - this.pos.y))
				{
					this.pos = this.target;
				}
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000DCB3 File Offset: 0x0000BEB3
		public virtual void applyCameraTransformation()
		{
			OpenGL.glTranslatef((double)(-(double)this.pos.x), (double)(-(double)this.pos.y), 0.0);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000DCDD File Offset: 0x0000BEDD
		public virtual void cancelCameraTransformation()
		{
			OpenGL.glTranslatef((double)this.pos.x, (double)this.pos.y, 0.0);
		}

		// Token: 0x04000812 RID: 2066
		public CAMERA_TYPE type;

		// Token: 0x04000813 RID: 2067
		public float speed;

		// Token: 0x04000814 RID: 2068
		public Vector pos;

		// Token: 0x04000815 RID: 2069
		public Vector target;

		// Token: 0x04000816 RID: 2070
		public Vector offset;
	}
}
