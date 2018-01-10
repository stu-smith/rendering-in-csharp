using System;
using Rendering.Core;

namespace Rendering.Volumes
{
    public class Sphere : Volume
    {
        public Sphere(Material material, double radius)
        : base(material)
        {
            _radius = radius;
            _inverseRadius = 1.0 / radius;
        }

        public override RayPosition? SurfaceIntersection(Ray ray)
        {
            var ro = ray.RayOrigin;
            var rd = ray.RayDirection;
            var op = ro.To(Point.Origin);
            var b = Vector.DotProduct(op, rd);
            var det = b * b - Vector.DotProduct(op, op) + _radius * _radius;

            if (det < 0.0)
            {
                return null;
            }

            var eps = 0.0001;
            var sd = Math.Sqrt(det);

            if (b - sd > eps)
            {
                return new RayPosition(b - sd);
            }
            if (b + sd > eps)
            {
                return new RayPosition(b + sd);
            }

            return null;
        }

        public override UnitVector SurfaceNormalAtPoint(Point point)
        {
            return UnitVector.UnsafeCreateUnitVector(Point.Origin.To(point) * _inverseRadius);
        }

        public override bool IsPointInVolume(Point p)
        {
            var ds = p.X * p.X + p.Y * p.Y + p.Z * p.Z;

            return ds <= _radius * _radius;
        }

        private readonly double _radius, _inverseRadius;
    }
}
