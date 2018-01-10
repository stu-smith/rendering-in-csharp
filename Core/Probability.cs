using System;

namespace Rendering.Core
{
    public struct Probability
    {
        public Probability(double value)
        {
            if (value < 0.0 || value > 1.0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            Value = value;
        }

        public static Probability Zero => new Probability();

        public static Probability operator +(Probability pa, Probability pb)
        {
            return new Probability(pa.Value + pb.Value);
        }

        public static Probability operator -(Probability pa, Probability pb)
        {
            return new Probability(pa.Value - pb.Value);
        }

        public static Probability operator *(Probability pa, Probability pb)
        {
            return new Probability(pa.Value * pb.Value);
        }

        public static Probability operator /(Probability pa, Probability pb)
        {
            return new Probability(pa.Value / pb.Value);
        }

        public static Probability Max(Probability pa, Probability pb)
        {
            return new Probability(Math.Max(pa.Value, pb.Value));
        }

        public static Probability Max(Probability pa, Probability pb, Probability pc)
        {
            return new Probability(Math.Max(pa.Value, Math.Max(pb.Value, pc.Value)));
        }

        public double Value { get; }
    }
}