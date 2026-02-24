using System.Collections.Generic;

using ctr_wp7.iframework.core;
using ctr_wp7.iframework.helpers;
using ctr_wp7.ios;

namespace ctr_wp7.iframework.sfe
{
    // Token: 0x0200001F RID: 31
    internal class ConstraintedPoint : MaterialPoint
    {
        // Token: 0x06000156 RID: 342 RVA: 0x0000A74F File Offset: 0x0000894F
        public override void dealloc()
        {
            constraints = null;
            base.dealloc();
        }

        // Token: 0x06000157 RID: 343 RVA: 0x0000A75E File Offset: 0x0000895E
        public override NSObject init()
        {
            if (base.init() != null)
            {
                prevPos = vect(2.1474836E+09f, 2.1474836E+09f);
                pin = vect(-1f, -1f);
                constraints = [];
            }
            return this;
        }

        // Token: 0x06000158 RID: 344 RVA: 0x0000A7A0 File Offset: 0x000089A0
        public virtual void addConstraintwithRestLengthofType(ConstraintedPoint c, float r, Constraint.CONSTRAINT t)
        {
            Constraint constraint = new();
            _ = constraint.init();
            constraint.cp = c;
            constraint.restLength = r;
            constraint.type = t;
            constraints.Add(constraint);
        }

        // Token: 0x06000159 RID: 345 RVA: 0x0000A7DC File Offset: 0x000089DC
        public virtual void removeConstraint(ConstraintedPoint o)
        {
            for (int i = 0; i < constraints.Count; i++)
            {
                Constraint constraint = constraints[i];
                if (constraint.cp == o)
                {
                    constraints.RemoveAt(i);
                    return;
                }
            }
        }

        // Token: 0x0600015A RID: 346 RVA: 0x0000A822 File Offset: 0x00008A22
        public virtual void removeConstraints()
        {
            constraints = [];
        }

        // Token: 0x0600015B RID: 347 RVA: 0x0000A830 File Offset: 0x00008A30
        public virtual void changeConstraintFromTo(ConstraintedPoint o, ConstraintedPoint n)
        {
            int count = constraints.Count;
            for (int i = 0; i < count; i++)
            {
                Constraint constraint = constraints[i];
                if (constraint != null && constraint.cp == o)
                {
                    constraint.cp = n;
                    return;
                }
            }
        }

        // Token: 0x0600015C RID: 348 RVA: 0x0000A878 File Offset: 0x00008A78
        public virtual void changeConstraintFromTowithRestLength(ConstraintedPoint o, ConstraintedPoint n, float l)
        {
            int count = constraints.Count;
            for (int i = 0; i < count; i++)
            {
                Constraint constraint = constraints[i];
                if (constraint != null && constraint.cp == o)
                {
                    constraint.cp = n;
                    constraint.restLength = l;
                    return;
                }
            }
        }

        // Token: 0x0600015D RID: 349 RVA: 0x0000A8C8 File Offset: 0x00008AC8
        public virtual void changeRestLengthToFor(float l, ConstraintedPoint n)
        {
            int count = constraints.Count;
            for (int i = 0; i < count; i++)
            {
                Constraint constraint = constraints[i];
                if (constraint != null && constraint.cp == n)
                {
                    constraint.restLength = l;
                    return;
                }
            }
        }

        // Token: 0x0600015E RID: 350 RVA: 0x0000A910 File Offset: 0x00008B10
        public virtual bool hasConstraintTo(ConstraintedPoint p)
        {
            int count = constraints.Count;
            for (int i = 0; i < count; i++)
            {
                Constraint constraint = constraints[i];
                if (constraint != null && constraint.cp == p)
                {
                    return true;
                }
            }
            return false;
        }

        // Token: 0x0600015F RID: 351 RVA: 0x0000A954 File Offset: 0x00008B54
        public virtual float restLengthFor(ConstraintedPoint n)
        {
            int count = constraints.Count;
            for (int i = 0; i < count; i++)
            {
                Constraint constraint = constraints[i];
                if (constraint != null && constraint.cp == n)
                {
                    return constraint.restLength;
                }
            }
            return -1f;
        }

        // Token: 0x06000160 RID: 352 RVA: 0x0000A99E File Offset: 0x00008B9E
        public override void resetAll()
        {
            base.resetAll();
            prevPos = vect(2.1474836E+09f, 2.1474836E+09f);
            removeConstraints();
        }

        // Token: 0x06000161 RID: 353 RVA: 0x0000A9C1 File Offset: 0x00008BC1
        public override void update(float delta)
        {
            update(delta, 1f);
        }

        // Token: 0x06000162 RID: 354 RVA: 0x0000A9D0 File Offset: 0x00008BD0
        public virtual void update(float delta, float koeff)
        {
            totalForce = vectZero;
            if (!disableGravity)
            {
                totalForce = !vectEqual(globalGravity, vectZero) ? vectAdd(totalForce, vectMult(globalGravity, weight)) : vectAdd(totalForce, gravity);
            }
            if (highestForceIndex != -1)
            {
                for (int i = 0; i <= highestForceIndex; i++)
                {
                    totalForce = vectAdd(totalForce, forces[i]);
                }
            }
            totalForce = vectMult(totalForce, invWeight);
            a = vectMult(totalForce, (float)((double)delta / 1.0 * 0.01600000075995922 * (double)koeff));
            if (prevPos.x == 2.1474836E+09f)
            {
                prevPos = pos;
            }
            posDelta.x = pos.x - prevPos.x + a.x;
            posDelta.y = pos.y - prevPos.y + a.y;
            v = vectMult(posDelta, (float)(1.0 / (double)delta));
            prevPos = pos;
            pos = vectAdd(pos, posDelta);
        }

        // Token: 0x06000163 RID: 355 RVA: 0x0000AB74 File Offset: 0x00008D74
        public static void satisfyConstraints(ConstraintedPoint p)
        {
            if (p.pin.x != -1f)
            {
                p.pos = p.pin;
                return;
            }
            int count = p.constraints.Count;
            Vector vectZero = MathHelper.vectZero;
            int i = 0;
            while (i < count)
            {
                Constraint constraint = p.constraints[i];
                vectZero.x = constraint.cp.pos.x - p.pos.x;
                vectZero.y = constraint.cp.pos.y - p.pos.y;
                if (vectZero.x == 0f && vectZero.y == 0f)
                {
                    vectZero.x = vectZero.y = 1f;
                }
                float num = vectLength(vectZero);
                float restLength = constraint.restLength;
                switch (constraint.type)
                {
                    case Constraint.CONSTRAINT.CONSTRAINT_NOT_MORE_THAN:
                        if (num > restLength)
                        {
                            goto IL_0109;
                        }
                        break;
                    case Constraint.CONSTRAINT.CONSTRAINT_NOT_LESS_THAN:
                        if (num < restLength)
                        {
                            goto IL_0109;
                        }
                        break;
                    default:
                        goto IL_0109;
                }
            IL_021B:
                i++;
                continue;
                Vector vector;
            IL_0109:
                vector = vectZero;
                float invWeight = constraint.cp.invWeight;
                float num2 = (num > 1f) ? num : 1f;
                float num3 = (num - restLength) / (num2 * (p.invWeight + invWeight));
                float num4 = p.invWeight * num3;
                vectZero.x *= num4;
                vectZero.y *= num4;
                num4 = invWeight * num3;
                vector.x *= num4;
                vector.y *= num4;
                p.pos.x += vectZero.x;
                p.pos.y += vectZero.y;
                if (constraint.cp.pin.x == -1f)
                {
                    ConstraintedPoint cp = constraint.cp;
                    cp.pos.x -= vector.x;
                    ConstraintedPoint cp2 = constraint.cp;
                    cp2.pos.y -= vector.y;
                    goto IL_021B;
                }
                goto IL_021B;
            }
        }

        // Token: 0x06000164 RID: 356 RVA: 0x0000ADA8 File Offset: 0x00008FA8
        public static void qcpupdate(ConstraintedPoint p, float delta, float koeff)
        {
            p.totalForce = vectZero;
            if (!p.disableGravity)
            {
                p.totalForce = !vectEqual(globalGravity, vectZero)
                    ? vectAdd(p.totalForce, vectMult(globalGravity, p.weight))
                    : vectAdd(p.totalForce, p.gravity);
            }
            if (p.highestForceIndex != -1)
            {
                for (int i = 0; i <= p.highestForceIndex; i++)
                {
                    p.totalForce = vectAdd(p.totalForce, p.forces[i]);
                }
            }
            p.totalForce = vectMult(p.totalForce, p.invWeight);
            p.a = vectMult(p.totalForce, (float)((double)delta / 1.0 * 0.01600000075995922 * (double)koeff));
            if (p.prevPos.x == 2.1474836E+09f)
            {
                p.prevPos = p.pos;
            }
            p.posDelta.x = p.pos.x - p.prevPos.x + p.a.x;
            p.posDelta.y = p.pos.y - p.prevPos.y + p.a.y;
            p.v = vectMult(p.posDelta, (float)(1.0 / (double)delta));
            p.prevPos = p.pos;
            p.pos = vectAdd(p.pos, p.posDelta);
        }

        // Token: 0x04000785 RID: 1925
        public Vector prevPos;

        // Token: 0x04000786 RID: 1926
        public Vector pin;

        // Token: 0x04000787 RID: 1927
        public List<Constraint> constraints;

        // Token: 0x04000788 RID: 1928
        public float preCalcDblTime;

        // Token: 0x04000789 RID: 1929
        public float preCalcItersPerSecond;
    }
}
