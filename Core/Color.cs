using System;

namespace Rendering.Core
{
    public struct Color
    {
        public Color(double r, double g, double b)
        {
            _r = MathUtility.Clamp(r, 0.0, 1.0);
            _g = MathUtility.Clamp(g, 0.0, 1.0);
            _b = MathUtility.Clamp(b, 0.0, 1.0);
        }

        private readonly double _r, _g, _b;
    }
}