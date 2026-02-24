using ctr_wp7.ios;

namespace ctr_wp7.iframework.sfe
{
    internal sealed class Constraint : NSObject
    {
        public ConstraintedPoint cp;

        public float restLength;

        public CONSTRAINT type;

        public enum CONSTRAINT
        {
            CONSTRAINT_DISTANCE,
            CONSTRAINT_NOT_MORE_THAN,
            CONSTRAINT_NOT_LESS_THAN
        }
    }
}
