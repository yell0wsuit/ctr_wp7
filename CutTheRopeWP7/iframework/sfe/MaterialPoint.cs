using ctr_wp7.iframework.core;
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
                forces = new Vector[10];
                setWeight(1f);
                resetAll();
            }
            return this;
        }

        // Token: 0x06000149 RID: 329 RVA: 0x0000A4AE File Offset: 0x000086AE
        public virtual void setWeight(float w)
        {
            weight = w;
            invWeight = (float)(1.0 / weight);
            gravity = vect(0f, 784f * weight);
        }

        // Token: 0x0600014A RID: 330 RVA: 0x0000A4EB File Offset: 0x000086EB
        public override void dealloc()
        {
            forces = null;
            base.dealloc();
        }

        // Token: 0x0600014B RID: 331 RVA: 0x0000A4FA File Offset: 0x000086FA
        public virtual void resetForces()
        {
            forces = new Vector[10];
            highestForceIndex = -1;
        }

        // Token: 0x0600014C RID: 332 RVA: 0x0000A510 File Offset: 0x00008710
        public virtual void resetAll()
        {
            resetForces();
            v = vectZero;
            a = vectZero;
            pos = vectZero;
            posDelta = vectZero;
            totalForce = vectZero;
        }

        // Token: 0x0600014D RID: 333 RVA: 0x0000A54F File Offset: 0x0000874F
        public virtual void setForcewithID(Vector force, int n)
        {
            forces[n] = force;
            if (n > highestForceIndex)
            {
                highestForceIndex = n;
            }
        }

        // Token: 0x0600014E RID: 334 RVA: 0x0000A573 File Offset: 0x00008773
        public virtual void deleteForce(int n)
        {
            forces[n] = vectZero;
        }

        // Token: 0x0600014F RID: 335 RVA: 0x0000A58B File Offset: 0x0000878B
        public virtual Vector getForce(int n)
        {
            return forces[n];
        }

        // Token: 0x06000150 RID: 336 RVA: 0x0000A5A0 File Offset: 0x000087A0
        public virtual void applyImpulseDelta(Vector impulse, float delta)
        {
            if (!vectEqual(impulse, vectZero))
            {
                Vector vector = vectMult(impulse, (float)((double)delta / 1.0));
                pos = vectAdd(pos, vector);
            }
        }

        // Token: 0x06000151 RID: 337 RVA: 0x0000A5E0 File Offset: 0x000087E0
        public virtual void updatewithPrecision(float delta, float p)
        {
            int num = (int)(delta / p) + 1;
            if (num != 0)
            {
                delta /= num;
            }
            for (int i = 0; i < num; i++)
            {
                update(delta);
            }
        }

        // Token: 0x06000152 RID: 338 RVA: 0x0000A610 File Offset: 0x00008810
        public virtual void update(float delta)
        {
            totalForce = vectZero;
            if (!disableGravity)
            {
                if (!vectEqual(globalGravity, vectZero))
                {
                    totalForce = vectAdd(totalForce, vectMult(globalGravity, weight));
                }
                else
                {
                    totalForce = vectAdd(totalForce, gravity);
                }
            }
            if (highestForceIndex != -1)
            {
                for (int i = 0; i <= highestForceIndex; i++)
                {
                    totalForce = vectAdd(totalForce, forces[i]);
                }
            }
            totalForce = vectMult(totalForce, invWeight);
            a = vectMult(totalForce, (float)((double)delta / 1.0));
            v = vectAdd(v, a);
            posDelta = vectMult(v, (float)((double)delta / 1.0));
            pos = vectAdd(pos, posDelta);
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
        public static Vector globalGravity;

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
