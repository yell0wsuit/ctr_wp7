using System;

using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.sfe
{
    // Token: 0x0200001E RID: 30
    internal class MaterialPoint : NSObject
    {
        // Token: 0x06000148 RID: 328 RVA: 0x0000A485 File Offset: 0x00008685
        public override NSObject init()
        {
            if (base.init() != null)
            {
                this.forces = new Vector[10];
                this.setWeight(1f);
                this.resetAll();
            }
            return this;
        }

        // Token: 0x06000149 RID: 329 RVA: 0x0000A4AE File Offset: 0x000086AE
        public virtual void setWeight(float w)
        {
            this.weight = w;
            this.invWeight = (float)(1.0 / (double)this.weight);
            this.gravity = MathHelper.vect(0f, 784f * this.weight);
        }

        // Token: 0x0600014A RID: 330 RVA: 0x0000A4EB File Offset: 0x000086EB
        public override void dealloc()
        {
            this.forces = null;
            base.dealloc();
        }

        // Token: 0x0600014B RID: 331 RVA: 0x0000A4FA File Offset: 0x000086FA
        public virtual void resetForces()
        {
            this.forces = new Vector[10];
            this.highestForceIndex = -1;
        }

        // Token: 0x0600014C RID: 332 RVA: 0x0000A510 File Offset: 0x00008710
        public virtual void resetAll()
        {
            this.resetForces();
            this.v = MathHelper.vectZero;
            this.a = MathHelper.vectZero;
            this.pos = MathHelper.vectZero;
            this.posDelta = MathHelper.vectZero;
            this.totalForce = MathHelper.vectZero;
        }

        // Token: 0x0600014D RID: 333 RVA: 0x0000A54F File Offset: 0x0000874F
        public virtual void setForcewithID(Vector force, int n)
        {
            this.forces[n] = force;
            if (n > this.highestForceIndex)
            {
                this.highestForceIndex = n;
            }
        }

        // Token: 0x0600014E RID: 334 RVA: 0x0000A573 File Offset: 0x00008773
        public virtual void deleteForce(int n)
        {
            this.forces[n] = MathHelper.vectZero;
        }

        // Token: 0x0600014F RID: 335 RVA: 0x0000A58B File Offset: 0x0000878B
        public virtual Vector getForce(int n)
        {
            return this.forces[n];
        }

        // Token: 0x06000150 RID: 336 RVA: 0x0000A5A0 File Offset: 0x000087A0
        public virtual void applyImpulseDelta(Vector impulse, float delta)
        {
            if (!MathHelper.vectEqual(impulse, MathHelper.vectZero))
            {
                Vector vector = MathHelper.vectMult(impulse, (float)((double)delta / 1.0));
                this.pos = MathHelper.vectAdd(this.pos, vector);
            }
        }

        // Token: 0x06000151 RID: 337 RVA: 0x0000A5E0 File Offset: 0x000087E0
        public virtual void updatewithPrecision(float delta, float p)
        {
            int num = (int)(delta / p) + 1;
            if (num != 0)
            {
                delta /= (float)num;
            }
            for (int i = 0; i < num; i++)
            {
                this.update(delta);
            }
        }

        // Token: 0x06000152 RID: 338 RVA: 0x0000A610 File Offset: 0x00008810
        public virtual void update(float delta)
        {
            this.totalForce = MathHelper.vectZero;
            if (!this.disableGravity)
            {
                if (!MathHelper.vectEqual(MaterialPoint.globalGravity, MathHelper.vectZero))
                {
                    this.totalForce = MathHelper.vectAdd(this.totalForce, MathHelper.vectMult(MaterialPoint.globalGravity, this.weight));
                }
                else
                {
                    this.totalForce = MathHelper.vectAdd(this.totalForce, this.gravity);
                }
            }
            if (this.highestForceIndex != -1)
            {
                for (int i = 0; i <= this.highestForceIndex; i++)
                {
                    this.totalForce = MathHelper.vectAdd(this.totalForce, this.forces[i]);
                }
            }
            this.totalForce = MathHelper.vectMult(this.totalForce, this.invWeight);
            this.a = MathHelper.vectMult(this.totalForce, (float)((double)delta / 1.0));
            this.v = MathHelper.vectAdd(this.v, this.a);
            this.posDelta = MathHelper.vectMult(this.v, (float)((double)delta / 1.0));
            this.pos = MathHelper.vectAdd(this.pos, this.posDelta);
        }

        // Token: 0x06000153 RID: 339 RVA: 0x0000A738 File Offset: 0x00008938
        public virtual void drawForces()
        {
        }

        // Token: 0x04000775 RID: 1909
        protected const double TIME_SCALE = 1.0;

        // Token: 0x04000776 RID: 1910
        private const double PIXEL_TO_SI_METERS_K = 80.0;

        // Token: 0x04000777 RID: 1911
        public const double GCONST = 784.0;

        // Token: 0x04000778 RID: 1912
        private const int MAX_FORCES = 10;

        // Token: 0x04000779 RID: 1913
        public static Vector globalGravity = default(Vector);

        // Token: 0x0400077A RID: 1914
        public Vector pos;

        // Token: 0x0400077B RID: 1915
        public Vector posDelta;

        // Token: 0x0400077C RID: 1916
        public Vector v;

        // Token: 0x0400077D RID: 1917
        public Vector a;

        // Token: 0x0400077E RID: 1918
        public Vector totalForce;

        // Token: 0x0400077F RID: 1919
        public float weight;

        // Token: 0x04000780 RID: 1920
        public float invWeight;

        // Token: 0x04000781 RID: 1921
        public Vector[] forces;

        // Token: 0x04000782 RID: 1922
        public int highestForceIndex;

        // Token: 0x04000783 RID: 1923
        public Vector gravity;

        // Token: 0x04000784 RID: 1924
        public bool disableGravity;
    }
}
