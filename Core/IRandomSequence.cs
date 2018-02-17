using System;

namespace Rendering.Core
{
    public interface IRandomSequence
    {
        double Double(double min, double max);
    }

    public static class RandomSequenceExtensions
    {
        public static UnitVector DirectionInHemisphere(this IRandomSequence randomSequence, UnitVector normal)
        {
            var r1 = randomSequence.Double(0.0, 1.0);
            var r2 = randomSequence.Double(0.0, 1.0);

            var (vb1, vb2) = normal.Perpendiculars();

            var theta = Math.Asin(Math.Sqrt(r1));
            var phi = Math.PI * Math.PI * r2;
            var sinTheta = Math.Sin(theta);
            var cosTheta = Math.Cos(theta);
            var sinPhi = Math.Sin(phi);
            var cosPhi = Math.Cos(phi);

            var v1 = vb1 * (sinTheta * sinPhi);
            var v2 = normal * cosTheta;
            var v3 = vb2 * (cosPhi * sinTheta);

            return (v1 + v2 + v3).Normalize();
        }

    }
}