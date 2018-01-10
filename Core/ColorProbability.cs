using System;

namespace Rendering.Core
{
    public struct ColorProbability
    {
        public ColorProbability(Probability r, Probability g, Probability b)
        {
            Red = r;
            Green = g;
            Blue = b;
        }

        public ColorProbability(double r, double g, double b)
        {
            Red = new Probability(r);
            Green = new Probability(g);
            Blue = new Probability(b);
        }

        public Probability SumComponents()
        {
            return Red + Green + Blue;
        }

        public static ColorProbability Zero => new ColorProbability();

        public static Probability MaxOfAdditions(ColorProbability cpa, ColorProbability cpb)
        {
            var ar = cpa.Red;
            var ag = cpa.Green;
            var ab = cpa.Blue;

            var br = cpb.Red;
            var bg = cpb.Green;
            var bb = cpb.Blue;

            return Probability.Max(ar + br, ag + bg, ab + bb);
        }

        public static Probability ProportionOfAdditions(ColorProbability cpa, ColorProbability cpb)
        {
            var aSum = cpa.Red.Value + cpa.Green.Value + cpa.Blue.Value;
            var bSum = cpb.Red.Value + cpb.Green.Value + cpb.Blue.Value;

            return new Probability(aSum / (aSum + bSum));
        }

        public Probability Red { get; }
        public Probability Green { get; }
        public Probability Blue { get; }
    }
}