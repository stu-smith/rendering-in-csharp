using System;

namespace Rendering.Core
{
    public static class MathUtility
    {
        public static double Clamp(double v, double min, double max)
        {
            if (v < min)
            {
                return min;
            }
            if (v > max)
            {
                return max;
            }
            return v;
        }
    }
}